﻿using System;
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

namespace TMS
{
    /// <summary>
    /// Interaction logic for plannerPage.xaml
    /// </summary>
    public partial class PlannerPage : Page
    {
        public PlannerPage()
        {
            InitializeComponent();
        }

        private void NewOrder_Click(object sender, RoutedEventArgs e)
        {
            PlannerFrame.Content = new NewOrdersPage();
        }

        private void activeOrder_Click(object sender, RoutedEventArgs e)
        {
            PlannerFrame.Content = new ActiveOrdersPage();
        }
    }
}
