using AutoMapper;
using Azure.Messaging.EventGrid;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhereToDataAccess.Entities;
using WhereToDataAccess.Enums;
using WhereToDataAccess.Interfaces;
using WhereToServices.DTOs;
using WhereToServices.Interfaces;

namespace WhereToServices
{
    public class UserTourService : IUserTourService
    {
        private readonly IQueueMessageSubscriber<BookingFinishedEvent> queueMessageSubscriber;
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;

        public UserTourService(IUnitOfWork uow, IMapper mapper, IQueueMessageSubscriber<BookingFinishedEvent> queueMessageSubscriber)
        {
            this.mapper = mapper;
            this.uow = uow;
            this.queueMessageSubscriber = queueMessageSubscriber;
        }

        public void RegisterUserForTour(UserTour userTour)
        {
            userTour.DateRegistered = DateTime.Now.Date;
            userTour.IsPayed = false;
            userTour.Status = UserTourStatus.Registered;
            uow.UserTours.Create(userTour);
            uow.Save();
        }
        public void RemoveUserFromTour(UserTour userTour)
        {
            uow.UserTours.Delete(userTour.UserId, userTour.TourId);
            uow.Save();
        }

        public async Task RemoveExpiredBookingsAsync()
        {
            // Retrieve bookings that are 3 days overdue and not payed
            var overdueBookings = await GetNotPayedAndOverdueUserToursAsync();

            // Process overdue bookings 
            foreach (var booking in overdueBookings)
            {
                await RemoveUserFromTourAsync(booking);
            }
        }

        private async Task RemoveUserFromTourAsync(UserTour userTour)
        {
            await uow.UserTours.DeleteAsync(userTour.UserId, userTour.TourId);
            await uow.SaveAsync();
        }

        private async Task<List<UserTour>> GetNotPayedAndOverdueUserToursAsync()
        {
            var threeDaysAgo = DateTime.UtcNow.AddDays(-3).Date;
            return await uow.UserTours.GetNotPayedAndRegisteredEarlierUserToursAsync(threeDaysAgo);
        }

        public void PayForTour(PayForTourDto payForTourDto)
        {
            //var userTour = mapper.Map<PayForTourDto, UserTour>(payForTourDto);
            var userTour = uow.UserTours.GetUserTourByUserIdAndTourId(payForTourDto.UserId, payForTourDto.TourId);
            userTour.IsPayed = true;
            userTour.Status = UserTourStatus.Processing;
            UpdateUserTour(userTour);
        }

        public async Task ApplyForTourAsync()
        {
            var bookingModel = await queueMessageSubscriber.ReadMessageFromQueueAsync();

            if (bookingModel != null)
            {
                var user = uow.Users.GetByPassport(bookingModel.Passport);
                var userTour = uow.UserTours.GetUserTourByUserIdAndTourId(user.Id, Int32.Parse(bookingModel.TourId));
                userTour.Status = UserTourStatus.Applied;
                UpdateUserTour(userTour);
                await queueMessageSubscriber.DeleteMessageAsync();
            }
        }

        private void UpdateUserTour(UserTour userTour)
        {
            uow.UserTours.Update(userTour);
            uow.Save();
        }
    }
}
