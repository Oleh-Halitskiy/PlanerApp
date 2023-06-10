using RESTServer.Models;
using System;
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
using WPF_FrontEnd.RESTUtils;

namespace WPF_FrontEnd
{
    /// <summary>
    /// Interaction logic for RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        RESTClient WebClient = new RESTClient();
        public RegistrationWindow()
        {
            InitializeComponent();
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void RegButton_Click(object sender, RoutedEventArgs e)
        {
            if(!string.IsNullOrEmpty(FNTextBox.Text) &&
               !string.IsNullOrEmpty(LNTextBox.Text) && 
               !string.IsNullOrEmpty(LoginTextBox.Text) &&
               !string.IsNullOrEmpty(PasswordBox.Password) && 
               !string.IsNullOrEmpty(EmailTextBox.Text))
            {
                if(!WebClient.UserExists(LoginTextBox.Text))
                {
                    User user = new User(FNTextBox.Text,LNTextBox.Text,LoginTextBox.Text,PasswordBox.Password,EmailTextBox.Text);
                    WebClient.InsertPerson(user);
                    MessageBox.Show("Registration is successful");
                    Close();
                }
                else
                {
                    MessageBox.Show("User with such login already exist");
                }
            }
            else
            {
                MessageBox.Show("One of the fields is empty check your input");
            }
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Close();    
        }
    }
}
