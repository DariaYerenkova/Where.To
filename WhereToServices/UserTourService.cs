using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhereToDataAccess.Entities;
using WhereToDataAccess.Interfaces;
using WhereToServices.Interfaces;

namespace WhereToServices
{
    public class UserTourService : IUserTourService
    {
        private readonly IUnitOfWork uow;

        public UserTourService(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public void RegisterUserForTour(UserTour userTour)
        {
            userTour.DateRegistered = DateTime.Now.Date;
            userTour.IsPayed = false;
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
    }
}
