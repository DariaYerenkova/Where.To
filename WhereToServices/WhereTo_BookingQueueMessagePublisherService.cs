using Azure.Storage.Queues;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WhereToDataAccess.Entities;
using WhereToDataAccess.Interfaces;
using WhereToServices.DTOs;
using WhereToServices.Interfaces;

namespace WhereToServices
{
    public class WhereTo_BookingQueueMessagePublisherService : IQueueMessagePublisher<PayForTourDto>
    {
        private readonly IUnitOfWork uow;
        private readonly QueueClient queueClient;

        public WhereTo_BookingQueueMessagePublisherService(IUnitOfWork uow, QueueServiceClient queueClient)
        {
            this.uow = uow;
            this.queueClient = queueClient.GetQueueClient("whereto-booking");
        }

        public async Task SendMessageToQueueAsync(string message)
        {
            await queueClient.SendMessageAsync(message);
        }

        public string GenerateSerializedMesssageForQueue(PayForTourDto model)
        {
            //Generate message for queue
            var message = GenerateMessageForWhereTo_BookingQueue(model.UserId, model.TourId);
            var serializedMessage = JsonSerializer.Serialize(message);

            return serializedMessage;
        }

        private WhereToBookingMessage GenerateMessageForWhereTo_BookingQueue(int userId, int tourId)
        {
            var user = uow.Users.Get(userId);
            var tour = uow.Tours.Get(tourId);

            WhereToBookingMessage queueMessage = new WhereToBookingMessage(user.FirstName, user.LastName, user.Passport, tour.Id);
            return queueMessage;
        }
    }
}
