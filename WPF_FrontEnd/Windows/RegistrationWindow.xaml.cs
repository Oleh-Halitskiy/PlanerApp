using RESTServer.Models;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WPF_FrontEnd.RESTUtils;

namespace WPF_FrontEnd
{
    /// <summary>
    /// Interaction logic for RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        private RESTClient WebClient = new RESTClient();

        public RegistrationWindow() => InitializeComponent();

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

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e) => Close();
    }
}
