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

namespace NEXP.Pages
{
    /// <summary>
    /// Interaction logic for BlockAndTrial.xaml
    /// </summary>
    public partial class BlockAndTrial : UserControl
    {
        private DateTime downTime;
        private object downSender;
        private Point downPosition;
        private int CurrentPage = 1;
        public BlockAndTrial()
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
                    int pageNum = 4;
                    int EstimatePage = 3;
                    if (tmp.Name == "NextButton")
                    {
                        if (CurrentPage == pageNum)
                        {
                            MessageBoxButton btn = MessageBoxButton.OK;
                            FirstFloor.ModernUI.Windows.Controls.ModernDialog.ShowMessage("Congratulations! You have already finished 4. Decide blocks & trials, please click the top-left Back to the main window and continue your design.", "Congratulations!", btn);
                            return;
                        }

                        if(CurrentPage == EstimatePage)
                        {
                            if (! Control.getControlInstance().IsValidEstimate())
                            {
                                MessageBoxButton btn = MessageBoxButton.OK;
                                FirstFloor.ModernUI.Windows.Controls.ModernDialog.ShowMessage("Sorry,our tool automatecially detected that your experimental arrangment is not reasonable. Please check your Variables/Strategy/Block/Trial/ to continue.", "Error Message!", btn);
                                return;
                            }
                        }
                        if (CurrentPage < pageNum) CurrentPage++;
                        switch (CurrentPage)
                        {
                            case 1:
                                NavigationCommands.GoToPage.Execute("/Content/blockTrialTutorial.xaml", Frame);
                                break;
                            case 2:
                                NavigationCommands.GoToPage.Execute("/Content/Balance.xaml", Frame);
                                break;
                            case 3:
                                NavigationCommands.GoToPage.Execute("/Content/Estimate.xaml", Frame);
                                break;
                            case 4:
                                NavigationCommands.GoToPage.Execute("/Content/Visualization.xaml", Frame);
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
                                NavigationCommands.GoToPage.Execute("/Content/blockTrialTutorial.xaml", Frame);
                                break;
                            case 2:
                                NavigationCommands.GoToPage.Execute("/Content/Balance.xaml", Frame);
                                break;
                            case 3:
                                NavigationCommands.GoToPage.Execute("/Content/Estimate.xaml", Frame);
                                break;
                            case 4:
                                NavigationCommands.GoToPage.Execute("/Content/Visualization.xaml", Frame);
                                break;
                        }
                    }
                }
            }
        }
    }
}
