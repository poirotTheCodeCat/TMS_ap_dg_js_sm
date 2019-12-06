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
using System.IO;
using Microsoft.Win32;

namespace TMS_ap_dg_js_sm
{
    /// <summary>
    /// Interaction logic for LogfilePage.xaml
    /// </summary>
    public partial class LogfilePage : Page
    {
        public LogfilePage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// This method opens a dialogue box that allows the user to select a text file from the computer, then loads the 
        /// text that is read in the file to the viewpanel on the page. This should be used to retrieve Log Files
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();       // opens dialog box that asks user to select a file
            dialog.Filter = "Text files (*.txt)|*.txt";         // this filter only allows text files to be open

            if(dialog.ShowDialog() == true)
            {
                string line = "";
                int counter = 0;

                StreamReader reader = new StreamReader(dialog.FileName);
                while ((line = reader.ReadLine()) != null)
                {
                    LogfileDisplay.Items.Add(line);
                    counter++;
                }
                reader.Close();
            }
        }
    }
}
