using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NEXP.Pages
{
    /// <summary>
    /// Interaction logic for Save.xaml
    /// </summary>
    public partial class Save : System.Windows.Controls.UserControl
    {
        private SaveFileDialog dialog;

        public Save()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            dialog = new SaveFileDialog();
            dialog.FileName = "NewStudy";
            dialog.DefaultExt = ".xml";
            dialog.Filter = "Xml documents (.xml)|*.xml";
            dialog.ValidateNames = true;
            

            dialog.FileOk += dialog_FileOk;

            var result = dialog.ShowDialog();

        }

        void dialog_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //Log.getLogInstance().writeLog("*" + dialog.FileName + "*");
            //e.Cancel = true;
            //string fileName = dialog.FileName;
            //if (String.IsNullOrEmpty(fileName))
            //{
            //    System.Windows.Forms.MessageBox.Show("Make sure the file name is valid.");
            //}
            //else
            //{
            //    System.Windows.Forms.MessageBox.Show("Name is valid");
            //}
            NEXP.MainWindow.datas.WriteToXml(dialog.FileName);
        }
    }
}
