using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NEXP.Content
{
    /// <summary>
    /// Interaction logic for Conditions.xaml
    /// </summary>
    public partial class Conditions : UserControl
    {
        public Conditions()
        {
            InitializeComponent();
            BindingProcess();
        }
        private void BindingProcess()
        {
            datagrid.DataContext = NEXP.MainWindow.datas.independentVariables;

        }
        private void WindowsLoaded(object sender, RoutedEventArgs e)//Calculate Condition and set Control.ConditionNum
        {
            int tmp = 1; //num of conditions

            foreach (var idv in MainWindow.datas.independentVariables)
            {
                if (idv.subjectDesign == SUBJECTDESIGN.Within)
                {
                    // Collect name of levels of idv.
                    List<string> levelsName = new List<string>();
                    foreach (var level in idv.levels)
                    {
                        levelsName.Add(level.name);
                    }

                    switch (idv.counterBalance)
                    {
                        case COUNTERBALANCE.FullyCounterBalancing: //Permutate

                            // Generate permutation.
                            int n = 0;
                            foreach (var i in Utils.GenerateSimulation.Permutate(levelsName, levelsName.Count))
                            {
                                n++;
                            }
                            tmp *= n;
                            break;

                        case COUNTERBALANCE.LatinSquare://count of the level

                            tmp *= levelsName.Count;
                            break;

                        case COUNTERBALANCE.NoCounterBalancing://count of the level
                            tmp *= levelsName.Count;
                            break;
                    }
                }
                else
                {
                    // Do nothing.
                    //Between IDV does not affect the num of conditions
                    //tmp *= idv.levels.Count;
                }
            }

            Control.getControlInstance().setConditionsNum(tmp);
        }
    }
}
