using RESTServer.Models;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using WPF_FrontEnd.AppVars;
using WPF_FrontEnd.RESTUtils;

namespace WPF_FrontEnd.UserControls
{
    /// <summary>
    /// Interaction logic for Item.xaml
    /// </summary>
    public partial class Item : UserControl
    {
        /// <summary>
        /// Variable for rest client
        /// </summary>
        private RESTClient WebClient;

        /// <summary>
        /// Variable for note property binding
        /// </summary>
        public static readonly DependencyProperty NoteProperty = DependencyProperty.Register("Note", typeof(Note), typeof(Item));

        /// <summary>
        /// Variable for note property
        /// </summary>
        public Note Note
        {
            get { return (Note)GetValue(NoteProperty); }
            set { SetValue(NoteProperty, value); }
        }

        /// <summary>
        /// Variable for note title
        /// </summary>
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        /// <summary>
        /// Variable for title property binding
        /// </summary>
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(Item));

        /// <summary>
        /// Variable for note time
        /// </summary>
        public string Time
        {
            get { return (string)GetValue(TimeProperty); }
            set { SetValue(TimeProperty, value); }
        }

        /// <summary>
        /// Variable for time property binding
        /// </summary>
        public static readonly DependencyProperty TimeProperty = DependencyProperty.Register("Time", typeof(string), typeof(Item));

        /// <summary>
        /// Variable for color property binding
        /// </summary>
        public SolidColorBrush Color
        {
            get { return (SolidColorBrush)GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }

        /// <summary>
        /// Variable for color property binding
        /// </summary>
        public static readonly DependencyProperty ColorProperty = DependencyProperty.Register("Color", typeof(SolidColorBrush), typeof(Item));


        /// <summary>
        /// Variable for icon property
        /// </summary>
        public FontAwesome.WPF.FontAwesomeIcon Icon
        {
            get { return (FontAwesome.WPF.FontAwesomeIcon)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        /// <summary>
        /// Variable for icon property binding
        /// </summary>
        public static readonly DependencyProperty IconProperty = DependencyProperty.Register("Icon", typeof(FontAwesome.WPF.FontAwesomeIcon), typeof(Item));


        /// <summary>
        /// Variable for icon bell property
        /// </summary>
        public FontAwesome.WPF.FontAwesomeIcon IconBell
        {
            get { return (FontAwesome.WPF.FontAwesomeIcon)GetValue(IconBellProperty); }
            set { SetValue(IconBellProperty, value); }
        }

        /// <summary>
        /// Variable for icon bell property binding
        /// </summary>
        public static readonly DependencyProperty IconBellProperty = DependencyProperty.Register("IconBell", typeof(FontAwesome.WPF.FontAwesomeIcon), typeof(Item));


        /// <summary>
        /// Event for when we press edit button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditButton_Click(object sender, MouseButtonEventArgs e)
        {
            EditWindow editWindow = new EditWindow(Note);
            editWindow.ShowDialog();
            GlobalVariables.MainWindow.RefreshCurrentItems();

        }

        /// <summary>
        /// Delete for when we press edit button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteButton_Click(object sender, MouseButtonEventArgs e)
        {
            WebClient.DeleteNote(Note.ID);
            GlobalVariables.CurrentNotes = WebClient.GetNotesByUserID(GlobalVariables.CurrentUser.ID);
            GlobalVariables.MainWindow.RefreshCurrentItems();
        }

        /// <summary>
        /// Default ctor where we also call first init
        /// </summary>
        public Item()
        {
            InitializeComponent();
            FirstInit();
        }

        /// <summary>
        /// Method that we use to init some variables that we later use in this window
        /// </summary>
        public void FirstInit()
        {
            WebClient = new RESTClient();
            item.Color = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFF"));
            item.IconBell = FontAwesome.WPF.FontAwesomeIcon.Bell;
        }

        /// <summary>
        /// Setting the note here, because we can't do it in ctor
        /// </summary>
        /// <param name="note"></param>
        public void SetNote(Note note)
        {
            Note = note;
            Title = Note.Title;
            Time = Note.Time;

            if (Note.IsChecked == true)
            {
                Icon = FontAwesome.WPF.FontAwesomeIcon.CheckCircle;
            }
            else
            {
                Icon = FontAwesome.WPF.FontAwesomeIcon.CircleOutline;
            }
        }

        /// <summary>
        /// Changing icon for checked/unchecked here with the press of the button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckButton_Click(object sender, MouseButtonEventArgs e)
        {
            if (Note.IsChecked == true)
            {
                Icon = FontAwesome.WPF.FontAwesomeIcon.CircleOutline;
                Note.IsChecked = false;
                WebClient.UpdateNote(Note);
            }
            else
            {
                Icon = FontAwesome.WPF.FontAwesomeIcon.CheckCircle;
                Note.IsChecked = true;
                WebClient.UpdateNote(Note);
            }
        }
    }
}
