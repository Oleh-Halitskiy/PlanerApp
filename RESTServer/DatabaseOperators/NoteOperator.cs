using RESTServer.DataUtils;
using RESTServer.Models;

namespace RESTServer.DatabaseOperators
{
    /// <summary>
    /// Provides operations related to note management.
    /// </summary>
    public class NoteOperator
    {
        private DataAccess dataAccess = new DataAccess();

        /// <summary>
        /// Deletes a note from the database based on the specified ID.
        /// </summary>
        /// <param name="id">The ID of the note to delete.</param>
        public async void DeleteNoteByID(int id)
        {
            Note note = new Note();
            note.ID = id;
            await dataAccess.SaveData("dbo.spDeleteNoteByID", new { note.ID });
        }

        /// <summary>
        /// Retrieves a collection of notes associated with the specified user ID.
        /// </summary>
        /// <param name="id">The ID of the user to retrieve notes for.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains the collection of notes.</returns>
        public async Task<IEnumerable<Note>> GetNotesByUserID(int id)
        {
            User user = new User();
            user.ID = id;
            Task<IEnumerable<Note>> TaskNotes = dataAccess.LoadData<Note, dynamic>("dbo.spGetNotesByUserID", new { user.ID });
            IEnumerable<Note> INotes = await TaskNotes;
            return INotes;
        }

        /// <summary>
        /// Inserts a new note into the database.
        /// </summary>
        /// <param name="note">The note object to insert.</param>
        public async void InsertNote(Note note) => await dataAccess.SaveData("dbo.spInsertNote", new { note.Title, note.Time, note.UserID, note.NoteDate, note.IsChecked });

        /// <summary>
        /// Updates an existing note in the database.
        /// </summary>
        /// <param name="note">The note object to update.</param>
        public async void UpdateNote(Note note) => await dataAccess.SaveData("dbo.spUpdateNoteByID", new { note.ID, note.Title, note.Time, note.IsChecked });
    }
}
