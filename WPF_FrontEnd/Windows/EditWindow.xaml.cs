using RESTServer.Models;
using System;
using System.Windows;
using System.Windows.Input;
using WPF_FrontEnd.AppVars;
using WPF_FrontEnd.RESTUtils;

namespace WPF_FrontEnd
{
    /// <summary>
    /// Interaction logic for EditWindow.xaml
    /// </summary>
    public partial class EditWindow : Window
    {
        private Note note;
        private RESTClient WebClient;

        public EditWindow(Note note)
        {
            InitializeComponent();
            WebClient = new RESTClient();
            this.note = note;
            TitleTextBox.Text = note.Title;
            TimeTextBox.Text = note.Time;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void ReturnButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            note.Title = TitleTextBox.Text;
            note.Time = TimeTextBox.Text;
            WebClient.UpdateNote(note);
            GlobalVariables.CurrentNotes = WebClient.GetNotesByUserID(GlobalVariables.CurrentUser.ID);
            Close(); 
        }
    }
}
