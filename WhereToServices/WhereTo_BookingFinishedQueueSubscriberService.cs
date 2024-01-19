using Azure.Messaging.EventGrid;
using Azure.Storage.Queues;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WhereToServices.DTOs;
using WhereToServices.Interfaces;

namespace WhereToServices
{
    public class WhereTo_BookingFinishedQueueSubscriberService : IQueueMessageSubscriber<BookingFinishedEvent>
    {
        private readonly QueueClient queueClient;
        private string messageId;
        private string popReceipt;

        public WhereTo_BookingFinishedQueueSubscriberService(QueueServiceClient queueClient)
        {
            this.queueClient = queueClient.GetQueueClient("whereto-booking-finished") ;
        }

        public async Task DeleteMessageAsync()
        {
            if (messageId != null && popReceipt != null)
            {
                await queueClient.DeleteMessageAsync(messageId, popReceipt);
            }
        }

        public async Task<BookingFinishedEvent> ReadMessageFromQueueAsync()
        {
            EventGridEvent deserializedMessage = null;
            BookingFinishedEvent bookingFinished = null;
            var response = await queueClient.ReceiveMessageAsync();
            byte[] decodedBytes = Convert.FromBase64String(Encoding.UTF8.GetString(response.Value.Body));
            string decodedMessage = Encoding.UTF8.GetString(decodedBytes);

            if (!string.IsNullOrEmpty(decodedMessage))
            {
                messageId = response.Value.MessageId;
                popReceipt = response.Value.PopReceipt;
                deserializedMessage = DeserializeMessage<EventGridEvent>(decodedMessage);
                bookingFinished = DeserializeMessage<BookingFinishedEvent>(deserializedMessage.Data.ToString());
            }

            return bookingFinished;
        }
        private T DeserializeMessage<T>(string message)
        {
            return JsonSerializer.Deserialize<T>(message);
        }
    }
}
