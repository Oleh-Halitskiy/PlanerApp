using System.Text.Json.Serialization;

namespace RESTServer.Models
{
    public class User
    {
        private int id;
        private string firstName;
        private string lastName;
        private string login;
        private string password;
        private string email;

        [JsonPropertyName("id")]
        public int ID { get => id; set => id = value; }
        [JsonPropertyName("firstName")]
        public string FirstName { get => firstName; set => firstName = value; }
        [JsonPropertyName("lastName")]
        public string LastName { get => lastName; set => lastName = value; }
        [JsonPropertyName("login")]
        public string Login { get => login; set => login = value; }
        [JsonPropertyName("password")]
        public string Password { get => password; set => password = value; }
        [JsonPropertyName("email")]
        public string Email { get => email; set => email = value; }

        public User()
        {

        }
        public User(string firstName, string lastName, string login, string password, string email)
        { 
            FirstName = firstName;
            LastName = lastName;
            Login = login;
            Password = password;
            Email = email;
        }
    }
}
