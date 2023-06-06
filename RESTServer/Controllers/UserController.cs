using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RESTServer.DatabaseOperators;
using RESTServer.Models;

namespace RESTServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        UserOperator userOperator = new UserOperator();
        [HttpGet(Name = "ByLogin")]
        public async Task<IEnumerable<User>> Get(string login)
        {
            Task<IEnumerable<User>> IuserTask = userOperator.GetUserByLogin(login);
            IEnumerable<User> Iuser = await IuserTask;
            return Iuser;
        }
        [HttpPut(Name = "InsertUser")]
        public void Put(string FirstName, string LastName, string Login, string Password, string Email)
        {
            User user = new User(FirstName, LastName, Login, Password, Email);
            userOperator.InsertUser(user);
        }
    }
}
