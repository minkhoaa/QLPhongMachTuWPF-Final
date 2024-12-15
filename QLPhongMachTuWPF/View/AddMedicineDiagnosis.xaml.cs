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
using System.Windows.Shapes;

namespace QLPhongMachTuWPF.View
{
    /// <summary>
    /// Interaction logic for AddMedicineDiagnosis.xaml
    /// </summary>
    public partial class AddMedicineDiagnosis : Window
    {
        public AddMedicineDiagnosis()
        {
            InitializeComponent();
            var status = new List<string>
            {
                "Available", "Unavailable"
            };

            cmbStatus.ItemsSource = status;

            var days = Enumerable.Range(1, 31).ToList();
            cmbDay.ItemsSource = days;
            

            var months = new List<string>
{
    "January", "February", "March", "April", "May", "June",
    "July", "August", "September", "October", "November", "December"
};

            cmbMonth.ItemsSource = months;
            

            var years = Enumerable.Range(1900, DateTime.Now.Year - 1900 + 1).ToList();


            cmbYear.ItemsSource = years;
            

        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

      
    }
}
