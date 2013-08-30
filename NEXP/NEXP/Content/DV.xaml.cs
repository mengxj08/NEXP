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
    /// Interaction logic for DV.xaml
    /// </summary>
    public partial class DV : UserControl
    {
        public DV()
        {
            InitializeComponent();

            BindingProcess();
        }
        private void BindingProcess()
        {
            tv.DataContext = NEXP.MainWindow.datas.dependentVariables;
        }
        private void add_item(object sender, RoutedEventArgs e)
        {
            if (addItemText.Text != "")
            {
                NEXP.DependentVariable dv = new NEXP.DependentVariable();
                dv.name = addItemText.Text;
                NEXP.MainWindow.datas.dependentVariables.Add(dv);
            }
            else 
            {
                MessageBoxButton btn = MessageBoxButton.OK;
                FirstFloor.ModernUI.Windows.Controls.ModernDialog.ShowMessage("You should fill in the Input TextArea below before adding!", "Error Message", btn);
            }
            return;
        }
        private void del_item(object sender, RoutedEventArgs e)
        {
            if (tv.SelectedItem != null)
            {
                NEXP.DependentVariable dv = (NEXP.DependentVariable)tv.SelectedItem;
                NEXP.MainWindow.datas.dependentVariables.Remove(dv);
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
                NEXP.MainWindow.datas.dependentVariables.RemoveAt(tv.Items.IndexOf(tv.SelectedItem));
                return;
            }
        }
    }
}
