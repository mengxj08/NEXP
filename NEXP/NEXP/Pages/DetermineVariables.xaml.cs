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
    /// Interaction logic for DetermineVariables.xaml
    /// </summary>
    public partial class DetermineVariables : UserControl
    {
        private DateTime downTime;
        private object downSender;
        private Point downPosition;
        private int CurrentPage = 1;

        public DetermineVariables()
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
                    int pageNum = 2;
                    if (tmp.Name == "NextButton")
                    {
                        if (CurrentPage < pageNum) CurrentPage++;
                        switch (CurrentPage)
                        {
                            case 1:
                                NavigationCommands.GoToPage.Execute("/content/IDV.xaml", Frame);
                                break;
                            case 2:
                                NavigationCommands.GoToPage.Execute("/content/DV.xaml", Frame);
                                break;       
                        }
                        //Log.getLogInstance().writeLog(Frame.Content.ToString());
                        //NavigationCommands.GoToPage.Execute("/Pages/Home.xaml", this);   // http://mui.codeplex.com/discussions/434905

                    }
                    else if (tmp.Name == "BackButton")
                    {
                        if (CurrentPage > 1) CurrentPage--;
                        switch (CurrentPage)
                        {
                            case 1:
                                NavigationCommands.GoToPage.Execute("/content/IDV.xaml", Frame);
                                break;
                            case 2:
                                NavigationCommands.GoToPage.Execute("/content/DV.xaml", Frame);
                                break;
                        }
                    }
                }
            }
        }
    }
}
