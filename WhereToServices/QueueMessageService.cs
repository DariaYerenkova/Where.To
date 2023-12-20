using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhereToDataAccess.Interfaces;
using WhereToServices.DTOs;
using WhereToServices.Interfaces;

namespace WhereToServices
{
    public class QueueMessageService : IQueueMessageService
    {
        private readonly IUnitOfWork uow;

        public QueueMessageService(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public Whereto_booking_message GenerateMessageForWhereTo_BookingQueue(int userId, int tourId)
        {
            var user = uow.Users.Get(userId);
            var tour = uow.Tours.Get(tourId);

            Whereto_booking_message queueMessage = new Whereto_booking_message(user.FirstName, user.LastName, user.Passport, tour.TourName);
            return queueMessage;
        }
    }
}
