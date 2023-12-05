using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhereToDataAccess.Entities;

namespace WhereToDataAccess.Interfaces
{
    public interface IUserRepository
    {
        IQueryable<User> GetAll();
        User Get(int id);
        void Create(User item);
        void Update(User item);
        void Delete(int id);
    }
}
