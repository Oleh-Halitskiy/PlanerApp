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
        private RESTClient WebClient;

        public static readonly DependencyProperty NoteProperty = DependencyProperty.Register("Note", typeof(Note), typeof(Item));

        public Note Note
        {
            get { return (Note)GetValue(NoteProperty); }
            set { SetValue(NoteProperty, value); }
        }

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(Item));


        public string Time
        {
            get { return (string)GetValue(TimeProperty); }
            set { SetValue(TimeProperty, value); }
        }

        public static readonly DependencyProperty TimeProperty = DependencyProperty.Register("Time", typeof(string), typeof(Item));


        public SolidColorBrush Color
        {
            get { return (SolidColorBrush)GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }

        public static readonly DependencyProperty ColorProperty = DependencyProperty.Register("Color", typeof(SolidColorBrush), typeof(Item));


        public FontAwesome.WPF.FontAwesomeIcon Icon
        {
            get { return (FontAwesome.WPF.FontAwesomeIcon)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        public static readonly DependencyProperty IconProperty = DependencyProperty.Register("Icon", typeof(FontAwesome.WPF.FontAwesomeIcon), typeof(Item));


        public FontAwesome.WPF.FontAwesomeIcon IconBell
        {
            get { return (FontAwesome.WPF.FontAwesomeIcon)GetValue(IconBellProperty); }
            set { SetValue(IconBellProperty, value); }
        }

        public static readonly DependencyProperty IconBellProperty = DependencyProperty.Register("IconBell", typeof(FontAwesome.WPF.FontAwesomeIcon), typeof(Item));

        private void EditButton_Click(object sender, MouseButtonEventArgs e)
        {
            EditWindow editWindow = new EditWindow(Note);
            editWindow.ShowDialog();
            GlobalVariables.MainWindow.RefreshCurrentItems();

        }

        private void DeleteButton_Click(object sender, MouseButtonEventArgs e)
        {
            WebClient.DeleteNote(Note.ID);
            GlobalVariables.CurrentNotes = WebClient.GetNotesByUserID(GlobalVariables.CurrentUser.ID);
            GlobalVariables.MainWindow.RefreshCurrentItems();
        }

        public Item()
        {
            InitializeComponent();
            FirstInit();
        }

        public void FirstInit()
        {
            WebClient = new RESTClient();
            item.Color = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFF"));
            item.IconBell = FontAwesome.WPF.FontAwesomeIcon.Bell;
        }

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
