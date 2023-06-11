using RESTServer.Models;

namespace PlanerAppTests.Tests
{
    /// <summary>
    /// Provides unit tests for the model classes.
    /// </summary>
    public class ModelClassesTests
    {
        /// <summary>
        /// Tests the setting of properties in the User class.
        /// </summary>
        [Fact]
        public void User_SetProperties_PropertiesAreSetCorrectly()
        {
            // Arrange
            string firstName = "John";
            string lastName = "Doe";
            string login = "johndoe";
            string password = "password123";
            string email = "johndoe@example.com";

            // Act
            User user = new User(firstName, lastName, login, password, email);

            // Assert
            Assert.Equal(firstName, user.FirstName);
            Assert.Equal(lastName, user.LastName);
            Assert.Equal(login, user.Login);
            Assert.Equal(password, user.Password);
            Assert.Equal(email, user.Email);
        }

        /// <summary>
        /// Tests the setting of properties in the Note class.
        /// </summary>
        [Fact]
        public void Note_SetProperties_PropertiesAreSetCorrectly()
        {
            // Arrange
            string title = "Sample Note";
            string time = "10:00 AM";
            int userID = 1;
            DateTime noteDate = DateTime.Now;
            bool isChecked = false;

            // Act
            Note note = new Note(title, time, userID, noteDate, isChecked);

            // Assert
            Assert.Equal(title, note.Title);
            Assert.Equal(time, note.Time);
            Assert.Equal(userID, note.UserID);
            Assert.Equal(noteDate, note.NoteDate);
            Assert.Equal(isChecked, note.IsChecked);
        }
    }
}
