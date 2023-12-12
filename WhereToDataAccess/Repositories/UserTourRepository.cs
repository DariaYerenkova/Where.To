using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhereToDataAccess.Entities;
using WhereToDataAccess.Interfaces;

namespace WhereToDataAccess.Repositories
{
    internal class UserTourRepository : IUserTourRepository
    {
        private readonly WhereToDataContext context;

        public UserTourRepository(WhereToDataContext context)
        {
            this.context = context;
        }

        public void Create(UserTour item)
        {
            context.UserTours.Add(item);
        }

        public void Delete(int userId, int tourId)
        {
            UserTour userTour = context.UserTours.FirstOrDefault(ut=> ut.UserId == userId && ut.TourId == tourId);
            if (userTour != null)
            {
                context.UserTours.Remove(userTour);
            }
        }

        public async Task DeleteAsync(int userId, int tourId)
        {
            UserTour userTour = await context.UserTours.FirstOrDefaultAsync(ut => ut.UserId == userId && ut.TourId == tourId);
            if (userTour != null)
            {
                context.UserTours.Remove(userTour);
            }
        }

        public async Task<List<UserTour>> GetNotPayedAndRegisteredEarlierUserToursAsync(DateTime date)
        {
            var overdueBookings = await context.UserTours
                        .Where(ut => ut.DateRegistered < date && !ut.IsPayed)
                        .ToListAsync();

            return overdueBookings;
        }

        public void Update(UserTour item)
        {
            throw new NotImplementedException();
        }
    }
}
