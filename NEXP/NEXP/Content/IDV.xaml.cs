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
        private DateTime downTime;
        private object downSender;
        private Point downPosition;
        
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
            Control.getControlInstance().RQContributeToVariable();
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
            if (addItemText.Text == "")
            {
                MessageBoxButton btn = MessageBoxButton.OK;
                FirstFloor.ModernUI.Windows.Controls.ModernDialog.ShowMessage("You should fill in the Input TextArea below before adding!", "Error Message", btn);
                return;
            }
            //if (addItemText.Text != "")
            //{
                //if (tv.SelectedItem == null)
                //{
                NEXP.IndependentVariable idv = new NEXP.IndependentVariable();
                idv.name = addItemText.Text;
                NEXP.MainWindow.datas.independentVariables.Add(idv);
                //}
                /*
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
                 * */
            //}
        }
        private void Icon_Add_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.downSender = sender;
                this.downTime = DateTime.Now;
                this.downPosition = e.GetPosition(sender as Canvas);
            }
        }
        private void Icon_Add_MouseUp(object sender, MouseButtonEventArgs e)
        {
            //Log.getLogInstance().writeLog("click");
            if (e.LeftButton == MouseButtonState.Released && sender == this.downSender)
            {
                TimeSpan timeSinceDown = DateTime.Now - this.downTime;
                if (timeSinceDown.TotalMilliseconds < 500)
                {
                    System.Windows.Controls.Canvas tmp = sender as System.Windows.Controls.Canvas;
                    if (tmp.Name == "Add")
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
                    else if (tmp.Name == "Minus")
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
                    else
                    {
                    }
                }
            }
        }
        private void Item_Add(object sender, RoutedEventArgs e)
        {
            if (addItemText.Text == "")
            {
                MessageBoxButton btn = MessageBoxButton.OK;
                FirstFloor.ModernUI.Windows.Controls.ModernDialog.ShowMessage("You should fill in the Input TextArea below before adding!", "Error Message", btn);
                return;
            }
            TreeViewItem currentTreeViewItem = null;

            //Find the original Source and set it as the currentTreeViewItem
            FrameworkElement element = e.OriginalSource as FrameworkElement;
            if (element != null)
            {
                while (VisualTreeHelper.GetParent(element) != null)
                {
                    element = VisualTreeHelper.GetParent(element) as FrameworkElement;
                    TreeViewItem item = element as TreeViewItem;
                    if (item != null)
                    {
                        // Perform custom logic here
                        // You have to return here because otherwise the method will traverse
                        // the whole visual tree on every mouse move and there will be performance
                        // implications
                        currentTreeViewItem = item;
                        break;
                    }
                }
            }

            currentTreeViewItem.IsSelected = true;//Mark the current TreeViewItem as the selectedItem
            if (tv.Items.IndexOf(tv.SelectedItem) != -1)
            {
                NEXP.IndependentVariable selectedIDV = (NEXP.IndependentVariable)tv.SelectedItem;
                NEXP.IndependentVariable.Level level = new NEXP.IndependentVariable.Level();
                level.name = addItemText.Text;
                selectedIDV.levels.Add(level);
                currentTreeViewItem.IsExpanded = true;
            }
            else
            {
                NEXP.IndependentVariable.Level selectedLevel = (NEXP.IndependentVariable.Level)tv.SelectedItem;
                NEXP.IndependentVariable.Level level = new NEXP.IndependentVariable.Level();
                level.name = addItemText.Text;
                foreach (NEXP.IndependentVariable IDV in NEXP.MainWindow.datas.independentVariables)
                {
                    if (IDV.levels.Contains(selectedLevel))
                    {
                        //Log.getLogInstance().writeLog("second");
                        IDV.levels.Add(level);
                        break;
                    }
                }
                currentTreeViewItem.IsExpanded = true;
            }

        }
        private void Item_Del(object sender, RoutedEventArgs e)
        {
            TreeViewItem currentTreeViewItem = null;

            //Find the original Source and set it as the currentTreeViewItem
            FrameworkElement element = e.OriginalSource as FrameworkElement;
            if (element != null)
            {
                while (VisualTreeHelper.GetParent(element) != null)
                {
                    element = VisualTreeHelper.GetParent(element) as FrameworkElement;
                    TreeViewItem item = element as TreeViewItem;
                    if (item != null)
                    {
                        // Perform custom logic here
                        // You have to return here because otherwise the method will traverse
                        // the whole visual tree on every mouse move and there will be performance
                        // implications
                        currentTreeViewItem = item;
                        break;
                    }
                }
            }
            
            currentTreeViewItem.IsSelected = true;//Mark the current TreeViewItem as the selectedItem
            if (tv.Items.IndexOf(tv.SelectedItem) != -1)
            {

                NEXP.MainWindow.datas.independentVariables.RemoveAt(tv.Items.IndexOf(tv.SelectedItem));
                return;
            }
            else
            {
                NEXP.IndependentVariable.Level tmp = (NEXP.IndependentVariable.Level)tv.SelectedItem;
                foreach (NEXP.IndependentVariable IDV in NEXP.MainWindow.datas.independentVariables)
                {
                    if (IDV.levels.Contains(tmp))
                    {
                        //Log.getLogInstance().writeLog("second");
                        IDV.levels.Remove(tmp);
                    }

                }

            }
        }
    }
}
