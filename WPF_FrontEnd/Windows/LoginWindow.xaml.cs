using System.Windows;
using System.Windows.Input;
using WPF_FrontEnd.AppVars;
using WPF_FrontEnd.RESTUtils;

namespace WPF_FrontEnd
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        /// <summary>
        /// Variable that hold ref to rest client
        /// </summary>
        private RESTClient WebClient = new RESTClient();

        /// <summary>
        /// Default ctor
        /// </summary>
        public LoginWindow() => InitializeComponent();

        /// <summary>
        /// Event for dragging the window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        /// <summary>
        /// Method that holds login for when registration button is pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RegButton_Click(object sender, RoutedEventArgs e)
        {
            RegistrationWindow registrationWindow = new RegistrationWindow();
            Hide();
            registrationWindow.ShowDialog();
            Show(); 
        }
        /// <summary>
        /// Method that holds login for when login button is pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                        Hide();
                        GlobalVariables.MainWindow = mainWindow;
                        mainWindow.ShowDialog();
                        Close();
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
        private void CloseButton_Click(object sender, RoutedEventArgs e) => Close();
    }
}
