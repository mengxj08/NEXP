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
    }
}
