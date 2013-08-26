using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEXP
{
    class Control
    {
        private static Control control;
        //private Boolean FLAG = true;
        private Control()
        {
           
        }
        public static Control getControlInstance()
        {
            if (control == null)
            {
                control = new Control();
            }
            return control;
        }
        public void RQContributeToVariable()
        {
            //global::NEXP.MODE NEXP.MainWindow.modeTyp;
            /*
            if (NEXP.MainWindow.modeType == NEXP.MODE.NewItem && FLAG)
            {
                FLAG = false;

                NEXP.IndependentVariable suggest1 = new NEXP.IndependentVariable("Technology");

                suggest1.levels.Add(new NEXP.IndependentVariable.Level(NEXP.MainWindow.datas.researchQuestion.hypothesis.mainSolution));
                foreach(string temp in NEXP.MainWindow.datas.researchQuestion.hypothesis.compareSolutions)
                {
                    suggest1.levels.Add(new NEXP.IndependentVariable.Level(temp));
                }
                NEXP.MainWindow.datas.independentVariables.Add(suggest1);

                NEXP.IndependentVariable suggest2 = new NEXP.IndependentVariable("Tasks");
                foreach (string temp in NEXP.MainWindow.datas.researchQuestion.hypothesis.tasks)
                {
                    suggest2.levels.Add(new NEXP.IndependentVariable.Level(temp));
                }
                NEXP.MainWindow.datas.independentVariables.Add(suggest2);

                foreach (string temp in NEXP.MainWindow.datas.researchQuestion.hypothesis.measures)
                {
                    NEXP.MainWindow.datas.dependentVariables.Add(new NEXP.DependentVariable(temp));
                }
            }
            */
        }
    }
}
