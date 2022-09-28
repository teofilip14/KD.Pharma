using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using KD.Common.Model.Models;
using KD.Services.User;
using System.Text;

namespace KD.Web.API.Controllers
{
    [Authorize]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly IUserService userService;
        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }

        [AllowAnonymous]
        [Route("/api/Users")]
        [HttpGet]
        public IEnumerable<UserModel> Get()
        {
            try
            {
                var users = userService.GetUsers();

                return users;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        [Route("/api/Users/{userId}")]
        [HttpGet]
        public UserModel Get(Guid userId)
        {
            try
            {
                var user = userService.GetUserById(userId);

                return user;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        [Route("/api/Users/{userId}")]
        [HttpDelete]
        public void Delete(Guid userId)
        {
            try
            {
                userService.DeleteUser(userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        [Route("/api/Users")]
        [HttpPost]
        public UserModel Create([FromBody] UserModel user)
        {
            try
            {
                UserModel createdUser = userService.CreateUser(user);
                return createdUser;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        [Route("/api/Users/{userId}")]
        [HttpPut]
        public UserModel Update(Guid userId, [FromBody] JsonElement user)
        {
            try
            {
                var userModel = JsonSerializer.Deserialize<UserModel>(user);
                UserModel updatedUser = userService.UpdateUser(userModel);
                return updatedUser;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }
        [AllowAnonymous]
        [Route("/api/ValidateUser")]
        [HttpPost]
        public bool ValidateUser([FromBody] string userDetails)
        {

            if (userDetails is not null)
            {
                byte[] data = Convert.FromBase64String(userDetails);
                string decodedString = Encoding.UTF8.GetString(data);

                string[] detail = decodedString.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                string username = detail[0];
                string password = detail[1];

                var user = userService.ValidateUser(username, password);
                if (user is not null)
                {
                    return true;
                }
            }
            return false;
        }
    }
}