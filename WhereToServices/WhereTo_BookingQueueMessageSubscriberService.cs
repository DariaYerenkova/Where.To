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

        public WhereTo_BookingQueueMessageSubscriberService(QueueClient queueClient)
        {
            this.queueClient = queueClient;
        }

        public async Task<WhereToBookingMessage> ReadMessageFromQueueAsync()
        {
            WhereToBookingMessage deserializedMessage = null;
            var message = await queueClient.ReceiveMessageAsync();

            if (message.Value != null) 
            {
                deserializedMessage = DeserializeMessage(message.Value.Body);

                await queueClient.DeleteMessageAsync(message.Value.MessageId, message.Value.PopReceipt);
            }

            return deserializedMessage;
        }

        private WhereToBookingMessage DeserializeMessage(BinaryData message)
        {
            return JsonSerializer.Deserialize<WhereToBookingMessage>(message);
        }
    }
}
