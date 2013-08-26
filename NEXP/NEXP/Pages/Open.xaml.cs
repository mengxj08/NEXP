using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Peers;
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
    /// Interaction logic for Open.xaml
    /// </summary>
    public partial class Open : System.Windows.Controls.UserControl
    {
        private OpenFileDialog dialog;
        private bool firstLoad = true;

        public Open()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            dialog = new OpenFileDialog();

            dialog.Filter = "XML Files (.xml)|*.xml";
            dialog.RestoreDirectory = true;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                //NewDesignTool.MainWindow.datas = new DataStructure();
                NEXP.MainWindow.datas.ReadFromFile(dialog.FileName);

                //NewDesignTool.Pages.IDV.IDV_PAGE_FLAG = true;
                //NewDesignTool.Pages.DV.DV_PAGE_FLAG = true;
                //NewDesignTool.Pages.ArrangeConditions.AC_PAGE_FLAG = true;
                //NewDesignTool.Pages.RQ_list1.RQL1_PAGE_FLAG = true;
                //NewDesignTool.Pages.RQ_list2.RQL2_PAGE_FLAG = true;
                //NewDesignTool.Pages.RQ_list3.RQL3_PAGE_FLAG = true;
                //NewDesignTool.Pages.Estimate.ESTIMATE_PAGE_FLAG = true;
            }
        }

        private void OpenWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //if (firstLoad)
            //{
            //    firstLoad = false;
            //    //ButtonAutomationPeer peer = new ButtonAutomationPeer(OpenButton);
            //    //IInvokeProvider invokeProv = peer.GetPattern(PatternInterface.Invoke) as 
            //    OpenButton.RaiseEvent(new RoutedEventArgs(System.Windows.Controls.Button.ClickEvent));
            //}
            //else
            //{
            //    firstLoad = true;
            //}
        }
    }
}
