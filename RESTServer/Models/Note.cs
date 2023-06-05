namespace RESTServer.Models
{
    public class Note
    {
        private int id;
        private string title;
        private string time;
        private int userID;
        private DateTime noteDate;
        private bool isChecked;

        public int ID { get => id; set => id = value; }
        public string Title { get => title; set => title = value; }
        public string Time { get => time; set => time = value; }
        public int UserID { get => userID; set => userID = value; }
        public DateTime NoteDate { get => noteDate; set => noteDate = value; }
        public bool IsChecked { get => isChecked; set => isChecked = value; }

        public Note()
        {

        }

        public Note(string title, string time, int userID, DateTime noteDate, bool isChecked)
        {
            Title = title;
            Time = time;
            UserID = userID;
            NoteDate = noteDate;
            IsChecked = isChecked;
        }
    }
}
