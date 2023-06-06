using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using RESTServer.DatabaseOperators;
using RESTServer.Models;

namespace RESTServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        NoteOperator noteOperator = new NoteOperator();

        [HttpGet(Name = "ByUserID")]
        public async Task<IEnumerable<Note>> Get(int userID)
        {
            Task<IEnumerable<Note>> InoteTask = noteOperator.GetNotesByUserID(userID);
            IEnumerable<Note> Inote = await InoteTask;
            return Inote;
        }
        [HttpPut(Name = "InsertNote")]
        public void Put(string Title, string Time, int UserID, DateTime NoteDate, bool IsChecked)
        {
            Note note = new Note(Title, Time, UserID, NoteDate, IsChecked);
            noteOperator.InsertNote(note);
        }
        [HttpDelete(Name = "DeleteNote")]
        public void Delete(int ID)
        {
            noteOperator.DeleteNoteByID(ID);
        }
        [HttpPatch(Name = "UpdateNote")]
        public void Patch(int ID, string Title, string Time, bool isChecked)
        {
            Note note = new Note();
            note.Title = Title;
            note.Time = Time;
            note.IsChecked = isChecked;
            note.ID = ID;
            noteOperator.UpdateNote(note);
        }
    }
}
