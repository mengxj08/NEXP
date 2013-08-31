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
            contexts.DataContext = NEXP.MainWindow.datas.researchQuestion.hypothesis;
        }
        private void Item_Add(object sender, RoutedEventArgs e)
        {
            if (Addtext.Text == "")
            {
                //System.Windows.MessageBox.Show("error");
                MessageBoxButton btn = MessageBoxButton.OK;
                FirstFloor.ModernUI.Windows.Controls.ModernDialog.ShowMessage("You should fill in the Input TextArea below before adding!","Error Message",btn);
                return;
            }
            FirstFloor.ModernUI.Windows.Controls.ModernButton tmp = sender as FirstFloor.ModernUI.Windows.Controls.ModernButton;

            if (tmp.Name == "Add_compareSolutions")
            {
                NEXP.MainWindow.datas.researchQuestion.hypothesis.compareSolutions.Add(Addtext.Text);
            }
            else if (tmp.Name == "Add_tasks")
            {
                NEXP.MainWindow.datas.researchQuestion.hypothesis.tasks.Add(Addtext.Text);
            }
            else if (tmp.Name == "Add_measures")
            {
                NEXP.MainWindow.datas.researchQuestion.hypothesis.measures.Add(Addtext.Text);
            }
            else if (tmp.Name == "Add_contexts")
            {
                NEXP.MainWindow.datas.researchQuestion.hypothesis.contexts.Add(Addtext.Text);
            }
            else 
            { }
        }
        private void Item_Del(object sender, RoutedEventArgs e)
        {
            FirstFloor.ModernUI.Windows.Controls.ModernButton tmp = sender as FirstFloor.ModernUI.Windows.Controls.ModernButton;

            if (tmp.Name == "Del_compareSolutions")
            {
                if (compareSolutions.SelectedItem != null)
                {
                    NEXP.MainWindow.datas.researchQuestion.hypothesis.compareSolutions.RemoveAt(compareSolutions.Items.IndexOf(compareSolutions.SelectedItem));
                    compareSolutions.SelectedIndex = 0;
                }
                else
                    showErrorMessage();
            }
            else if (tmp.Name == "Del_tasks")
            {
                if (tasks.SelectedItem != null)
                {
                    NEXP.MainWindow.datas.researchQuestion.hypothesis.tasks.RemoveAt(tasks.Items.IndexOf(tasks.SelectedItem));
                    tasks.SelectedIndex = 0;
                }
                else
                    showErrorMessage();
            }
            else if (tmp.Name == "Del_measures")
            {
                if (measures.SelectedItem != null)
                {
                    NEXP.MainWindow.datas.researchQuestion.hypothesis.measures.RemoveAt(measures.Items.IndexOf(measures.SelectedItem));
                    measures.SelectedIndex = 0;
                }
                else
                    showErrorMessage();
            }
            else if (tmp.Name == "Del_contexts")
            {
                if (contexts.SelectedItem != null)
                {
                    NEXP.MainWindow.datas.researchQuestion.hypothesis.contexts.RemoveAt(contexts.Items.IndexOf(contexts.SelectedItem));
                    contexts.SelectedIndex = 0;
                }
                else
                    showErrorMessage();
            }
            else
            { }
        }
        private void showErrorMessage()
        {
            MessageBoxButton btn = MessageBoxButton.OK;
            FirstFloor.ModernUI.Windows.Controls.ModernDialog.ShowMessage("You should choose the item you want to delete!", "Error Message", btn);
            return;
        }
    }
}
