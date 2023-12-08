using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WhereToDataAccess.Entities;

namespace WhereTo.Controllers
{
    [Route("api/payment")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IUserTourService userTourService;

        public PaymentController(IUserTourService userTourService)
        {
            this.userTourService = userTourService;
        }

        [HttpPost]
        public ActionResult PayForTour([FromBody] UserTour userTour)
        {
            userTourService.PayForTour(userTour);

            return Created(nameof(PaymentController.PayForTour), userTour);
        }
    }
}
