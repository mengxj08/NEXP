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
    /// Interaction logic for RQ_list3.xaml
    /// </summary>
    public partial class RQ_list3 : UserControl
    {
        public RQ_list3()
        {
            InitializeComponent();

            BindingProcess();
        }
        private void BindingProcess()
        {
            compareSolutions.DataContext = NEXP.MainWindow.datas.researchQuestion.hypothesis;
            measures.DataContext = NEXP.MainWindow.datas.researchQuestion.hypothesis;
            tasks.DataContext = NEXP.MainWindow.datas.researchQuestion.hypothesis;
            mainSolution.DataContext = NEXP.MainWindow.datas.researchQuestion.hypothesis;
            context.DataContext = NEXP.MainWindow.datas.researchQuestion.hypothesis;
        }
        private void Item_Add(object sender, RoutedEventArgs e)
        {
            if (Addtext.Text == "") return;
            if (checkbox1.IsChecked == true)
            {
                NEXP.MainWindow.datas.researchQuestion.hypothesis.compareSolutions.Add(Addtext.Text);
            }
            else if (checkbox2.IsChecked == true)
            {
                NEXP.MainWindow.datas.researchQuestion.hypothesis.tasks.Add(Addtext.Text);
            }
            else if (checkbox3.IsChecked == true)
            {
                NEXP.MainWindow.datas.researchQuestion.hypothesis.measures.Add(Addtext.Text);
            }
            else
            { }

        }
        private void Item_Del(object sender, RoutedEventArgs e)
        {
            if (checkbox1.IsChecked == true)
            {
                if (compareSolutions.SelectedItem != null)
                    NEXP.MainWindow.datas.researchQuestion.hypothesis.compareSolutions.RemoveAt(compareSolutions.Items.IndexOf(compareSolutions.SelectedItem));
            }
            else if (checkbox2.IsChecked == true)
            {
                if (tasks.SelectedItem != null)
                    NEXP.MainWindow.datas.researchQuestion.hypothesis.tasks.RemoveAt(tasks.Items.IndexOf(tasks.SelectedItem));
            }
            else if (checkbox3.IsChecked == true)
            {
                if (measures.SelectedItem != null)
                    NEXP.MainWindow.datas.researchQuestion.hypothesis.measures.RemoveAt(measures.Items.IndexOf(measures.SelectedItem));
            }
            else
            { }
        }
        private void checkbox1_checked(object sender, RoutedEventArgs e)
        {
            checkbox2.IsChecked = false;
            checkbox3.IsChecked = false;
        }
        private void checkbox2_checked(object sender, RoutedEventArgs e)
        {
            checkbox1.IsChecked = false;
            checkbox3.IsChecked = false;
        }
        private void checkbox3_checked(object sender, RoutedEventArgs e)
        {
            checkbox1.IsChecked = false;
            checkbox2.IsChecked = false;
        }
    }
}
