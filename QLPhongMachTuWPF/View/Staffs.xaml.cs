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

namespace QLPhongMachTuWPF.View
{
    /// <summary>
    /// Interaction logic for Staffs.xaml
    /// </summary>
    public partial class Staffs : UserControl
    {
        public Staffs()
        {
            InitializeComponent();
        }
        private void AddStaffButton_Click(object sender, RoutedEventArgs e)
        {
            // Mở cửa sổ AddPatientWindow
            AddStaff addStaffWindow = new AddStaff();
            addStaffWindow.ShowDialog(); 
        }
    }
}
