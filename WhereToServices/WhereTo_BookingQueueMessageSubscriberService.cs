using Azure.Storage.Queues;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WhereToDataAccess.WhereTo_BookingInterfaces;
using WhereToServices.DTOs;
using WhereToServices.Interfaces;

namespace WhereToServices
{
    public class WhereTo_BookingQueueMessageSubscriberService : IQueueMessageSubscriber<WhereToBookingMessage>
    {
        private readonly QueueClient queueClient;
        private Azure.Response<Azure.Storage.Queues.Models.QueueMessage> response;

        public WhereTo_BookingQueueMessageSubscriberService(QueueClient queueClient)
        {
            this.queueClient = queueClient;
        }

        public async Task<WhereToBookingMessage> ReadMessageFromQueueAsync()
        {
            WhereToBookingMessage deserializedMessage = null;
            response = await queueClient.ReceiveMessageAsync();

            if (response.Value != null) 
            {
                deserializedMessage = DeserializeMessage(response.Value.Body);
            }

            return deserializedMessage;
        }

        public async Task DeleteMessageAsync()
        {
            if (response != null)
            {
                await queueClient.DeleteMessageAsync(response.Value.MessageId, response.Value.PopReceipt);
            }
        }

        private WhereToBookingMessage DeserializeMessage(BinaryData message)
        {
            return JsonSerializer.Deserialize<WhereToBookingMessage>(message);
        }
    }
}
