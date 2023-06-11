using Autofac.Extras.Moq;
using Moq;
using RESTServer.DataUtils;
using RESTServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanerAppTests.Tests
{
    /// <summary>
    /// Provides unit tests for the DataAccess class.
    /// </summary>
    public class DataAccessTest
    {
        /// <summary>
        /// Tests the general data access save operations.
        /// </summary>
        [Fact]
        public async Task GeneralDataAccessSaveTest()
        {
            using (var mock = AutoMock.GetLoose())
            {
                Note note = new Note("Note Title", "19.00 - 20.00", 1, DateTime.Today, false);
                mock.Mock<IDataAccess>().Setup(x => x.SaveData("dbo.spInsertNote", new { note.Title, note.Time, note.UserID, note.NoteDate, note.IsChecked }));
                mock.Mock<IDataAccess>().Setup(x => x.SaveData("dbo.spUpdateNoteByID", new { note.ID, note.Title, note.Time, note.IsChecked }));
                mock.Mock<IDataAccess>().Setup(x => x.SaveData("dbo.spDeleteNoteByID", new { note.ID }));
                var cls = mock.Create<DBprocessor>();

                cls.InsertNote(note);
                cls.UpdateNote(note);
                cls.DeleteNoteByID(note.ID);
                mock.Mock<IDataAccess>().Verify(x => x.SaveData("dbo.spInsertNote", new { note.Title, note.Time, note.UserID, note.NoteDate, note.IsChecked }), Times.Exactly(1));
                mock.Mock<IDataAccess>().Verify(x => x.SaveData("dbo.spUpdateNoteByID", new { note.ID, note.Title, note.Time, note.IsChecked }), Times.Exactly(1));
                mock.Mock<IDataAccess>().Verify(x => x.SaveData("dbo.spDeleteNoteByID", new { note.ID }), Times.Exactly(1));
            }
        }

        /// <summary>
        /// Tests the general data access load operation.
        /// </summary>
        [Fact]
        public void GeneralDataAccessLoadTest()
        {
            using (var mock = AutoMock.GetLoose())
            {
                User user = new User();
                user.ID = 1;
                mock.Mock<IDataAccess>().Setup(x => x.LoadData<Note, dynamic>("dbo.spGetNotesByUserID", new { user.ID })).Returns(GetNotes);
                var cls = mock.Create<DBprocessor>();
                var expected = GetNotes();
                IEnumerable<Note> enums = expected.Result;
                List<Note> expectedlist = enums.ToList();
                var actual = cls.LoadNotesByUserID(1);

                Assert.True(actual != null);
                Assert.Equal(expectedlist.Count, actual.Count);
                Assert.Equal(expectedlist[0].Title, actual[0].Title);
                Assert.Equal(expectedlist[1].Time, actual[1].Time);
                Assert.Equal(expectedlist[2].UserID, actual[2].UserID);
            }
        }

        /// <summary>
        /// Represents a processor class for database operations.
        /// </summary>
        public class DBprocessor
        {
            private readonly IDataAccess daacc;

            /// <summary>
            /// Initializes a new instance of the DBprocessor class.
            /// </summary>
            /// <param name="dataAccess">The data access object to use.</param>
            public DBprocessor(IDataAccess dataAccess) => daacc = dataAccess;

            /// <summary>
            /// Loads notes by user ID from the database.
            /// </summary>
            /// <param name="UserID">The user ID.</param>
            /// <returns>A list of notes.</returns>
            public List<Note> LoadNotesByUserID(int UserID)
            {
                User user = new User();
                user.ID = UserID;
                Task<IEnumerable<Note>> test = daacc.LoadData<Note, dynamic>("dbo.spGetNotesByUserID", new { user.ID });
                IEnumerable<Note> resutlts = test.Result;
                List<Note> list = resutlts.ToList();
                return list;
            }

            /// <summary>
            /// Inserts a note into the database.
            /// </summary>
            /// <param name="note">The note to insert.</param>
            public async Task InsertNote(Note note) => await daacc.SaveData("dbo.spInsertNote", new { note.Title, note.Time, note.UserID, note.NoteDate, note.IsChecked });

            /// <summary>
            /// Updates a note in the database.
            /// </summary>
            /// <param name="note">The note to update.</param>
            public async void UpdateNote(Note note) => await daacc.SaveData("dbo.spUpdateNoteByID", new { note.ID, note.Title, note.Time, note.IsChecked });

            /// <summary>
            /// Deletes a note from the database by ID.
            /// </summary>
            /// <param name="id">The ID of the note to delete.</param>
            public async void DeleteNoteByID(int id)
            {
                Note note = new Note();
                note.ID = id;
                await daacc.SaveData("dbo.spDeleteNoteByID", new { note.ID });
            }
        }

        /// <summary>
        /// Mocks the return query from the database.
        /// </summary>
        /// <returns>A task containing the list of notes.</returns>
        public Task<IEnumerable<Note>> GetNotes()
        {
            Note note = new Note("Note Title1", "19.00 - 20.00", 1, DateTime.Today, false);
            Note note1 = new Note("Note Title2", "19.00 - 20.00", 2, DateTime.Today, false);
            Note note2 = new Note("Note Title3", "19.00 - 20.00", 3, DateTime.Today, false);
            var list = new List<Note>
        {
            note,
            note1,
            note2
        };
            return Task.FromResult<IEnumerable<Note>>(list);
        }
    }
}

