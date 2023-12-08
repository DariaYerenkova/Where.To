using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WhereToDataAccess.Entities;
using WhereToServices.Interfaces;

namespace WhereTo.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService userService;

        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            var user = userService.GetUserById(id);

            return Ok(user);

        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            var users = userService.GetUsers();

            return Ok(users);
        }

        [HttpPost]
        public IActionResult CreateUser([FromBody] User user)
        {
            userService.CreateUser(user);

            return Created(nameof(UsersController.GetUser), user);
        }
    }
}
