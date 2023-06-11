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

        /// <summary>
        /// ID that we don't set anywhere, it comes from DB
        /// </summary>
        [JsonPropertyName("id")]
        public int ID { get => id; set => id = value; }

        /// <summary>
        /// First name of user
        /// </summary>
        [JsonPropertyName("firstName")]
        public string FirstName { get => firstName; set => firstName = value; }

        /// <summary>
        /// Last name of user
        /// </summary>
        [JsonPropertyName("lastName")]
        public string LastName { get => lastName; set => lastName = value; }

        /// <summary>
        /// Login of the user
        /// </summary>
        [JsonPropertyName("login")]
        public string Login { get => login; set => login = value; }

        /// <summary>
        /// Password for users account
        /// </summary>
        [JsonPropertyName("password")]
        public string Password { get => password; set => password = value; }

        /// <summary>
        /// Email for users account
        /// </summary>
        [JsonPropertyName("email")]
        public string Email { get => email; set => email = value; }

        /// <summary>
        /// Empty ctor
        /// </summary>
        public User()
        {

        }

        /// <summary>
        /// Ctor with parameters
        /// </summary>
        /// <param name="firstName">First name of the user</param>
        /// <param name="lastName">Last name of the user</param>
        /// <param name="login">Login of the user</param>
        /// <param name="password">Password for users account</param>
        /// <param name="email">Email for users account</param>
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
