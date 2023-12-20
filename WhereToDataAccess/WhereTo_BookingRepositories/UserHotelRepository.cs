using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhereToDataAccess.WhereTo_BookingEntities;
using WhereToDataAccess.WhereTo_BookingInterfaces;

namespace WhereToDataAccess.WhereTo_BookingRepositories
{
    public class UserHotelRepository : IUserHotelRepository
    {
        private readonly WhereTo_BookingDataContext context;

        public UserHotelRepository(WhereTo_BookingDataContext context)
        {
            this.context = context;
        }

        public void Create(UserHotel item)
        {
            context.UserHotels.Add(item);
        }

        public void Delete(int id)
        {
            UserHotel userHotel = context.UserHotels.Find(id);
            if (userHotel != null)
            {
                context.UserHotels.Remove(userHotel);
            }
        }

        public UserHotel Get(int id)
        {
            return context.UserHotels.Include(u => u.User).FirstOrDefault(u => u.Id == id);
        }

        public IQueryable<UserHotel> GetAll()
        {
            return context.UserHotels.Include(u => u.User);
        }

        public void Update(UserHotel item)
        {
            context.Entry(item).State = EntityState.Modified;
        }
    }
}
