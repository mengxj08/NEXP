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
    /// Interaction logic for IDV.xaml
    /// </summary>
    public partial class IDV : UserControl
    {
        public IDV()
        {
            InitializeComponent();

            BindingProcess();
        }
        private void BindingProcess()
        {
            this.tv.DataContext = NEXP.MainWindow.datas.independentVariables;
        }
        private void IDV_PageLoaded(object sender, RoutedEventArgs e)
        {

        }
        private void del_item(object sender, RoutedEventArgs e)
        {
            if (tv.SelectedItem != null)
            {
                //Log.getLogInstance().writeLog((tv.Items.IndexOf(tv.SelectedItem)).ToString());

                if (tv.Items.IndexOf(tv.SelectedItem) != -1)
                {
                    NEXP.MainWindow.datas.independentVariables.RemoveAt(tv.Items.IndexOf(tv.SelectedItem));
                    return;
                }
                else
                {
                    NEXP.IndependentVariable.Level selectedLevel = (NEXP.IndependentVariable.Level)tv.SelectedItem;
                    foreach (NEXP.IndependentVariable IDV in NEXP.MainWindow.datas.independentVariables)
                    {
                        if (IDV.levels.Contains(selectedLevel))
                        {
                            //Log.getLogInstance().writeLog("second");
                            IDV.levels.Remove(selectedLevel);
                        }

                    }

                }
            }
        }
        private void add_item(object sender, RoutedEventArgs e)
        {
            if (addItemText.Text != "")
            {
                if (tv.SelectedItem == null)
                {
                    NEXP.IndependentVariable idv = new NEXP.IndependentVariable();
                    idv.name = addItemText.Text;
                    NEXP.MainWindow.datas.independentVariables.Add(idv);
                }
                else if (tv.Items.IndexOf(tv.SelectedItem) != -1)
                {
                    NEXP.IndependentVariable selectedIDV = (NEXP.IndependentVariable)tv.SelectedItem;
                    NEXP.IndependentVariable.Level level = new NEXP.IndependentVariable.Level();
                    level.name = addItemText.Text;
                    selectedIDV.levels.Add(level);
                }
                else
                {
                }
            }
        }
        private void Icon_Add(object sender, RoutedEventArgs e)
        {
            //Log.getLogInstance().writeLog("click");
            if (addItemText.Text != "")
            {
                if (tv.SelectedItem == null)
                {
                    NEXP.IndependentVariable idv = new NEXP.IndependentVariable();
                    idv.name = addItemText.Text;
                    NEXP.MainWindow.datas.independentVariables.Add(idv);
                }
                else if (tv.Items.IndexOf(tv.SelectedItem) != -1)
                {
                    NEXP.IndependentVariable selectedIDV = (NEXP.IndependentVariable)tv.SelectedItem;
                    NEXP.IndependentVariable.Level level = new NEXP.IndependentVariable.Level();
                    level.name = addItemText.Text;
                    selectedIDV.levels.Add(level);
                }
                else
                {
                }
            }
        }
        private void Icon_Del(object sender, RoutedEventArgs e)
        {
            if (tv.SelectedItem != null)
            {
                //Log.getLogInstance().writeLog((tv.Items.IndexOf(tv.SelectedItem)).ToString());

                if (tv.Items.IndexOf(tv.SelectedItem) != -1)
                {
                    NEXP.MainWindow.datas.independentVariables.RemoveAt(tv.Items.IndexOf(tv.SelectedItem));
                    return;
                }
                else
                {
                    NEXP.IndependentVariable.Level selectedLevel = (NEXP.IndependentVariable.Level)tv.SelectedItem;
                    foreach (NEXP.IndependentVariable IDV in NEXP.MainWindow.datas.independentVariables)
                    {
                        if (IDV.levels.Contains(selectedLevel))
                        {
                            //Log.getLogInstance().writeLog("second");
                            IDV.levels.Remove(selectedLevel);
                        }

                    }

                }
            }
        }
    }
}
