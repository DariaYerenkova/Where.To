using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WhereToDataAccess.Entities;
using WhereToServices.DTOs;
using WhereToServices.Interfaces;

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
        public ActionResult PayForTour(PayForTourDto model)
        {
            userTourService.PayForTour(model);

            return Created(nameof(PaymentController.PayForTour), model);
        }
    }
}
