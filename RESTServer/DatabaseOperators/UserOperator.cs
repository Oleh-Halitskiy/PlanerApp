using RESTServer.DataUtils;
using RESTServer.Models;

namespace RESTServer.DatabaseOperators
{
    public class UserOperator
    {
        private DataAccess dataAccess = new DataAccess();
        public async Task<IEnumerable<User>> GetUserByLogin(string login)
        {
            User user = new User();
            user.Login = login;
            Task<IEnumerable<User>> TaskUser = dataAccess.LoadData<User, dynamic>("dbo.spGetUserByLogin", new { user.Login });
            IEnumerable<User> IUser = await TaskUser;
            return IUser;
        }
        public async void InsertUser(User user) => await dataAccess.SaveData("dbo.spInsertUser", new { user.FirstName, user.LastName, user.Login, user.Password, user.Email });
    }
}