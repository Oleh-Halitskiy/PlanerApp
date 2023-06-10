using RESTServer.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;

namespace WPF_FrontEnd.RESTUtils
{
    public class RESTClient
    {
        private string url = "https://localhost:7237/";
        private string userRoute = "User";
        private string noteRoute = "Note";
        RestClient client;
        public RESTClient()
        {
            client = new RestClient();
        }
        // user related requests
        public User GetUserByLogin(string login)
        {
            var request = new RestRequest(url + userRoute);
            request.AddParameter("Login", login);
            string response = client.Get(request).Content.ToString();
            return JsonSerializer.Deserialize<List<User>>(response).FirstOrDefault();
        }
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
        public void InsertPerson(User user)
        {
            var request = new RestRequest(url + userRoute, Method.Post);
            request.AddJsonBody(user);
            client.Post(request);
        }

        // notes related requests
        public List<Note> GetNotesByUserID(int userID)
        {
            var request = new RestRequest(url + noteRoute);
            request.AddParameter("userID", userID);
            string response = client.Get(request).Content.ToString();
            return JsonSerializer.Deserialize<List<Note>>(response);
        }
    }
}
