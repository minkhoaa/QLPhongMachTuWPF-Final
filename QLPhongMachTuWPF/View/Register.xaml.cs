
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
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        public Register()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
        private void PasswordBox_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void textUser_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtUser.Focus();

        }

        private void txtUser_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtUser.Text) && txtUser.Text.Length > 0)
            {
                textUser.Visibility = Visibility.Collapsed;
            }
            else
            {
                textUser.Visibility = Visibility.Visible;
            }
        }

        private void textPassword_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtPass.Focus();
        }

        private void txtPass_TextChanged(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPass.Password) && txtPass.Password.Length > 0)
            {
                textPassword.Visibility = Visibility.Collapsed;
            }
            else
            {
                textPassword.Visibility = Visibility.Visible;
            }
        }

        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }







        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current?.Shutdown();
        }

        private void txtConfirmPass_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtConfirmPass.Password) && txtConfirmPass.Password.Length > 0)
            {
                textconfirmPassword.Visibility = Visibility.Collapsed;
            }
            else
            {
                textconfirmPassword.Visibility = Visibility.Visible;
            }
        }

        private void txtPass_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPass.Password) && txtPass.Password.Length > 0)
            {
                textPassword.Visibility = Visibility.Collapsed;
            }
            else
            {
                textPassword.Visibility = Visibility.Visible;
            }
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void textDisplayName_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtDisplayName.Focus();
        }

        private void txtDisplayName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtDisplayName.Text) && txtDisplayName.Text.Length > 0)
            {
                textDisplayName.Visibility = Visibility.Collapsed;
            }
            else
            {
                textDisplayName.Visibility = Visibility.Visible;
            }
        }

        private void textconfirmPassword_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtConfirmPass.Focus();
        }

        private void txtConfirmPass_PasswordChanged_1(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtConfirmPass.Password) && txtConfirmPass.Password.Length > 0)
            {
                textconfirmPassword.Visibility = Visibility.Collapsed;
            }
            else
            {
                txtConfirmPass.Visibility = Visibility.Visible;
            }
        }

        private void textEmail_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtEmail.Focus();
        }

        private void txtEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtEmail.Text) && txtEmail.Text.Length > 0)
            {
                textEmail.Visibility = Visibility.Collapsed;
            }
            else
            {
                textEmail.Visibility = Visibility.Visible;
            }
        }

        private void textEmail_MouseDown_1(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
