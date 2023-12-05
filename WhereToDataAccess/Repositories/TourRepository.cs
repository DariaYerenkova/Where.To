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
    public class TourRepository : ITourRepository
    {
        private readonly WhereToDataContext context;

        public TourRepository(WhereToDataContext context)
        {
            this.context = context;
        }

        public void Create(Tour item)
        {
            context.Add(item);
        }

        public void Delete(int id)
        {
            Tour tour = context.Tours.Find(id);
            if (tour != null)
            {
                context.Tours.Remove(tour);
            }
        }

        public Tour Get(int id)
        {
            return context.Tours.Include(t => t.UserTours).Include(t => t.TourCities).FirstOrDefault(t => t.Id == id);
        }

        public IQueryable<Tour> GetAll()
        {
            return context.Tours.Include(t => t.UserTours).Include(t => t.TourCities);
        }

        public void Update(Tour item)
        {
            context.Entry(item).State = EntityState.Modified;
        }
    }
}
