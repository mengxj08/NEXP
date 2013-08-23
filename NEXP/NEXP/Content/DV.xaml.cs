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
        }
        private void del_item(object sender, RoutedEventArgs e)
        {
            if (tv.SelectedItem != null)
            {
                NEXP.DependentVariable dv = (NEXP.DependentVariable)tv.SelectedItem;
                NEXP.MainWindow.datas.dependentVariables.Remove(dv);
            }
        }
    }
}
