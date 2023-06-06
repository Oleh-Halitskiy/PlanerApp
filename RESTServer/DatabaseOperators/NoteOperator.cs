using RESTServer.DataUtils;
using RESTServer.Models;

namespace RESTServer.DatabaseOperators
{
    public class NoteOperator
    {
        private DataAccess dataAccess = new DataAccess();
        public async void DeleteNoteByID(int id)
        {
            Note note = new Note();
            note.ID = id;
            await dataAccess.SaveData("dbo.spDeleteNoteByID", new { note.ID });
        }
        public async Task<IEnumerable<Note>> GetNotesByUserID(int id)
        {
            User user = new User();
            user.ID = id;
            Task<IEnumerable<Note>> TaskNotes = dataAccess.LoadData<Note, dynamic>("dbo.spGetNotesByUserID", new { user.ID });
            IEnumerable<Note> INotes = await TaskNotes;
            return INotes;
        }
        public async void InsertNote(Note note)
        {
            await dataAccess.SaveData("dbo.spInsertNote", new { note.Title, note.Time, note.UserID, note.NoteDate, note.IsChecked });
        }
        public async void UpdateNote(Note note)
        {
            await dataAccess.SaveData("dbo.spUpdateNoteByID", new { note.ID, note.Title, note.Time, note.IsChecked });
        }
    }
}
