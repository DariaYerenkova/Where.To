using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhereToDataAccess.WhereTo_BookingEntities;

namespace WhereToDataAccess.WhereTo_BookingInterfaces
{
    public interface IUserRepository
    {
        IQueryable<User> GetAll();
        User Get(int id);
        User Create(User item);
        void Update(User item);
        void Delete(int id);
    }
}
