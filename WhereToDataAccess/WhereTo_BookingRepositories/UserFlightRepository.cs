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
    public class UserFlightRepository : IUserFlightRepository
    {
        private readonly WhereTo_BookingDataContext context;

        public UserFlightRepository(WhereTo_BookingDataContext context)
        {
            this.context = context;
        }

        public void Create(UserFlight item)
        {
            context.UserFlights.Add(item);
        }

        public void Delete(int id)
        {
            UserFlight userFlight = context.UserFlights.Find(id);
            if (userFlight != null)
            {
                context.UserFlights.Remove(userFlight);
            }
        }

        public UserFlight Get(int id)
        {
            return context.UserFlights.Include(u => u.User).FirstOrDefault(u => u.Id == id);
        }

        public IQueryable<UserFlight> GetAll()
        {
            return context.UserFlights.Include(u => u.User);
        }

        public void Update(UserFlight item)
        {
            context.Entry(item).State = EntityState.Modified;
        }
    }
}
