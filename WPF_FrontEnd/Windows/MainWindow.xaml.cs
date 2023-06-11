using RESTServer.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
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
        /// <summary>
        /// variable that holds rest client
        /// </summary>
        private RESTClient WebClient;

        /// <summary>
        /// reference to the previously pressed button in array of year button
        /// </summary>
        private Button previouslyPressedYearBT;

        /// <summary>
        /// reference to the previously pressed button in array of month button
        /// </summary>
        private Button previouslyPressedMonthBT;

        /// <summary>
        /// List that holds current date notes
        /// </summary>
        private List<Note> todaysNotes;
        /// <summary>
        /// List that is binded to list box and holds current items
        /// </summary>
        private ObservableCollection<Item> currentItems;

        public ObservableCollection<Item> CurrentItems { get => currentItems; set => currentItems = value; }


        /// <summary>
        /// Default constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            FirstCalendarInit();
        }

        /// <summary>
        /// Method that is used for setting variables and lauching the calendar in working state
        /// </summary>
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

        /// <summary>
        /// Event for when add button is pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Event for array of month buttons
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Event for array of year buttons
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Event for setting current date
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        // changes every month
        private void MainCalendar_DisplayDateChanged(object sender, System.Windows.Controls.CalendarDateChangedEventArgs e)
        {
            MonthNameLabel.Text = MainCalendar.DisplayDate.ToString("MMMM");
            MonthNameLabelRight.Text = MainCalendar.DisplayDate.ToString("MMMM");
        }

        /// <summary>
        /// Event for setting current date
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        // changes every click
        private void MainCalendar_OnSelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            CurrentDayNumber.Text = MainCalendar.SelectedDate.Value.Day.ToString();
            DayNameLabelRight.Text = MainCalendar.SelectedDate.Value.DayOfWeek.ToString();
            FilterNotesByDate(MainCalendar.SelectedDate.Value);
        }

        /// <summary>
        /// Sets current month in calendar
        /// </summary>
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

        /// <summary>
        /// Method that is used to filter notes from db for selected day
        /// </summary>
        /// <param name="date">Current date in calendar</param>
        private void FilterNotesByDate(DateTime date)
        {
            todaysNotes = GlobalVariables.CurrentNotes.Where(note => note.NoteDate.Date == date.Date).ToList();
            SetObservableCollectionFromList(todaysNotes);
        }

        /// <summary>
        /// Method that converts Notes to Items
        /// </summary>
        /// <param name="notes">List of Notes</param>
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

        /// <summary>
        /// Event for hiding hint when typing 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtNote_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNote.Text) && txtNote.Text.Length > 0)
                lblNote.Visibility = Visibility.Collapsed;
            else
                lblNote.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Event for hiding hint when typing 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtTime_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtTime.Text) && txtTime.Text.Length > 0)
                lblTime.Visibility = Visibility.Collapsed;
            else
                lblTime.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Event for dragging the window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Border_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        /// <summary>
        /// Method for refreshing the window with current items
        /// </summary>
        public void RefreshCurrentItems() => FilterNotesByDate(MainCalendar.SelectedDate.Value);

        /// <summary>
        /// Method that loads all the Note for current user from DB
        /// </summary>
        private void LoadUserNotes() => GlobalVariables.CurrentNotes = WebClient.GetNotesByUserID(GlobalVariables.CurrentUser.ID);

        /// <summary>
        /// Method for scrolling left in year section
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScrollLeftButton_Click(object sender, RoutedEventArgs e) => yearSelectionScrollViewer.LineLeft();

        /// <summary>
        /// Method for scrolling right in year section
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScrollRightButton_Click(object sender, RoutedEventArgs e) => yearSelectionScrollViewer.LineRight();

        /// <summary>
        /// Method for closing window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_ClickClose(object sender, RoutedEventArgs e) => this.Close();


        /// <summary>
        /// Method for focusing on text box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblNote_MouseDown(object sender, MouseButtonEventArgs e) => txtNote.Focus();

        /// <summary>
        /// Method for focusing on text box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblTime_MouseDown(object sender, MouseButtonEventArgs e) => txtTime.Focus();

    }
}
