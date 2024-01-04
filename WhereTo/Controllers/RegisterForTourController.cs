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
        public ActionResult RegisterUserForTour(UserTour request)
        {
            userTourService.RegisterUserForTour(request);

            return Created(nameof(RegisterForTourController.RegisterUserForTour), request);
        }

        [HttpPost("RemoveRegistration")]
        public ActionResult RemoveUserFromTour(UserTour request)
        {
            userTourService.RemoveUserFromTour(request);

            return Ok();
        }

        [HttpPost("ApplyForTour")]
        public ActionResult ApplyForTour()
        {
            return Ok("you hit apply for a Tour endpoint");
        }
    }
}
