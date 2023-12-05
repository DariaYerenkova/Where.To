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
            try
            {
                var user = userService.GetUserById(id);

                if (user == null)
                {
                    return NotFound();
                }

                return Ok(user);
            }
            catch (Exception e)
            {

                return BadRequest();
            }
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            try
            {
                var users = userService.GetUsers();

                if (users == null)
                {
                    return NotFound();
                }

                return Ok(users);
            }
            catch (Exception e)
            {

                return BadRequest();
            }
        }

        [HttpPost]
        public IActionResult CreateUser([FromBody] User user)
        {
            try
            {
                userService.CreateUser(user);

                return Created(nameof(UsersController.GetUser), user);
            }
            catch (Exception e)
            {

                return BadRequest();
            }
        }
    }
}
