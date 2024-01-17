using Microsoft.EntityFrameworkCore;
using WhereToDataAccess.Entities;
using WhereToDataAccess.Interfaces;
//using static WhereToDataAccess.Interfaces.IRepository;

namespace WhereToDataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly WhereToDataContext context;

        public UserRepository(WhereToDataContext context)
        {
            this.context = context;
        }

        public void Create(User item)
        {
            context.Users.Add(item);
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
            return context.Users.Include(u => u.UserTours).FirstOrDefault(u => u.Id == id);
        }

        public User GetByPassport(string passport)
        {
            return context.Users.Include(u => u.UserTours).FirstOrDefault(u => u.Passport == passport);
        }

        public IQueryable<User> GetAll()
        {
            return context.Users.Include(u => u.UserTours);
        }

        public void Update(User item)
        {
            context.Entry(item).State = EntityState.Modified;
        }
    }
}
