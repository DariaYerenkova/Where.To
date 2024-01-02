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
        private string messageId;
        private string popReceipt;

        public WhereTo_BookingQueueMessageSubscriberService(QueueClient queueClient)
        {
            this.queueClient = queueClient;
        }

        public async Task<WhereToBookingMessage> ReadMessageFromQueueAsync()
        {
            WhereToBookingMessage deserializedMessage = null;
            var response = await queueClient.ReceiveMessageAsync();

            if (response.Value != null) 
            {
                messageId = response.Value.MessageId;
                popReceipt = response.Value.PopReceipt;
                deserializedMessage = DeserializeMessage(response.Value.Body);
            }

            return deserializedMessage;
        }

        public async Task DeleteMessageAsync()
        {
            if (messageId != null && popReceipt != null)
            {
                await queueClient.DeleteMessageAsync(messageId, popReceipt);
            }
        }

        private WhereToBookingMessage DeserializeMessage(BinaryData message)
        {
            return JsonSerializer.Deserialize<WhereToBookingMessage>(message);
        }
    }
}
