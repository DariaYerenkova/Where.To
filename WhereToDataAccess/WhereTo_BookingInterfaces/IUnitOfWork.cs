using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhereToDataAccess.WhereTo_BookingInterfaces
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }
        IUserFlightRepository UserFlights { get; }
        IUserHotelRepository UserHotels { get; }
        void Save();
        Task SaveAsync();
    }
}
