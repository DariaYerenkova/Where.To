using Azure.Core;
using Azure.Storage.Queues;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
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
        private readonly IQueueMessageService<PayForTourDto> queueMessageService;

        public PaymentController(IUserTourService userTourService, IQueueMessageService<PayForTourDto> queueMessageService)
        {
            this.userTourService = userTourService;
            this.queueMessageService = queueMessageService;
        }

        [HttpPost]
        public async Task<ActionResult> PayForTour(PayForTourDto model)
        {
            userTourService.PayForTour(model);

            //send message to the queue for further processing 
            await queueMessageService.SendMessageToQueueAsync(queueMessageService.GenerateSerializedMesssageForQueue(model));

            return Created(nameof(PaymentController.PayForTour), model);
        }
    }
}
