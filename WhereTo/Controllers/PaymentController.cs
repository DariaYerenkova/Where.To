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
        private readonly IQueueMessageService queueMessageService;
        private readonly QueueClient queueClient;

        public PaymentController(IUserTourService userTourService, QueueClient queueClient, IQueueMessageService queueMessageService)
        {
            this.userTourService = userTourService;
            this.queueClient = queueClient;
            this.queueMessageService = queueMessageService;
        }

        [HttpPost]
        public async Task<ActionResult> PayForTour(PayForTourDto model)
        {
            userTourService.PayForTour(model);

            //Generate message for queue
            var message = queueMessageService.GenerateMessageForWhereTo_BookingQueue(model.UserId, model.TourId);

            //send message to the queue for further processing 
            var serializedMessage = JsonSerializer.Serialize(message);
            await queueClient.SendMessageAsync(serializedMessage);

            return Created(nameof(PaymentController.PayForTour), model);
        }
    }
}
