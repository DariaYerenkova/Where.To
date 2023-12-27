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
    public class UserRepository : IUserRepository
    {
        private readonly WhereTo_BookingDataContext context;
        public UserRepository(WhereTo_BookingDataContext _context)
        {
            this.context = _context;
        }

        public User Create(User item)
        {
            return context.Users.Add(item).Entity;
        }

        public void Delete(int id)
        {
            User user = context.Users.Find(id);
            if (user != null)
            {
                context.Users.Remove(user);
            }
        }

        public User Get(int id)
        {
            return context.Users.Include(u => u.UserFlights).Include(u=>u.UserHotels).FirstOrDefault(u => u.Id == id);
        }

        public IQueryable<User> GetAll()
        {
            return context.Users.Include(u => u.UserFlights).Include(u => u.UserHotels);
        }

        public void Update(User item)
        {
            context.Entry(item).State = EntityState.Modified;
        }
    }
}
