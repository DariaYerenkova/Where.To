using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhereToDataAccess.Entities;
using WhereToDataAccess.Interfaces;
using WhereToDataAccess.Repositories;

namespace WhereToDataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        #region private members

        private readonly WhereToDataContext whereToDataContext;

        private ITourRepository tourRepository;
        private IUserRepository userRepository;
        private ICityRepository cityRepository;
        private ITourCityRepository tourCityRepository;
        private IUserTourRepository userTourRepository;

        #endregion

        public ITourRepository Tours
        {
            get
            {
                if (tourRepository == null)
                    tourRepository = new TourRepository(whereToDataContext);
                return tourRepository;
            }
        }

        public IUserRepository Users
        {
            get
            {
                if (userRepository == null)
                    userRepository = new UserRepository(whereToDataContext);
                return userRepository;
            }
        }

        public ICityRepository Cities
        {
            get
            {
                if (cityRepository == null)
                    cityRepository = new CityRepository(whereToDataContext);
                return cityRepository;
            }
        }

        public ITourCityRepository TourCities
        {
            get
            {
                if (tourCityRepository == null)
                    tourCityRepository = new TourCityRepository(whereToDataContext);
                return tourCityRepository;
            }
        }

        public IUserTourRepository UserTours
        {
            get
            {
                if (userTourRepository == null)
                    userTourRepository = new UserTourRepository(whereToDataContext);
                return userTourRepository;
            }
        }

        public void Save()
        {
            whereToDataContext.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await whereToDataContext.SaveChangesAsync();
        }

        public UnitOfWork(WhereToDataContext whereToDataContext)
        {
            this.whereToDataContext = whereToDataContext;
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    whereToDataContext.Dispose();
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
