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

        public WhereTo_BookingUnitOfWork(WhereTo_BookingDataContext whereTo_BookingDataContext)
        {
            this.whereTo_BookingDataContext = whereTo_BookingDataContext;
        }

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
            whereTo_BookingDataContext.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await whereTo_BookingDataContext.SaveChangesAsync();
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    whereTo_BookingDataContext.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
