using KD.Common.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KD.Services.User
{
    public interface IUserService
    {
        IEnumerable<UserModel> GetUsers();
        UserModel GetUserById(Guid userId);
        void DeleteUser(Guid userId);
        UserModel CreateUser(UserModel user);
        UserModel UpdateUser(UserModel user);
        UserModel ValidateUser(string username, string password);
    }
}
