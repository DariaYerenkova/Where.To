using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhereToDataAccess.Entities;

namespace WhereToServices.Interfaces
{
    public interface IUserService
    {
        public IEnumerable<User> GetUsers();
        public User GetUserById(int id);
        public void CreateUser(User user);
        public void Update(User user);
        public void Delete(int id);

    }
}
