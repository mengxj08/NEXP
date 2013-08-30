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
        private Boolean FLAG = true;

        private int conditionsNum = 1;// the total Num of the conditions

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
        public void setConditionsNum(int temp)//set the total num of conditions
        {
            this.conditionsNum = temp;
        }
        public int getConditionsNum()//get the total num of conditions
        {
            return this.conditionsNum;
        }
        public void RQContributeToVariable()
        {
            
            if (NEXP.MainWindow.modeType == NEXP.MODE.NewItem && FLAG)
            {
                FLAG = false;

                if (NEXP.MainWindow.datas.researchQuestion.hypothesis.mainSolution == null || NEXP.MainWindow.datas.researchQuestion.hypothesis.mainSolution == "") return;

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
        }
        public bool IsValidCondition()//Check for Valid Condition: IV/IDV/block/Trial
        {
            if (MainWindow.datas.independentVariables.Count == 0)
                return false;
            foreach (var idv in MainWindow.datas.independentVariables)
            {
                if (idv.levels.Count == 0)
                    return false;
            }
            if (MainWindow.datas.arrangement.block == 0 || MainWindow.datas.arrangement.trial == 0)
                return false;

            return true;
        }
        public bool IsValidEstimate()//Estimate if the arrangement makes sense
        {
            if (MainWindow.datas.arrangement.block == 0 || MainWindow.datas.arrangement.trial == 0 || MainWindow.datas.arrangement.actualNum == 0 || MainWindow.datas.arrangement.feePerParticipant == 0 || MainWindow.datas.arrangement.timePerTrial == 0)
                return false;

            if (MainWindow.datas.arrangement.totalTimeCost >= 120)
                return false;

            return true;
        }
    }
}
