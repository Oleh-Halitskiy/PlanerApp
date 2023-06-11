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
        /// <summary>
        /// Note that about to be changed
        /// </summary>
        private Note note;
        /// <summary>
        /// Variable for rest client
        /// </summary>
        private RESTClient WebClient;

        /// <summary>
        /// Default ctor with exception of setting fields from note var
        /// </summary>
        /// <param name="note"></param>
        public EditWindow(Note note)
        {
            InitializeComponent();
            WebClient = new RESTClient();
            this.note = note;
            TitleTextBox.Text = note.Title;
            TimeTextBox.Text = note.Time;
        }

        /// <summary>
        /// Event for dragging the window across
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
        /// <summary>
        /// Event for when Edit button is pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            note.Title = TitleTextBox.Text;
            note.Time = TimeTextBox.Text;
            WebClient.UpdateNote(note);
            GlobalVariables.CurrentNotes = WebClient.GetNotesByUserID(GlobalVariables.CurrentUser.ID);
            Close(); 
        }
        /// <summary>
        /// Return button event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReturnButton_Click(object sender, RoutedEventArgs e) => Close();
    }
}
