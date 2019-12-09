using System;
using System.Collections.Generic;
using System.Configuration;
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
using TMS.Business_Layer.users;

namespace TMS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Content = new BuyerPage();

            Logger.Log("Application start.");
        }

        /// <summary>
        /// This redirects the frame to the admin page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Admin_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new AdminPage();
        }

        /// <summary>
        /// This redirects the frame to the Planner page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Planner_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new PlannerPage();
        }

        /// <summary>
        /// This redirects the frame to the Buyer page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Buyer_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new BuyerPage();
        }
    }
}
