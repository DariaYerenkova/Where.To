using Azure.Messaging.EventGrid;
using Azure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhereToServices.Interfaces;
using WhereToServices.DTOs;

namespace WhereToServices
{
    public class EventPublisherService : IEventPublisherService<BookingFinishedEvent>
    {
        EventGridPublisherClient client;

        public EventPublisherService(EventGridPublisherClient client)
        {
            this.client = client;
        }

        public async Task PublishEventAsync(BookingFinishedEvent model)
        {
            var eventData = new
            {
                Id = Guid.NewGuid().ToString(),
                Subject = "CustomSubject",
                EventType = "custom.event.type",
                Data = new
                {
                    TourId = model.TourId,
                    Passport = model.UserPassport
                }
            };

            List<EventGridEvent> eventsList = new List<EventGridEvent>
            {
            new EventGridEvent(
                subject: eventData.Subject,
                eventType: eventData.EventType,
                dataVersion: "1.0",
                data: eventData.Data)
            };

            await client.SendEventsAsync(eventsList);
        }
    }
}
