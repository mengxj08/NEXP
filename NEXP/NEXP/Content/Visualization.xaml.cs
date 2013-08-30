using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
        private Process cmdProcess;

        public Visualization()
        {
            InitializeComponent();

            // Turn on local server here, and turn off in the Finalize().
            cmdProcess = new Process();
            cmdProcess.StartInfo.WorkingDirectory = @Directory.GetCurrentDirectory();
            cmdProcess.StartInfo.FileName = "cmd.exe";
            cmdProcess.StartInfo.Arguments = "/k python -m SimpleHTTPServer 8888";
            cmdProcess.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
            cmdProcess.Start();

        }

        // Click the button to generate data to .json file, and transfer to web browser to show the simulation result.
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            Utils.GenerateSimulation simulation = new Utils.GenerateSimulation();
            simulation.ShowSimulation();

            var url = "http://localhost:8888/Simulation.html";
            using (var process = new Process())
            {
                process.StartInfo.FileName = @"chrome.exe";
                process.StartInfo.Arguments = url + " --incognito";
                process.Start();
            }
        }

        ~Visualization()
        {
            cmdProcess.CloseMainWindow();
            cmdProcess.Close();
        }
    }
}
