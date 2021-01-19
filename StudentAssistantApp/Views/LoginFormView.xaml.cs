using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace StudentAssistantApp.Views
{
    /// <summary>
    /// Interaction logic for LoginFormView.xaml
    /// </summary>
    public partial class LoginFormView : Window
    {
        public LoginFormView()
        {
            InitializeComponent();
        }
        //teoretycznie złamanie zasad MVVM, ale jest konieczne by działało logowanie i tworzenie użytkownika
        private void Password_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null)
            {
                ((dynamic)this.DataContext).SecurePassword = ((PasswordBox)sender).SecurePassword;
            }
        }
        private void NewPassword1_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null)
            {
                ((dynamic)this.DataContext).NewSecurePassword1 = ((PasswordBox)sender).SecurePassword;
            }
        }
        private void NewPassword2_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null)
            {
                ((dynamic)this.DataContext).NewSecurePassword2 = ((PasswordBox)sender).SecurePassword;
            }
        }
    }
}
