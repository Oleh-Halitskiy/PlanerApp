using Microsoft.AspNetCore.Mvc;
using RESTServer.DatabaseOperators;
using RESTServer.Models;

namespace RESTServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    /// <summary>
    /// Provides API endpoints for note management.
    /// </summary>
    public class NoteController : ControllerBase
    {
        NoteOperator noteOperator = new NoteOperator();

        /// <summary>
        /// Retrieves a collection of notes associated with the specified user ID.
        /// </summary>
        /// <param name="userID">The ID of the user to retrieve notes for.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains the collection of notes.</returns>
        [HttpGet(Name = "ByUserID")]
        public async Task<IEnumerable<Note>> Get(int userID)
        {
            Task<IEnumerable<Note>> InoteTask = noteOperator.GetNotesByUserID(userID);
            IEnumerable<Note> Inote = await InoteTask;
            return Inote;
        }

        /// <summary>
        /// Inserts a new note into the database.
        /// </summary>
        /// <param name="noteBody">The note object to insert.</param>
        [HttpPost(Name = "InsertNote")]
        public void Post([FromBody] Note noteBody) => noteOperator.InsertNote(noteBody);

        /// <summary>
        /// Deletes a note from the database based on the specified ID.
        /// </summary>
        /// <param name="ID">The ID of the note to delete.</param>
        [HttpDelete(Name = "DeleteNote")]
        public void Delete(int ID) => noteOperator.DeleteNoteByID(ID);

        /// <summary>
        /// Updates an existing note in the database.
        /// </summary>
        /// <param name="noteBody">The note object to update.</param>
        [HttpPatch(Name = "UpdateNote")]
        public void Patch([FromBody] Note noteBody) => noteOperator.UpdateNote(noteBody);
    }
}
