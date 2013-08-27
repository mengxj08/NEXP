using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
    /// Interaction logic for Visualization.xaml
    /// </summary>
    public partial class Visualization : UserControl
    {
        public Visualization()
        {
            InitializeComponent();

            //string uri = "http://localhost:8888/Simulation.html";
            //this.browser.Navigate(new Uri(uri, UriKind.Absolute));

            // Turn on local server here, and turn off in the Finalize().
        }

        // Click the button to generate data to .json file, and transfer to web browser to show the simulation result.
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Utils.GenerateSimulation simulation = new Utils.GenerateSimulation();
            simulation.ShowSimulation();
        }

    }
}
