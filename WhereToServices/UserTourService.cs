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
    public class UserTourService : IUserTourService
    {
        private readonly IUnitOfWork uow;

        public UserTourService(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public void RegisterUserForTour(UserTour userTour)
        {
            userTour.DateRegistered = DateTime.Now.Date;
            userTour.IsPayed = false;
            uow.UserTours.Create(userTour);
            uow.Save();
        }
        public void RemoveUserFromTour(UserTour userTour)
        {
            uow.UserTours.Delete(userTour);
            uow.Save();
        }
    }
}
