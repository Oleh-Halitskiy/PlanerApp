using RESTServer.DataUtils;
using RESTServer.Models;

namespace RESTServer.DatabaseOperators
{
    /// <summary>
    /// Provides operations related to user management.
    /// </summary>
    public class UserOperator
    {
        private DataAccess dataAccess = new DataAccess();

        /// <summary>
        /// Retrieves a collection of users based on the specified login.
        /// </summary>
        /// <param name="login">The login to search for.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains the collection of users.</returns>
        public async Task<IEnumerable<User>> GetUserByLogin(string login)
        {
            User user = new User();
            user.Login = login;
            Task<IEnumerable<User>> TaskUser = dataAccess.LoadData<User, dynamic>("dbo.spGetUserByLogin", new { user.Login });
            IEnumerable<User> IUser = await TaskUser;
            return IUser;
        }

        /// <summary>
        /// Inserts a new user into the database.
        /// </summary>
        /// <param name="user">The user object to insert.</param>
        public async void InsertUser(User user) => await dataAccess.SaveData("dbo.spInsertUser", new { user.FirstName, user.LastName, user.Login, user.Password, user.Email });
    }
}