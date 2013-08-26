using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace NEXP
{
    public class DataStructure
    {

        public ResearchQuestion researchQuestion { get; set; }
        public Arrangement arrangement { get; set; }
        public ObservableCollection<IndependentVariable> independentVariables { get; set; }
        public ObservableCollection<DependentVariable> dependentVariables { get; set; }


        public DataStructure()
        {
            researchQuestion = new ResearchQuestion();
            arrangement = new Arrangement();
            independentVariables = new ObservableCollection<IndependentVariable>();
            dependentVariables = new ObservableCollection<DependentVariable>();
        }

        // Construct from an existing xml file.
        public void ReadFromFile(String path)
        {
            Clear();

            XmlDocument doc = new XmlDocument();
            doc.Load(path);

            // Initiate research question.
            XmlNode researchQuestionNode = doc.SelectSingleNode("design_guide/research_question");
            researchQuestion.experimentTitle = researchQuestionNode.SelectSingleNode("experiment_title").InnerText;
            researchQuestion.experimentDescription = researchQuestionNode.SelectSingleNode("experiment_description").InnerText;
            researchQuestion.experimentConductor = researchQuestionNode.SelectSingleNode("experiment_conductor").InnerText;
            researchQuestion.hypothesis.mainSolution = researchQuestionNode.SelectSingleNode("hypothesis/main_solution").InnerText;
            // Initiate compare solutions.
            XmlNode compareSolutionsNode = researchQuestionNode.SelectSingleNode("hypothesis/compare_solutions");
            XmlNodeList compareSolutionsNodeList = compareSolutionsNode.SelectNodes("compare_solution");
            foreach (XmlNode node in compareSolutionsNodeList)
            {
                string compareSolution = node.InnerText;
                //compareSolution.id = Int32.Parse(node.SelectSingleNode("id").InnerText);
                //compareSolution.name = node.SelectSingleNode("name").InnerText;
                researchQuestion.hypothesis.compareSolutions.Add(compareSolution);
            }
            // Initiate tasks.
            XmlNode tasksNode = researchQuestionNode.SelectSingleNode("hypothesis/tasks");
            XmlNodeList tasksNodeList = tasksNode.SelectNodes("task");
            foreach (XmlNode node in tasksNodeList)
            {
                string task = node.InnerText;
                //task.id = Int32.Parse(node.SelectSingleNode("id").InnerText);
                //task.name = node.SelectSingleNode("name").InnerText;
                researchQuestion.hypothesis.tasks.Add(task);
            }
            // Initiate context.
            researchQuestion.hypothesis.context = researchQuestionNode.SelectSingleNode("hypothesis/context").InnerText;
            // Initiate measures.
            XmlNode measuresNode = researchQuestionNode.SelectSingleNode("hypothesis/measures");
            XmlNodeList measuresNodeList = measuresNode.SelectNodes("measure");
            foreach (XmlNode node in measuresNodeList)
            {
                string measure = node.InnerText;
                //measure.id = Int32.Parse(node.SelectSingleNode("id").InnerText);
                //measure.name = node.SelectSingleNode("name").InnerText;
                researchQuestion.hypothesis.measures.Add(measure);
            }
            // Initiate target population.
            researchQuestion.hypothesis.targetPopulation = researchQuestionNode.SelectSingleNode("hypothesis/target_population").InnerText;
            // End of initiating research question.

            // Initiate variables.
            XmlNode variablesNode = doc.SelectSingleNode("design_guide/variables");
            XmlNodeList ivNodeList = variablesNode.SelectNodes("independent_variable");
            foreach (XmlNode ivNode in ivNodeList)
            {
                IndependentVariable iv = new IndependentVariable();
                iv.name = ivNode.SelectSingleNode("name").InnerText;
                iv.subjectDesign = (SUBJECTDESIGN)Enum.Parse(typeof(SUBJECTDESIGN), ivNode.SelectSingleNode("subject_design").InnerText, true);
                XmlNode levelsNode = ivNode.SelectSingleNode("levels");
                XmlNodeList levelsNodeList = levelsNode.SelectNodes("level");
                foreach (XmlNode node in levelsNodeList)
                {
                    IndependentVariable.Level level = new IndependentVariable.Level();
                    //level.id = Int32.Parse(node.SelectSingleNode("id").InnerText);
                    level.name = node.SelectSingleNode("name").InnerText;
                    iv.levels.Add(level);
                }
                iv.counterBalance = (COUNTERBALANCE)Enum.Parse(typeof(COUNTERBALANCE), ivNode.SelectSingleNode("counter_balance").InnerText, true);
                independentVariables.Add(iv);
            }
            //Log.getLogInstance().writeLog(independentVariables.Count.ToString());
            XmlNodeList dvNodeList = variablesNode.SelectNodes("dependent_variable");
            foreach (XmlNode node in dvNodeList)
            {
                DependentVariable dv = new DependentVariable();
                dv.name = node.SelectSingleNode("name").InnerText;
                dependentVariables.Add(dv);
            }
            // End of initiating variables.

            // Initiate arrangement.
            XmlNode arrangementNode = doc.SelectSingleNode("design_guide/arrangement");
            arrangement.minNum = Int32.Parse(arrangementNode.SelectSingleNode("min_number").InnerText);
            arrangement.actualNum = Int32.Parse(arrangementNode.SelectSingleNode("actual_number").InnerText);
            arrangement.feePerParticipant = Int32.Parse(arrangementNode.SelectSingleNode("fee_per_participant").InnerText);
            arrangement.trial = Int32.Parse(arrangementNode.SelectSingleNode("trial").InnerText);
            arrangement.timePerTrial = Int32.Parse(arrangementNode.SelectSingleNode("time_per_trial").InnerText);
            arrangement.block = Int32.Parse(arrangementNode.SelectSingleNode("block").InnerText);
            XmlNode participantsNode = arrangementNode.SelectSingleNode("participants");
            XmlNodeList participantsNodeList = participantsNode.SelectNodes("participant");
            foreach (XmlNode node in participantsNodeList)
            {
                Arrangement.Participant participant = new Arrangement.Participant();
                participant.id = Int32.Parse(node.SelectSingleNode("id").InnerText);
                participant.individualArrangement = node.SelectSingleNode("individual_arrangement").InnerText;
                arrangement.participants.Add(participant);
            }
            // End of Initiating arrangement.
        }

        private void Clear()
        {
            //researchQuestion = new ResearchQuestion();
            //arrangement = new Arrangement();
            //independentVariables = new ObservableCollection<IndependentVariable>();
            //dependentVariables = new ObservableCollection<DependentVariable>();

            NEXP.MainWindow.datas.researchQuestion.hypothesis.compareSolutions.Clear();
            NEXP.MainWindow.datas.researchQuestion.hypothesis.measures.Clear();
            NEXP.MainWindow.datas.researchQuestion.hypothesis.tasks.Clear();

            NEXP.MainWindow.datas.independentVariables.Clear();
            NEXP.MainWindow.datas.dependentVariables.Clear();

            NEXP.MainWindow.datas.arrangement.participants.Clear();
        }

        // Store to an xml file.
        public bool WriteToXml(String path)
        {
            using (XmlWriter writer = XmlWriter.Create(path))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("design_guide");

                // Write research question.
                writer.WriteStartElement("research_question");
                writer.WriteElementString("experiment_title", researchQuestion.experimentTitle);
                writer.WriteElementString("experiment_description", researchQuestion.experimentDescription);
                writer.WriteElementString("experiment_conductor", researchQuestion.experimentConductor);

                writer.WriteStartElement("hypothesis");
                writer.WriteElementString("main_solution", researchQuestion.hypothesis.mainSolution);
                writer.WriteStartElement("compare_solutions");
                foreach (String compareSolution in researchQuestion.hypothesis.compareSolutions)
                {
                    //writer.WriteStartElement("compare_solution");
                    //writer.WriteElementString("id", compareSolution.id.ToString());
                    //writer.WriteElementString("name", compareSolution);
                    //writer.WriteEndElement();
                    writer.WriteElementString("compare_solution", compareSolution);
                }
                writer.WriteEndElement();
                writer.WriteStartElement("tasks");
                foreach (string task in researchQuestion.hypothesis.tasks)
                {
                    //writer.WriteStartElement("task");
                    //writer.WriteElementString("id", task.id.ToString());
                    //writer.WriteElementString("name", task);
                    //writer.WriteEndElement();
                    writer.WriteElementString("task", task);
                }
                writer.WriteEndElement();
                writer.WriteElementString("context", researchQuestion.hypothesis.context);
                writer.WriteStartElement("measures");
                foreach (string measure in researchQuestion.hypothesis.measures)
                {
                    //writer.WriteStartElement("measure");
                    //writer.WriteElementString("id", measure.id.ToString());
                    //writer.WriteElementString("name", measure);
                    //writer.WriteEndElement();
                    writer.WriteElementString("measure", measure);
                }
                writer.WriteEndElement();
                writer.WriteElementString("target_population", researchQuestion.hypothesis.targetPopulation);
                writer.WriteEndElement();

                // End of research question
                writer.WriteEndElement();
                

                // Write variables.
                writer.WriteStartElement("variables");
                
                // Write independent variables.
                foreach (IndependentVariable independentVariable in independentVariables)
                {
                    writer.WriteStartElement("independent_variable");
                    writer.WriteElementString("name", independentVariable.name);
                    writer.WriteElementString("subject_design", independentVariable.subjectDesign.ToString());
                    writer.WriteStartElement("levels");
                    foreach (IndependentVariable.Level level in independentVariable.levels)
                    {
                        writer.WriteStartElement("level");
                        //writer.WriteElementString("id", level.id.ToString());
                        writer.WriteElementString("name", level.name);
                        writer.WriteEndElement();
                    }
                    writer.WriteEndElement();
                    writer.WriteElementString("counter_balance", independentVariable.counterBalance.ToString());
                    writer.WriteEndElement();
                }
                // End of independent variables.

                // Write dependent variables.
                foreach (DependentVariable dependentVariable in dependentVariables)
                {
                    writer.WriteStartElement("dependent_variable");
                    writer.WriteElementString("name", dependentVariable.name);
                    writer.WriteEndElement();
                }
                // End of dependent variables.

                // Write control variables. Not implemented!
                // End of control variables.

                // Write random variables. Not implemented!
                // End of random variables.

                // End of variables.
                writer.WriteEndElement();


                // Write arrangement.
                writer.WriteStartElement("arrangement");
                writer.WriteElementString("min_number", arrangement.minNum.ToString());
                writer.WriteElementString("actual_number", arrangement.actualNum.ToString());
                writer.WriteElementString("fee_per_participant", arrangement.feePerParticipant.ToString());
                writer.WriteElementString("trial", arrangement.trial.ToString());
                writer.WriteElementString("time_per_trial", arrangement.timePerTrial.ToString());
                writer.WriteElementString("block", arrangement.block.ToString());
                writer.WriteStartElement("participants");
                foreach (Arrangement.Participant participant in arrangement.participants)
                {
                    writer.WriteStartElement("participant");
                    writer.WriteElementString("id", participant.id.ToString());
                    writer.WriteElementString("individual_arrangment", participant.individualArrangement);
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
                // End of arrangement.
                writer.WriteEndElement();


                // End of design_guide and document.
                writer.WriteEndElement();
                writer.WriteEndDocument();
            }

            return true;
        }
    }

    public class ResearchQuestion : ViewModelBase
    {
        private string _experimentTitle;
        public string experimentTitle 
        {
            get
            {
                return this._experimentTitle;
            }
            set
            {
                if(value != this.experimentTitle)
                {  
                    this._experimentTitle = value;
                    onPropertyChanged("experimentTitle");
                }
            }
        }
        private string _experimentDescription;
        public string experimentDescription 
        {
            get
            {
                return this._experimentDescription;
            }
            set
            {
                if (value != this.experimentDescription)
                {
                    this._experimentDescription = value;
                    onPropertyChanged("experimentDescription");
                }
            }
        }

        private string _experimentConductor;
        public string experimentConductor 
        {
            get
            {
                return this._experimentConductor;
            }
            set
            {
                if (value != this.experimentConductor)
                {
                    this._experimentConductor = value;
                    onPropertyChanged("experimentConductor");
                }
            }
        }
        public Hypothesis hypothesis { get; set; }

        public ResearchQuestion()
        {
            hypothesis = new Hypothesis();
        }

        public class Hypothesis : ViewModelBase
        {
            private string _mainSolution;
            public string mainSolution 
            {
                get
                {
                    return this._mainSolution;
                }
                set
                {
                    if (value != this.mainSolution)
                    {
                        this._mainSolution = value;
                        onPropertyChanged("mainSolution");
                    }
                }
            }

            public ObservableCollection<String> compareSolutions { get; set; }
            public ObservableCollection<String> tasks { get; set; }

            private string _context;
            public string context 
            {
                get
                {
                    return this._context;
                }
                set
                {
                    if (value != this.context)
                    {
                        this._context = value;
                        onPropertyChanged("context");
                    }
                }
            }

            public ObservableCollection<String> measures { get; set; }
            public string targetPopulation { get; set; }

            public Hypothesis()
            {
                mainSolution = " ";
                compareSolutions = new ObservableCollection<String>();
                tasks = new ObservableCollection<String>();
                measures = new ObservableCollection<String>();
            }
            /*
                        public class CompareSolution
                        {
                            public CompareSolution()
                            { 
                            }
                            public CompareSolution(int id, string name)
                            {
                                this.id = id;
                                this.name = name;
                            }

                            public int id { get; set; }
                            public string name { get; set; }
                        }

                        public class Task : ViewModelBase
                        {
                            public Task()
                            { 
                            }
                            public Task(int id, string name)
                            {
                                this.id = id;
                                this.name = name;
                            }
                            public int id { get; set; }
                            private string _name;
                            public string name 
                            {
                                get
                                {
                                    return this._name;
                                }
                                set
                                {
                                    if (value != this.name)
                                    {
                                        this._name = value;
                                        this.onPropertyChanged("name");
                                    }
                                }
                            }
                        }

                        public class Measure : ViewModelBase
                        {
                            public Measure()
                            { 
                            }
                            public Measure(int id, string name)
                            {
                                this.id = id;
                                this.name = name;
                            }
                            public int id { get; set; }
                            private string _name;
                            public string name 
                            {
                                get
                                {
                                    return this._name;
                                }
                                set
                                {
                                    if(value != this.name)
                                    {
                                        this._name = value;
                                        this.onPropertyChanged("name");
                                    }
                                }
                            }

                        }
             *  * */
        }
    }

    public class IndependentVariable 
    {
        public string name { get; set; }
        public SUBJECTDESIGN subjectDesign{ get; set;}
        public COUNTERBALANCE counterBalance{ get; set;}
        public ObservableCollection<Level> levels { get; set; }

        public IndependentVariable()
        {
            levels = new ObservableCollection<Level>();
        }
        public IndependentVariable(string IDVName)
        {
            this.name = IDVName;
            levels = new ObservableCollection<Level>();
        }
        public class Level : ViewModelBase
        {
            public Level()
            { 
            }
            public Level(string levelName)
            {
                this.name = levelName;
            }
            private string _name;
            public string name
            {
                get
                {
                    return this._name;
                }
                set
                {
                    if (value != this.name)
                    {
                        this._name = value;
                        this.onPropertyChanged("name");
                    }
                }
            }
        }
    }

    public class DependentVariable : ViewModelBase
    {
        public DependentVariable()
        { 
        }
        public DependentVariable(string DVName)
        {
            this.name = DVName;
        }
        private string _name;
        public string name 
        {
            get
            {
                return this._name;
            }
            set
            {
                if(value != this.name)
                {
                    this._name = value;
                    this.onPropertyChanged("name");
                }
            }
        }
    }

    public class ControlVariable
    {
    }

    public class RandomVariable
    {
    }

    public class Arrangement : ViewModelBase
    {
        public int minNum { get; set; }
        private int _actualNum;
        public int actualNum 
        {
            get
            {
                return this._actualNum;
            }
            set 
            {
                this._actualNum = value;
                this.totalPayment = this.feePerParticipant * this.actualNum;
                this.onPropertyChanged("totalPayment");
                this.onPropertyChanged("actualNum");
                //Log.getLogInstance().writeLog("actualNum");
            } 
        }

        private int _feePerParticipant;
        public int feePerParticipant 
        {
            get
            {
                return _feePerParticipant;
            }
            set
            {
                this._feePerParticipant = value;
                this.totalPayment = this._feePerParticipant * this.actualNum;
                this.onPropertyChanged("feePerParticipant");
                this.onPropertyChanged("totalPayment");
                //Log.getLogInstance().writeLog("Test");
            }
        }
        private int _trial;
        public int trial 
        {
            get
            {
                return this._trial;
            }
            set 
            {
                this._trial = value;
                this.totalTimeCost = this.trial * this.block * this.timePerTrial * this.actualNum / 60;
                this.onPropertyChanged("trial");
                this.onPropertyChanged("totalTimeCost");
            } 
        }
        private int _timePerTrial;
        public int timePerTrial 
        {
            get 
            {
                return this._timePerTrial;
            }
            set
            {
                this._timePerTrial = value;
                this.totalTimeCost = this.trial * this.block * this.timePerTrial * this.actualNum / 60;
                this.onPropertyChanged("timePerTrial");
                this.onPropertyChanged("totalTimeCost");
               
            }
        }
        private int _block;
        public int block 
        {
            get
            {
                return this._block;
            }
            set
            {
                this._block = value;
                this.totalTimeCost = this.trial * this.block * this.timePerTrial * this.actualNum / 60;
                this.onPropertyChanged("totalTimeCost");
                this.onPropertyChanged("block");
            }
        }
        

        public int totalTimeCost { get; set; }
        public int totalPayment { get; set; }
        public ObservableCollection<Participant> participants { get; set; }
        public Arrangement()
        {
            participants = new ObservableCollection<Participant>();
        }
        public class Participant
        {
            public int id;
            public string individualArrangement;
        }
    }

    public enum SUBJECTDESIGN { Between, Within};
    public enum COUNTERBALANCE { FullyCounterBalancing, LatinSquare, NoCounterBalancing};
}
