using Microsoft.AspNetCore.Mvc;
using LaundryService;

namespace LaundryAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        UserGetServices user_GetServices;
        UserTransactionServices user_TransactionServices;

        public UserController()
        {
            user_GetServices = new UserGetServices();
            user_TransactionServices = new UserTransactionServices();

        }
        [HttpGet]
        public IEnumerable<User> GetUsers()
        {
            var activeusers = user_GetServices.GetUsersByStatus(1);

            List<User> users = new List<User>();

            foreach (var item in activeusers)
            {
                users.Add(new User { name = item.name, clWeight = item.clWeight });
            }

            return users;
        }
        [HttpPost]
        public JsonResult AddUser(User request)
        {
            var result = user_TransactionServices.CreateUser(request.name, request.clWeight);

            return new JsonResult(result);
        }
        [HttpPatch]
        public JsonResult UpdateUser(User request)
        {
            var result = user_TransactionServices.UpdateUser(request.name, request.clWeight);

            return new JsonResult(result);
        }
        [HttpDelete]
        public JsonResult DeleteUser(User request)
        {
            var DeleteUserss = new LaundryModel.User
            {
                name = request.name
            };

            var result = user_GetServices.DeleteUser(DeleteUserss);

            return new JsonResult(result);
        }
    }
}
