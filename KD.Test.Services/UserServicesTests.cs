using KD.Core.Data;
using KD.Core.DomainModels;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using KD.Common.Model.Automapper;
using KD.Services.User;
using KD.Common.Model.Models;

namespace KD.Test.Services
{
    public class UserServicesTests
    {
        private EFCoreRepository<User> userRepository;
    
        [OneTimeSetUp]
        public void Setup()
        {
            //Set up DbContext
            var options = new DbContextOptionsBuilder<PharmacyContext>();
            options.UseSqlServer("Server=.\\SQLEXPRESS;Database=Pharmacy;Trusted_Connection=True;");
            PharmacyContext _dbContext = new PharmacyContext(options.Options);

            //Set up Repos
            userRepository = new(_dbContext);

            //Set up Automapper
            AutoMapperConfiguration.Init();
            AutoMapperConfiguration.MapperConfiguration.AssertConfigurationIsValid();
        }

        [Test]
        public void GetUsersTest()
        {
            //arrange
            UserService service = GetService();

            //act
            var users = service.GetUsers();

            //assert
            Assert.That(users.Any());
        }

        [Test]
        public void CreateUserTest()
        {
            //arrange
            UserService service = GetService();
            Guid createdUserId = Guid.Empty;
            try
            {
                User user = CreateUserModel("ADMIN", "PASS");

                //act
                UserModel createdUser = service.CreateUser(user.ToModel());
                createdUserId = createdUser.UserId;

                //assert
                Assert.That(createdUser != null);
                Assert.That(createdUser?.Username == user.Username);
                Assert.That(createdUser?.Password == user.Password);            
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                service.DeleteUser(createdUserId);
            }
        }

        [Test]
        public void DeleteUserTest()
        {
            //arrange
            UserService service = GetService();
            UserModel user = CreateUserModel("ADMIN", "PASS").ToModel();
            UserModel createdUser = service.CreateUser(user);

            //act
            service.DeleteUser(createdUser.UserId);

            //assert
            var deletedUser = service.GetUserById(createdUser.UserId);
            Assert.That(deletedUser == null);
        }

        [Test]
        public void UpdateUserTest()
        {
            //arrange
            UserService service = GetService();
            UserModel user = CreateUserModel("ADMIN", "PASS").ToModel();
            UserModel createdUser = service.CreateUser(user);

            //act
            Setup();
            service = GetService();
            createdUser.Username = "ADMIN";
            service.UpdateUser(createdUser);

            //assert
            var updatedUser = service.GetUserById(createdUser.UserId);
            Assert.That(updatedUser != null);
        }

        private UserService GetService()
        {
            return new(userRepository);
        }
        private User CreateUserModel(string username, string password)
        {
            KD.Core.DomainModels.User user = new()
            {
                Username = username,
                Password = password,
            };
            return user;
        }
    }
}