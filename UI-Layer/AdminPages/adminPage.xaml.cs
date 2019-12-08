using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using Google.Protobuf.WellKnownTypes;


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
            FillCarrierList();
            FillRateList();
            FillRouteList();
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
            if (ip != "")
            {
                IpInfoDisplay.Text = ConfigurationManager.AppSettings["ipInfo"] = ip;
            }

            if (port != "")
            {
                PortInfoDisplay.Text = ConfigurationManager.AppSettings["portInfo"] = port;
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
        /// This method will allow the user to select a save location for the database backup.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackupBtn_Click(object sender, RoutedEventArgs e)
        {
            Stream logStream;
            SaveFileDialog dlg = new SaveFileDialog();

            dlg.Filter = "Text documents (.txt)|*.txt";
            dlg.FilterIndex = 2;
            dlg.RestoreDirectory = true;
            dlg.DefaultExt = ".txt";
            dlg.FileName = "TmsLocalBackup";
            //DateTime.Now.ToString("MMM dd, yyyy HH:MM:SS")

            // Show save file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            if (dlg.ShowDialog() == true)
            {
                if ((logStream = dlg.OpenFile()) != null)
                {
                    // Code to write the stream goes here.
                    logStream.Close();
                }
            }

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
            if (DataGridFullGrid.Visibility == Visibility.Visible)
            {
                DataGridFullGrid.Visibility = Visibility.Collapsed;
            }
            else
            {
                DataGridFullGrid.Visibility = Visibility.Visible;
            }

            //string fileName = ConfigurationManager.AppSettings["logLocation"] + "/TmsLocalBackup";

            //OpenFileDialog openFileDialog = new OpenFileDialog();
            //if (openFileDialog.ShowDialog() == true)
            //{
            //    StreamReader file = new StreamReader(fileName);


            //    //while ((int line = file.ReadLine()) != null)
            //    //{
            //    //    LogFileDisplay.Add(line);
            //    //}
            //    file.Close();
            //}
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
            var clickedItem = (ComboBox)sender;
            var index = clickedItem.SelectedIndex;

            if (index == 0)
            {
                RouteTableDisplay.Visibility = Visibility.Visible;
                RateTableDisplay.Visibility = Visibility.Collapsed;
                CarrierTableDisplay.Visibility = Visibility.Collapsed;
            }
            else if (index == 1)
            {
                RateTableDisplay.Visibility = Visibility.Visible;
                RouteTableDisplay.Visibility = Visibility.Collapsed;
                CarrierTableDisplay.Visibility = Visibility.Collapsed;

            }
            else if (index == 2)
            {
                CarrierTableDisplay.Visibility = Visibility.Visible;
                RateTableDisplay.Visibility = Visibility.Collapsed;
                RouteTableDisplay.Visibility = Visibility.Collapsed;

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

        ///// <summary>
        ///// Event handler that checks the element that is currently selected and sets the local data member equal 
        ///// responsible for tracking the selection to the selection
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void ContractGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    // set current item = sender
        //    DataGrid gridSelection = (DataGrid)sender;
        //    Contract newItem = gridSelection.SelectedItem as Contract;

        //    if (newItem != null)
        //    {
        //        selectedContract = newItem;
        //    }
        //    CreateBtn.IsEnabled = true;
        //}
    }
}




