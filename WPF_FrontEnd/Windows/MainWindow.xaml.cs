using RESTServer.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
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
using WPF_FrontEnd.AppVars;
using WPF_FrontEnd.RESTUtils;
using WPF_FrontEnd.UserControls;

namespace WPF_FrontEnd
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private RESTClient WebClient;
        private Button previouslyPressedYearBT;
        private Button previouslyPressedMonthBT;
        private List<Note> todaysNotes;

        private ObservableCollection<Item> currentItems;

        public ObservableCollection<Item> CurrentItems { get => currentItems; set => currentItems = value; }

        public MainWindow()
        {
            InitializeComponent();
            FirstCalendarInit();
        }
        public void RefreshCurrentItems()
        {
            FilterNotesByDate(MainCalendar.SelectedDate.Value);  
        }
        private void FirstCalendarInit()
        {
            currentItems = new ObservableCollection<Item>();
            WebClient = new RESTClient();
            todaysNotes = new List<Note>();
            SetCurrentWeek();
            LoadUserNotes();

            // enable data binding
            DataContext = this;

            // needed for year selection
            previouslyPressedYearBT = DefaultYearButton;

            // dates setup
            MainCalendar.SelectedDate = DateTime.Now;
            MonthNameLabel.Text = MainCalendar.DisplayDate.ToString("MMMM");
            MonthNameLabelRight.Text = MainCalendar.DisplayDate.ToString("MMMM");
            CurrentDayNumber.Text = MainCalendar.SelectedDate.Value.Day.ToString();
            FilterNotesByDate(MainCalendar.SelectedDate.Value.Date);
        }

        private void Border_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void lblNote_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtNote.Focus();
        }

        private void lblTime_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtTime.Focus();
        }

        private void txtNote_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNote.Text) && txtNote.Text.Length > 0)
                lblNote.Visibility = Visibility.Collapsed;
            else
                lblNote.Visibility = Visibility.Visible;
        }

        private void txtTime_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtTime.Text) && txtTime.Text.Length > 0)
                lblTime.Visibility = Visibility.Collapsed;
            else
                lblTime.Visibility = Visibility.Visible;
        }
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNote.Text) && !string.IsNullOrEmpty(txtTime.Text))
            {
                Note note = new Note(txtNote.Text, txtTime.Text, GlobalVariables.CurrentUser.ID, MainCalendar.SelectedDate.Value.Date, false);
                WebClient.InsertNote(note);
                GlobalVariables.CurrentNotes = WebClient.GetNotesByUserID(GlobalVariables.CurrentUser.ID);
                FilterNotesByDate(MainCalendar.SelectedDate.Value);
                txtNote.Text = "";
                txtTime.Text = "";
            }
            else
            {
                MessageBox.Show("One of the fields is empty");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // setting color to grey back for previous button
            var bc = new BrushConverter();
            previouslyPressedMonthBT.Foreground = (Brush)bc.ConvertFrom("#FFBABABA");

            // setting variables
            Button clickedButton = (Button)sender;
            previouslyPressedMonthBT = clickedButton;
            int monthNumber = Convert.ToInt32(clickedButton.Content);
            DateTime newTime = new DateTime(MainCalendar.SelectedDate.Value.Year, monthNumber, 1);

            // setting the calendar
            MainCalendar.SelectedDate = newTime;
            MainCalendar.DisplayDate = newTime;

            // setting color for button
            clickedButton.Foreground = (Brush)bc.ConvertFrom("#C73F69");
        }
        private void Button_ClickYear(object sender, RoutedEventArgs e)
        {
            // setting color to grey back for previous button
            var bc = new BrushConverter();
            previouslyPressedYearBT.Foreground = (Brush)bc.ConvertFrom("#FFBABABA");

            // setting variables
            Button clickedButton = (Button)sender;
            previouslyPressedYearBT = clickedButton;
            int yearNumber = Convert.ToInt32(clickedButton.Content);
            DateTime newTime = new DateTime(yearNumber, Convert.ToInt32(MainCalendar.SelectedDate.Value.Month), Convert.ToInt32(MainCalendar.SelectedDate.Value.Day));

            // setting the calendar
            MainCalendar.SelectedDate = newTime;
            MainCalendar.DisplayDate = newTime;

            // setting color for button
            clickedButton.Foreground = (Brush)bc.ConvertFrom("#C73F69");

        }
        private void ScrollLeftButton_Click(object sender, RoutedEventArgs e)
        {
            yearSelectionScrollViewer.LineLeft();
        }

        private void ScrollRightButton_Click(object sender, RoutedEventArgs e)
        {
            yearSelectionScrollViewer.LineRight();
        }
        private void Button_ClickClose(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        // changes every month
        private void MainCalendar_DisplayDateChanged(object sender, System.Windows.Controls.CalendarDateChangedEventArgs e)
        {
            MonthNameLabel.Text = MainCalendar.DisplayDate.ToString("MMMM");
            MonthNameLabelRight.Text = MainCalendar.DisplayDate.ToString("MMMM");
        }
        // changes every click
        private void MainCalendar_OnSelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            CurrentDayNumber.Text = MainCalendar.SelectedDate.Value.Day.ToString();
            DayNameLabelRight.Text = MainCalendar.SelectedDate.Value.DayOfWeek.ToString();
            FilterNotesByDate(MainCalendar.SelectedDate.Value);
        }
        private void SetCurrentWeek()
        {
            var bc = new BrushConverter();
            foreach (var child in MonthsButtonPanel.Children)
            {
                if (child is Button button)
                {
                    if (Convert.ToInt32(button.Content) == DateTime.Today.Month)
                    {
                        button.Foreground = (Brush)bc.ConvertFrom("#C73F69");
                        previouslyPressedMonthBT = button;
                        return;
                    }
                }
            }
        }
        private void LoadUserNotes()
        {
            GlobalVariables.CurrentNotes = WebClient.GetNotesByUserID(GlobalVariables.CurrentUser.ID);
        }
        private void FilterNotesByDate(DateTime date)
        {
            todaysNotes = GlobalVariables.CurrentNotes.Where(note => note.NoteDate.Date == date.Date).ToList();
            SetObservableCollectionFromList(todaysNotes);
        }
        private void SetObservableCollectionFromList(List<Note> notes)
        {
            CurrentItems.Clear();
            foreach (Note note in notes)
            {
                Item item = new Item();
                item.SetNote(note);
                currentItems.Add(item);
            }
        }
    }
}
