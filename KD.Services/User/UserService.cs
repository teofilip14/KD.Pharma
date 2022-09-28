using KD.Common.Model.Automapper;
using KD.Common.Model.Models;
using KD.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KD.Services.User
{
    public class UserService : IUserService
    {
        #region Fields
        private readonly IRepository<Core.DomainModels.User> userRepository;

        public UserService(IRepository<Core.DomainModels.User> userRepository)
        {
            this.userRepository = userRepository;
        }
        #endregion
        #region Methods

        public IEnumerable<UserModel> GetUsers()
        {
            var users = userRepository.Table.Select(x => x.ToModel()).ToList();
            return users;
        }

        public UserModel CreateUser(UserModel user)
        {
            if (user == null)
                return null;

            KD.Core.DomainModels.User userEntity = user.ToEntity();
            userEntity.UserId = Guid.Empty;
            userRepository.Insert(userEntity);

            UserModel createdUser = GetUserById(userEntity.UserId);
            return createdUser;
        }

        public UserModel GetUserById(Guid userId)
        {
            var user = userRepository.Table.FirstOrDefault(s => s.UserId == userId);
            return user.ToModel();
        }

        public UserModel UpdateUser(UserModel user)
        {
            if (user == null) return null;
            var databaseEntity = userRepository.TableNoTracking.FirstOrDefault(s => s.UserId == user.UserId);
            if (databaseEntity == null)
                return null;

            userRepository.Update(user.ToEntity());
            return GetUserById(user.UserId);
        }

        public void DeleteUser(Guid userId)
        {
            var databaseEntity = userRepository.Table.FirstOrDefault(s => s.UserId == userId);
            if (databaseEntity == null)
                return;

            userRepository.Delete(databaseEntity);
        }

        public UserModel ValidateUser(string username, string password)
        {
            try
            {
                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                    throw new ArgumentNullException("username or password is null or empty");
                var userEntity = userRepository.TableNoTracking.FirstOrDefault(s => s.Username == username && s.Password == password);
                return userEntity.ToModel();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }
        #endregion
    }
}
