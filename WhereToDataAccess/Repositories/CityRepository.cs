using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhereToDataAccess.Entities;
using WhereToDataAccess.Interfaces;

namespace WhereToDataAccess.Repositories
{
    public class CityRepository : ICityRepository
    {
        private readonly WhereToDataContext context;

        public CityRepository(WhereToDataContext context)
        {
            this.context = context;
        }

        public void Create(City item)
        {
            context.Cities.Add(item);
        }

        public void Delete(int id)
        {
            City city = context.Cities.Find(id);
            if (city != null)
            {
                context.Cities.Remove(city);
            }
        }

        public City Get(int id)
        {
            return context.Cities.Include(c => c.TourCities).FirstOrDefault(c => c.Id == id);
        }

        public IQueryable<City> GetAll()
        {
            return context.Cities.Include(c => c.TourCities);
        }

        public void Update(City item)
        {
            context.Entry(item).State = EntityState.Modified;
        }
    }
}
