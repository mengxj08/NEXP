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
using FirstFloor.ModernUI.Windows.Controls;

namespace NEXP.Pages
{
    /// <summary>
    /// Interaction logic for ResearchQuestion.xaml
    /// </summary>
    public partial class ResearchQuestion : UserControl
    {
        private DateTime downTime;
        private object downSender;
        private Point downPosition;
        private int CurrentPage = 1;

        public ResearchQuestion()
        {
            InitializeComponent();
        }

        private void OpaqueClickableImage_MouseDown_backButton(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.downSender = sender;
                this.downTime = DateTime.Now;
                this.downPosition = e.GetPosition(sender as Image);
            }
        }

        private void OpaqueClickableImage_MouseUp_backButton(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Released && sender == this.downSender)
            {
                TimeSpan timeSinceDown = DateTime.Now - this.downTime;
                if (timeSinceDown.TotalMilliseconds < 500)
                {
                    //MessageBox.Show("Image Click: " + sender.ToString());
                    NEXP.Utils.OpaqueClickableImage tmp = sender as NEXP.Utils.OpaqueClickableImage;

                    if (tmp.Name == "BackButton")
                    {
                        NavigationCommands.GoToPage.Execute("/Pages/Home.xaml", this);   // http://mui.codeplex.com/discussions/434905
                    }
                }
            }
        }

        private void OpaqueClickableImage_MouseDown_NextButton(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.downSender = sender;
                this.downTime = DateTime.Now;
                this.downPosition = e.GetPosition(sender as Image);
            }
        }

        private void OpaqueClickableImage_MouseUp_NextButton(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Released && sender == this.downSender)
            {
                TimeSpan timeSinceDown = DateTime.Now - this.downTime;
                if (timeSinceDown.TotalMilliseconds < 500)
                {
                    //MessageBox.Show("Image Click: " + sender.ToString());
                    NEXP.Utils.OpaqueClickableImage tmp = sender as NEXP.Utils.OpaqueClickableImage;
                    int pageNum = 3;
                    if (tmp.Name == "NextButton")
                    {
                        if (CurrentPage == pageNum)
                        {
                            MessageBoxButton btn = MessageBoxButton.OK;
                            FirstFloor.ModernUI.Windows.Controls.ModernDialog.ShowMessage("Congratulations! You have already finished 1.Research Question, please click the top-left Back to the main window and continue your design.", "Congratulations!", btn);
                        }
                        if (CurrentPage < pageNum) CurrentPage++;
                        switch (CurrentPage)
                        {
                            case 1:
                                NavigationCommands.GoToPage.Execute("/Content/RQ_list1.xaml", Frame);
                                break;
                            case 2:
                                NavigationCommands.GoToPage.Execute("/Content/RQ_list2.xaml", Frame);
                                break;
                            case 3:
                                NavigationCommands.GoToPage.Execute("/Content/RQ_list3.xaml", Frame);
                                break;
                        }
                        //Log.getLogInstance().writeLog(Frame.Content.ToString());
                        //NavigationCommands.GoToPage.Execute("/Pages/Home.xaml", this);   // http://mui.codeplex.com/discussions/434905

                    }
                    else if (tmp.Name == "NewBackButton")
                    {
                        if (CurrentPage > 1) CurrentPage--;
                        switch (CurrentPage)
                        {
                            case 1:
                                NavigationCommands.GoToPage.Execute("/Content/RQ_list1.xaml", Frame);
                                break;
                            case 2:
                                NavigationCommands.GoToPage.Execute("/Content/RQ_list2.xaml", Frame);
                                break;
                            case 3:
                                NavigationCommands.GoToPage.Execute("/Content/RQ_list3.xaml", Frame);
                                break;
                        }
                    }

                }
            }
        }
    }
}
