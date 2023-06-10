using RESTServer.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
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
using WPF_FrontEnd.AppVars;
using WPF_FrontEnd.RESTUtils;

namespace WPF_FrontEnd
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        RESTClient WebClient = new RESTClient();
        public LoginWindow()
        {
            InitializeComponent();
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void RegButton_Click(object sender, RoutedEventArgs e)
        {
            RegistrationWindow registrationWindow = new RegistrationWindow();
            Hide();
            registrationWindow.ShowDialog();
        }
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(LoginTextBox.Text) && !string.IsNullOrEmpty(PasswordTextBox.Password))
            {
                if (WebClient.UserExists(LoginTextBox.Text))
                {
                    var tempuser = WebClient.GetUserByLogin(LoginTextBox.Text);
                    if (tempuser.Password == PasswordTextBox.Password)
                    {
                        GlobalVariables.CurrentUser = tempuser;
                        MainWindow mainWindow = new MainWindow();
                        mainWindow.ShowDialog();
                        PasswordTextBox.Password = "";
                        LoginTextBox.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("Password is incorrect");
                    }
                }
                else
                {
                    MessageBox.Show("User with that login doesn't exist");
                }
            }
            else
            {
                MessageBox.Show("Login or password empty");
            }
        }
    }
}
