using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhereToDataAccess.Entities;
using WhereToDataAccess.Interfaces;

namespace WhereToDataAccess.Repositories
{
    public class TourCityRepository : ITourCityRepository
    {
        private readonly WhereToDataContext context;

        public TourCityRepository(WhereToDataContext context)
        {
            this.context = context;
        }

        public void Create(TourCity item)
        {
            context.TourCities.Add(item);
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public TourCity Get(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TourCity> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Update(TourCity item)
        {
            throw new NotImplementedException();
        }
    }
}
