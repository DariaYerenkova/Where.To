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

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public UserTour Get(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<UserTour> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Update(UserTour item)
        {
            throw new NotImplementedException();
        }
    }
}
