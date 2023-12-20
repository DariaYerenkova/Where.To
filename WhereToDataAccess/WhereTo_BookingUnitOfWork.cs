using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhereToDataAccess.WhereTo_BookingInterfaces;
using WhereToDataAccess.WhereTo_BookingRepositories;

namespace WhereToDataAccess
{
    public class WhereTo_BookingUnitOfWork : IUnitOfWork
    {
        private readonly WhereTo_BookingDataContext whereTo_BookingDataContext;

        private IUserRepository userRepository;
        private IUserFlightRepository userFlightRepository;
        private IUserHotelRepository userHotelRepository;

        public IUserRepository Users
        {
            get
            {
                if (userRepository == null)
                    userRepository = new UserRepository(whereTo_BookingDataContext);
                return userRepository;
            }
        }

        public IUserFlightRepository UserFlights
        {
            get
            {
                if (userFlightRepository == null)
                    userFlightRepository = new UserFlightRepository(whereTo_BookingDataContext);
                return userFlightRepository;
            }
        }
        public IUserHotelRepository UserHotels
        {
            get
            {
                if (userHotelRepository == null)
                    userHotelRepository = new UserHotelRepository(whereTo_BookingDataContext);
                return userHotelRepository;
            }
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public Task SaveAsync()
        {
            throw new NotImplementedException();
        }
    }
}
