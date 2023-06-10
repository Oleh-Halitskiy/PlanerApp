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
        [HttpPost(Name = "InsertNote")]
        public void Post([FromBody] Note noteBody)
        {
            noteOperator.InsertNote(noteBody);
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
