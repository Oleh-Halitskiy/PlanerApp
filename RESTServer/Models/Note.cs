using System.Text.Json.Serialization;

namespace RESTServer.Models
{
    /// <summary>
    /// Represents a note object.
    /// </summary>
    public class Note
    {
        private int id;
        private string title;
        private string time;
        private int userID;
        private DateTime noteDate;
        private bool isChecked;

        /// <summary>
        /// Gets or sets the ID of the note.
        /// </summary>
        [JsonPropertyName("id")]
        public int ID { get => id; set => id = value; }

        /// <summary>
        /// Gets or sets the title of the note.
        /// </summary>
        [JsonPropertyName("title")]
        public string Title { get => title; set => title = value; }

        /// <summary>
        /// Gets or sets the time associated with the note.
        /// </summary>
        [JsonPropertyName("time")]
        public string Time { get => time; set => time = value; }

        /// <summary>
        /// Gets or sets the ID of the user who created the note.
        /// </summary>
        [JsonPropertyName("userID")]
        public int UserID { get => userID; set => userID = value; }

        /// <summary>
        /// Gets or sets the date of the note.
        /// </summary>
        [JsonPropertyName("noteDate")]
        public DateTime NoteDate { get => noteDate; set => noteDate = value; }

        /// <summary>
        /// Gets or sets a value indicating whether the note is checked.
        /// </summary>
        [JsonPropertyName("isChecked")]
        public bool IsChecked { get => isChecked; set => isChecked = value; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Note"/> class.
        /// </summary>
        public Note()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Note"/> class with the specified values.
        /// </summary>
        /// <param name="title">The title of the note.</param>
        /// <param name="time">The time associated with the note.</param>
        /// <param name="userID">The ID of the user who created the note.</param>
        /// <param name="noteDate">The date of the note.</param>
        /// <param name="isChecked">A value indicating whether the note is checked.</param>
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
