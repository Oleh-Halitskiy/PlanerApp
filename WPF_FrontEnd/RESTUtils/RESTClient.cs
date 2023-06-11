using RESTServer.Models;
using RestSharp;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace WPF_FrontEnd.RESTUtils
{
    /// <summary>
    /// Class that in reality represents the wrapper for RestClient 
    /// </summary>
    public class RESTClient
    {
        /// <summary>
        /// string for url of service
        /// </summary>
        private string url = "https://localhost:7237/";

        /// <summary>
        /// string for holding route name for userController
        /// </summary>
        private string userRoute = "User";

        /// <summary>
        /// string for holding route name for noteController
        /// </summary>
        private string noteRoute = "Note";

        /// <summary>
        /// Variable holding rest client
        /// </summary>
        RestClient client;

        /// <summary>
        /// Default ctor where we instaintiate rest client variable
        /// </summary>
        public RESTClient() => client = new RestClient();

        // user related requests

        /// <summary>
        /// Makes a request to get user by login
        /// </summary>
        /// <param name="login">Users login</param>
        /// <returns></returns>
        public User GetUserByLogin(string login)
        {
            var request = new RestRequest(url + userRoute);
            request.AddParameter("Login", login);
            string response = client.Get(request).Content.ToString();
            return JsonSerializer.Deserialize<List<User>>(response).FirstOrDefault();
        }

        /// <summary>
        /// Checking if user exist
        /// </summary>
        /// <param name="login">User login</param>
        /// <returns></returns>
        public bool UserExists(string login)
        {
            var request = new RestRequest(url + userRoute);
            request.AddParameter("Login", login);
            string response = client.Get(request).Content.ToString();
            if(response == "[]")
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Inserts the user using post
        /// </summary>
        /// <param name="user">User that has to be sent</param>
        public void InsertPerson(User user)
        {
            var request = new RestRequest(url + userRoute, Method.Post);
            request.AddJsonBody(user);
            client.Post(request);
        }

        // notes related requests

        /// <summary>
        /// Method that is used for getting all user notes from DB
        /// </summary>
        /// <param name="userID">User's ID</param>
        /// <returns></returns>
        public List<Note> GetNotesByUserID(int userID)
        {
            var request = new RestRequest(url + noteRoute);
            request.AddParameter("userID", userID);
            string response = client.Get(request).Content.ToString();
            return JsonSerializer.Deserialize<List<Note>>(response);
        }

        /// <summary>
        /// Method that posts note straight to the DB xd xd xd
        /// </summary>
        /// <param name="note">Note that we want to send</param>
        public void InsertNote(Note note)
        {
            var request = new RestRequest(url + noteRoute);
            request.AddJsonBody(note);
            client.Post(request);
        }

        /// <summary>
        /// Method for deleting the note
        /// </summary>
        /// <param name="noteID">ID of Note in quesion</param>
        public void DeleteNote(int noteID)
        {
            var request = new RestRequest(url + noteRoute);
            request.AddParameter("ID", noteID);
            client.Delete(request);
        }

        /// <summary>
        /// Method for deleting the note
        /// </summary>
        /// <param name="noteID">ID of Note in quesion</param>
        public void UpdateNote(Note note)
        {
            var request = new RestRequest(url + noteRoute);
            request.AddJsonBody(note);
            client.Patch(request);
        }
    }
}
