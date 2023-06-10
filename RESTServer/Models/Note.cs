using System.Text.Json.Serialization;

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

        [JsonPropertyName("id")]
        public int ID { get => id; set => id = value; }
        [JsonPropertyName("title")]
        public string Title { get => title; set => title = value; }
        [JsonPropertyName("time")]
        public string Time { get => time; set => time = value; }
        [JsonPropertyName("userID")]
        public int UserID { get => userID; set => userID = value; }
        [JsonPropertyName("noteDate")]
        public DateTime NoteDate { get => noteDate; set => noteDate = value; }
        [JsonPropertyName("isChecked")]
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
