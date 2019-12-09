/*
 * File Name: AdminPage.xaml.cs
 * Program Name: TMS_ap_dg_js_sm
 * Programmers: Arron Perry, Daniel Grew, John Stanley, Sasha Malesevic
 * First Version: 2019-12-09
 */
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
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

            UpdateDataTables();

        }

        /// <summary>
        /// This method will update all datagrids used on the admin page. Log and display errors.
        /// </summary>
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
            if (!IsAllDigits(port) || !IsAllDigits(ip))
            {
                MessageBox.Show("No letters allowed in the IP or PORT."); 
                return;
            }
            if (ip == "DEFAULT")
            {
                ip = "159.89.117.198";
            }
            if (port == "DEFAULT")
            {
                port = "3306";
            }

            Configuration config = 
                ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            if (ip != "")
            {
                ConnectionStringSettings ipI = new ConnectionStringSettings("ipInfo", ip);

                config.AppSettings.Settings["ipInfo"].Value = ip;
                config.ConnectionStrings.ConnectionStrings.Remove("ipInfo");
                config.ConnectionStrings.ConnectionStrings.Add(ipI);
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");
                IpInfoDisplay.Text = ip;

                Logger.Log("IP Changed");
                // ConfigurationManager.AppSettings["ipInfo"] = ip;
            }

            if (port != "")
            {
                ConnectionStringSettings portI = new ConnectionStringSettings("ipInfo", port);
                config.AppSettings.Settings["portInfo"].Value = port;
                config.ConnectionStrings.ConnectionStrings.Remove("portInfo");
                //config.ConnectionStrings.ConnectionStrings.Add(portI);
                config.Save(ConfigurationSaveMode.Modified);
            
                ConfigurationManager.RefreshSection("appSettings");
                PortInfoDisplay.Text = port;

                Logger.Log("Port Changed");
                //ConfigurationManager.AppSettings["portInfo"] = port;
            }
        }

        /// <summary>
        /// This method checks if a string is only digits or decimals.
        /// </summary>
        /// <param name="s"></param>
        /// <returns>Bool to tell if only digits.</returns>
        private bool IsAllDigits(string s)
        {
            foreach (var c in s)
            {
                if (!char.IsDigit(c) && c != '.') return false;
            }

            return true;
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

        /// <summary>
        /// This method will update the location of where the log file is stored, on update it will attempt
        /// to print what is contained in the file at that location. If the user enters DEFAULT into the log location
        /// box the store location will return to where the exe was launched from, else it will allow the user
        /// to select a file location with a modal folder browser.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChangeLogLocation_OnClick(object sender, RoutedEventArgs e)
        {
            string filePath;

            if (LogFileLocationDisplay.Text.Trim() == "DEFAULT")
            {
                filePath = AppDomain.CurrentDomain.BaseDirectory + "\\TmsLog.txt";
                LogFileLocationDisplay.Text = filePath;
            }
            else
            {
                FolderBrowserDialog oFD = new FolderBrowserDialog();
                if (oFD.ShowDialog() != DialogResult.OK) return;
                LogFileLocationDisplay.Text = oFD.SelectedPath;
                filePath = LogFileLocationDisplay.Text.Trim() + "\\TmsLog.txt";
            }
            UpdateLogLocation(filePath);
            Logger.Log("Log file location changed to: " + filePath);

            //then refresh the RTB
            try
            {
                LogFileDisplay.SelectAll();
                LogFileDisplay.Selection.Text = "";

                StreamReader file = new StreamReader(filePath);
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

        /// <summary>
        /// This method will update the log store location value held in the App.Config file.
        /// </summary>
        /// <param name="logLocal"></param>
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
            if (!trigger) return;

            carrierList = admin.GetCarriers();
            FillCarrierGrid();
        }

        /// <summary>
        /// Fills a local copy of the route table as found in the local database.
        /// </summary>
        private void FillRouteList()
        {
            if (!trigger) return;

            routeList = admin.GetRoutes();
            FillRouteGrid();
        }

        /// <summary>
        /// Fills a local copy of the rate table as found in the local database.
        /// </summary>
        private void FillRateList()
        {
            if (!trigger) return;

            rateMarkup = admin.GetRates();

            rateList.Add(new TypeAndRate());
            rateList.Add(new TypeAndRate());

            for (int i = 0; i <= 1; i++)
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
            }
            FillRateGrid();
        }

        /// <summary>
        /// Fills the carrier datagrid with carrier information.
        /// </summary>
        private void FillCarrierGrid()
        {
            //foreach (Carrier c in carrierList)
            //{
            //    CarrierTableDisplay.Items.Add(c);
            //}

            CarrierTableDisplay.ItemsSource = carrierList;
        }

        /// <summary>
        /// Fills the route datagrid with route information.
        /// </summary>
        private void FillRouteGrid()
        {
            foreach (var s in carrierList)
            {
                var temp = "";
                foreach (var d in s.DepotCities)
                {
                    temp += d + ", ";
                }
                s.DepotCityString = temp;
            }

            foreach (TransportCorridor c in routeList)
            {
                RouteTableDisplay.Items.Add(c);
            }
            //RouteTableDisplay.ItemsSource = routeList;

        }

        /// <summary>
        /// Fills the rate datagrid with markup information.
        /// </summary>
        private void FillRateGrid()
        {
            //RateTableDisplay.ItemsSource = rateList;
            foreach (TypeAndRate c in rateList)
            {
                RateTableDisplay.Items.Add(c);
            }
        }

        /// <summary>
        /// This handler will exit out of the IP and PORT configuration view.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConfigBackBtn_OnClick(object sender, RoutedEventArgs e)
        {
            IpPortConfigGrid.Visibility = Visibility.Collapsed;
        }
        
        /// <summary>
        /// This method will clear all data tables of current data and load them with the most recent
        /// as found in the local TMS database.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RefreshDataTablesBtn_OnClick(object sender, RoutedEventArgs e)
        {
            trigger = false;
            //CarrierTableDisplay.Items.Clear();
            RouteTableDisplay.Items.Clear();
            RateTableDisplay.Items.Clear();
            carrierList.Clear();
            routeList.Clear();
            rateList.Clear();
            trigger = true;

            UpdateDataTables();

            refreshPopup.IsOpen = true;
        }

        /// <summary>
        /// This method will request the changed made to the carrier FTLRate be applied in the database.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ApplyCarrierChangesBtn_OnClick(object sender, RoutedEventArgs e)
        {
            foreach (var c in carrierList)
            {
                admin.UpdateCarrierTable(c);
            }

            updateTablePopup.IsOpen = true;
        }

        /// <summary>
        /// This method updates the local list with the new values held in the object present in the datagrid
        /// after changes were made. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CarrierTableDisplay_OnCellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            carrierList.Remove((Carrier) CarrierTableDisplay.SelectedItem);
            carrierList.Add((Carrier) CarrierTableDisplay.SelectedItem);
        }
    }
}




