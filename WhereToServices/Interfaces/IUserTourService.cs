using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhereToDataAccess.Entities;

namespace WhereToServices.Interfaces
{
    public interface IUserTourService
    {
        public void RegisterUserForTour(UserTour userTour);
        public void RemoveUserFromTour(UserTour userTour);
    }
}
