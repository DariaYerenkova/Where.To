using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhereToDataAccess.WhereTo_BookingEntities;

namespace WhereToDataAccess.WhereTo_BookingInterfaces
{
    public interface IUserFlightRepository
    {
        IQueryable<UserFlight> GetAll();
        UserFlight Get(int id);
        void Create(UserFlight item);
        void Update(UserFlight item);
        void Delete(int id);
    }
}
