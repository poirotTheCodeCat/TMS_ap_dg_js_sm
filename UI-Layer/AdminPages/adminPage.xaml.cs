using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using Google.Protobuf.WellKnownTypes;
using TMS.Business_Layer.users;
using System.Windows.Forms;
using MessageBox = System.Windows.MessageBox;
using OpenFileDialog = System.Windows.Forms.OpenFileDialog;

namespace TMS
{
    /// <summary>
    /// Interaction logic for adminPage.xaml
    /// </summary>
    public partial class AdminPage : Page
    {
        List<Carrier> carrierList = new List<Carrier>();
        List<TransportCorridor> routeList = new List<TransportCorridor>();
        List<double> rateMarkup = new List<double>();
        internal class TypeAndRate
        {
            public string truckType { get; set; }
            public double markup { get; set; }
        }
        List<TypeAndRate> rateList = new List<TypeAndRate>();

        Admin admin = new Admin();
        bool trigger;
        public AdminPage()
        {
            trigger = false;
            InitializeComponent();

            trigger = true;
            LoadIpPortInfo();
            //UpdateIpPortInfo("tms-historybuff.mysql.database.azure.com", "3306");

            UpdateDataTables();

        }

        private void UpdateDataTables()
        {
            try
            {
                FillCarrierList();
                FillRateList();
                FillRouteList();
            }
            catch (Exception e)
            {
                Logger.Log("TMS database connection error\n" + e);
                MessageBox.Show("Unable to connect IP or Port information");
            }
        }

        /// <summary>
        /// This method will load the current IP and Port information as stored in the App.config file.
        /// </summary>
        private void LoadIpPortInfo()
        {
            IpInfoDisplay.Text = ConfigurationManager.AppSettings["ipInfo"];
            PortInfoDisplay.Text = ConfigurationManager.AppSettings["portInfo"];
        }

        /// <summary>
        /// This method will update IP and Port information as stored in the App.config file.
        /// </summary>
        /// <param name="ip">The new IP</param>
        /// <param name="port">The new Port</param>
        private void UpdateIpPortInfo(string ip, string port)
        {
            Configuration config = 
                ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            if (ip != "")
            {
                config.AppSettings.Settings["ipInfo"].Value = ip;
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");
                IpInfoDisplay.Text = ip; 
                // ConfigurationManager.AppSettings["ipInfo"] = ip;
            }

            if (port != "")
            {
                config.AppSettings.Settings["portInfo"].Value = port;
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");
                PortInfoDisplay.Text = port; 
                //ConfigurationManager.AppSettings["portInfo"] = port;
            }
        }

        /// <summary>
        /// This method get the values held in the update ip and port text boxes and call the update method.
        /// </summary>
        private void ConfigUpdateBtn_OnClick(object sender, RoutedEventArgs e)
        {
            UpdateIpPortInfo(IpInfoUpdate.Text, PortInfoUpdate.Text);
            IpPortConfigGrid.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// This method allows the user to select a file path for where to save the backup of the local
        /// tms database.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GetFilePathBtn_OnClick(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog oFD = new FolderBrowserDialog();
            if (oFD.ShowDialog() != DialogResult.OK) return; 
            BackupFilePathtxb.Text = oFD.SelectedPath;
        }

