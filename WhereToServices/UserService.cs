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
    public class UserService:IUserService
    {
        private readonly IUnitOfWork uow;

        public UserService(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public void CreateUser(User user)
        {
            uow.Users.Create(user);
            uow.Save();
        }

        public void Delete(int id)
        {
            uow.Users.Delete(id);
            uow.Save();
        }

        public IEnumerable<User> GetUsers()
        {
            var users = uow.Users.GetAll();

            return users.ToList();
        }

        public User GetUserById(int id)
        {
            var user = uow.Users.Get(id);
            return user;
        }
        public void Update(User user)
        {
            uow.Users.Update(user);
            uow.Save();
        }
    }
}
