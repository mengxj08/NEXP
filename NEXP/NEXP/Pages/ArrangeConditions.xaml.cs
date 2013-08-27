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
    /// Interaction logic for ArrangeConditions.xaml
    /// </summary>
    public partial class ArrangeConditions : UserControl
    {
        private DateTime downTime;
        private object downSender;
        private Point downPosition;
        private int content = 1;
        public ArrangeConditions()
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

                    if (tmp.Name == "NextButton")
                    {
                        //Log.getLogInstance().writeLog(Frame.Content.ToString());
                        /*
                         if (Frame.Content.ToString() == "/Content/RQ_list2.xaml")
                         {
                             NavigationCommands.GoToPage.Execute("/Content/RQ_list1.xaml", Frame);
                         }
                         */
                        content ++;
                        content = content  % 4;
                        if (content == 0)
                        {
                            NavigationCommands.GoToPage.Execute("/Content/Conditions.xaml", Frame);
                        }
                        else if (content == 1)
                        {
                            NavigationCommands.GoToPage.Execute("/Content/ConditionsTutorials.xaml", Frame);
                        }
                        else if (content == 2)
                        {
                            NavigationCommands.GoToPage.Execute("/Content/MethodTutorials1.xaml", Frame);
                        }
                        else if (content == 3)
                        {
                            NavigationCommands.GoToPage.Execute("/Content/MethodTutorials2.xaml", Frame);
                        }
                        else { }
                        //Log.getLogInstance().writeLog(Frame.Content.ToString());
                        //NavigationCommands.GoToPage.Execute("/Pages/Home.xaml", this);   // http://mui.codeplex.com/discussions/434905
                    }
                }
            }
        }
    }
}