        /// <summary>
        /// This method will allow the user to select a save location for the database backup.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackupBtn_Click(object sender, RoutedEventArgs e)
        {
            string filePath = BackupFilePathtxb.Text.Trim() + "\\TMSLocalBackup.sql";

            admin.GetBackUp(filePath);

            MessageBox.Show("The local TMS database has been backed up to:\n" + filePath);
            //remove the popup from screen
            BackupDatabaseGrid.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// This handler will make the backup popup grid visible.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackupTmsDatabaseBtn_Click(object sender, RoutedEventArgs e)
        {
            if (BackupDatabaseGrid.Visibility == Visibility.Collapsed)
            {
                BackupDatabaseGrid.Visibility = Visibility.Visible;
            }
            else
            {
                BackupDatabaseGrid.Visibility = Visibility.Collapsed;
            }
        }

        /// <summary>
        /// This handler will collapse the backup database popup.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackupBackBtn_Click(object sender, RoutedEventArgs e)
        {
            BackupDatabaseGrid.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// This handler will display the IP and Port popup window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConfigIpPortBtn_Click(object sender, RoutedEventArgs e)
        {
            if (IpPortConfigGrid.Visibility == Visibility.Collapsed)
            {
                IpPortConfigGrid.Visibility = Visibility.Visible;
            }
            else
            {
                IpPortConfigGrid.Visibility = Visibility.Collapsed;
            }
        }

        /// <summary>
        /// This handler will hind the database display grid showing the log file box as well as print the log file
        /// to the screen for review.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LogFilesBtn_Click(object sender, RoutedEventArgs e)
        {
            //Logger.Log("test log");
            LogFileDisplay.Document.Blocks.Clear();
            if (DataGridFullGrid.Visibility == Visibility.Visible)
            {
                DataGridFullGrid.Visibility = Visibility.Collapsed;
                LogViewGrid.Visibility = Visibility.Visible;
            }
            else
            {
                DataGridFullGrid.Visibility = Visibility.Visible;
                LogViewGrid.Visibility = Visibility.Collapsed;
            }
            
            //if logfile grid currently hidden then do not attempt to open it
            if(LogViewGrid.Visibility == Visibility.Collapsed) return;
            
            //generate file name
            string fileName = ConfigurationManager.AppSettings["logLocation"];

            if (fileName == "~TmsLog.txt")
            {
                fileName = AppDomain.CurrentDomain.BaseDirectory + "\\TMSLog.txt";
            }
            //fill ui box
            LogFileLocationDisplay.Text = fileName;
            try
            {
                StreamReader file = new StreamReader(fileName);
                string line;

                while ((line = file.ReadLine()) != null)
                {
                    LogFileDisplay.AppendText(line + "\n");
                }
                file.Close();
            }
            catch
            {
                MessageBox.Show("There are no current log file available in this location. Change location or try again later.");
            }
        }

        private void ChangeLogLocation_OnClick(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog oFD = new FolderBrowserDialog();
            if (oFD.ShowDialog() != DialogResult.OK) return;
            LogFileLocationDisplay.Text = oFD.SelectedPath;

            UpdateLogLocation(LogFileLocationDisplay.Text.Trim()+"\\TmsLog.txt");
        }

        private void UpdateLogLocation(string logLocal)
        {
            Configuration config =
                ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            config.AppSettings.Settings["logLocation"].Value = logLocal;
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
            LogFileLocationDisplay.Text = ConfigurationManager.AppSettings["logLocation"];
        }

        /// <summary>
        /// This handler will change visibility of the three different datagrids that are available on this screen for viewing
        /// and editing.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DBTablesCmb_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!trigger) return;
            trigger = false;
            CarrierTableDisplay.Items.Clear();
            trigger = true;

            UpdateDataTables();
            var clickedItem = (System.Windows.Controls.ComboBox)sender;
            var index = clickedItem.SelectedIndex;

            if (index == 0)
            {
                RouteTableDisplay.Visibility = Visibility.Collapsed;
                RateTableDisplay.Visibility = Visibility.Collapsed;
                CarrierTableDisplay.Visibility = Visibility.Visible;
            }
            else if (index == 1)
            {
                RateTableDisplay.Visibility = Visibility.Visible;
                RouteTableDisplay.Visibility = Visibility.Collapsed;
                CarrierTableDisplay.Visibility = Visibility.Collapsed;

            }
            else if (index == 2)
            {
                CarrierTableDisplay.Visibility = Visibility.Collapsed;
                RateTableDisplay.Visibility = Visibility.Collapsed;
                RouteTableDisplay.Visibility = Visibility.Visible;

            }
        }


        /// <summary>
        /// Fills a local copy of the carrier table as found in the local database.
        /// </summary>
        private void FillCarrierList()
        {
            carrierList = admin.GetCarriers();
            FillCarrierGrid();
        }

        /// <summary>
        /// Fills a local copy of the route table as found in the local database.
        /// </summary>
        private void FillRouteList()
        {
            routeList = admin.GetRoutes();
            FillRouteGrid();
        }

        /// <summary>
        /// Fills a local copy of the rate table as found in the local database.
        /// </summary>
        private void FillRateList()
        {
            rateMarkup = admin.GetRates();
            foreach(var r in rateMarkup)
            {
                rateList.Add(new TypeAndRate());
            }

            for (int i = 0; i <= rateList.Count; i++)
            {
                if (i == 0)
                {
                    rateList[i].truckType = "FTL";
                    rateList[i].markup = rateMarkup[i];
                }
                else if (i == 1)
                {
                    rateList[i].truckType = "LTL";
                    rateList[i].markup = rateMarkup[i];
                }
                else if (i == 2)
                {
                    rateList[i].truckType = "REEF"; 
                    rateList[i].markup = rateMarkup[i];
                }
            }
            FillRateGrid();
        }

        /// <summary>
        /// Fills the carrier datagrid with carrier information.
        /// </summary>
        private void FillCarrierGrid()
        {
            foreach (Carrier c in carrierList)
            {
                CarrierTableDisplay.Items.Add(c);
            }
        }

        /// <summary>
        /// Fills the route datagrid with route information.
        /// </summary>
        private void FillRouteGrid()
        {
            foreach(var s in carrierList)
            {
                var temp = "";
                foreach (var d in s.DepotCities)
                {
                    temp += d+", ";
                }
                s.DepotCityString = temp;
            }

            foreach (TransportCorridor c in routeList)
            {
                RouteTableDisplay.Items.Add(c);
            }
        }

        /// <summary>
        /// Fills the rate datagrid with markup information.
        /// </summary>
        private void FillRateGrid()
        {

            foreach (TypeAndRate c in rateList)
            {
                RateTableDisplay.Items.Add(c);
            }
        }

        private void ConfigBackBtn_OnClick(object sender, RoutedEventArgs e)
        {
            IpPortConfigGrid.Visibility = Visibility.Collapsed;
        }

        private void ApplyRouteChangesBtn_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}




