using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WhereToDataAccess.Entities;
using WhereToServices.Interfaces;

namespace WhereTo.Controllers
{
    [Route("api/Register")]
    [ApiController]
    public class RegisterForTourController : ControllerBase
    {
        private readonly IUserTourService userTourService;

        public RegisterForTourController(IUserTourService userTourService)
        {
                this.userTourService = userTourService;
        }

        [HttpPost]
        public ActionResult RegisterUserForTour([FromBody] UserTour request)
        {
            try
            {
                userTourService.RegisterUserForTour(request);

                return Created(nameof(RegisterForTourController.RegisterUserForTour), request);
            }
            catch (Exception e)
            {

                return BadRequest();
            }
        }

        [HttpPost("RemoveRegistration")]
        public ActionResult RemoveUserFromTour([FromBody] UserTour request)
        {
            try
            {
                userTourService.RemoveUserFromTour(request);

                return Ok();
            }
            catch (Exception e)
            {

                return BadRequest();
            }
        }
    }
}
