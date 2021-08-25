using Dapper;
using Project2.Common.Entity;
using Project2.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2.BussinessLogic.Service
{
    public class UserInfoDataAccessLayer
    {
        UserContext _useContext;
        public UserInfoDataAccessLayer()
        {
            _useContext = new UserContext();
        }
        public User GetLoginUser(User login)
        {
            var userInfo = new User();
            userInfo.UserId = Guid.NewGuid();
            string sqlCommand = "SELECT * FROM user WHERE account = @account and password = @password;";

            var parameters = new DynamicParameters();
            parameters.Add("@account", login.Account);
            parameters.Add("@password", login.Password);
            userInfo = _useContext.DbConnection.QueryFirstOrDefault<User>(sqlCommand, parameters);
            return userInfo;
        }

    }
}
