using Microsoft.AspNetCore.Mvc;
using RESTServer.DatabaseOperators;
using RESTServer.Models;

namespace RESTServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    /// <summary>
    /// Provides API endpoints for user management.
    /// </summary>
    public class UserController : ControllerBase
    {
        UserOperator userOperator = new UserOperator();

        /// <summary>
        /// Retrieves a collection of users based on the specified login.
        /// </summary>
        /// <param name="login">The login to search for.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains the collection of users.</returns>
        [HttpGet(Name = "ByLogin")]
        public async Task<IEnumerable<User>> Get(string login)
        {
            Task<IEnumerable<User>> IuserTask = userOperator.GetUserByLogin(login);
            IEnumerable<User> Iuser = await IuserTask;
            return Iuser;
        }

        /// <summary>
        /// Inserts a new user into the database.
        /// </summary>
        /// <param name="userbody">The user object to insert.</param>
        [HttpPost(Name = "InsertUser")]
        public void Post([FromBody] User userbody) => userOperator.InsertUser(userbody);
    }
}
