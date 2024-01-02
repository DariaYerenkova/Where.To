using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhereToDataAccess.WhereTo_BookingEntities;

namespace WhereToDataAccess.WhereTo_BookingInterfaces
{
    public interface IUserHotelRepository
    {
        IQueryable<UserHotel> GetAll();
        UserHotel Get(int id);
        void Create(UserHotel item);
        void Update(UserHotel item);
        void Delete(int id);
    }
}
