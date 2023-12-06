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
            return context.Tours.Where(t => t.Id == id).Include(t => t.UserTours).Include(t => t.TourCities).ThenInclude(tc => tc.City).FirstOrDefault();
        }

        public IQueryable<Tour> GetAll()
        {
            return context.Tours.Include(t => t.UserTours).Include(t => t.TourCities).ThenInclude(tc => tc.City);
        }

        public IQueryable<Tour> GetToursByCity(int cityId)
        {
            var tours = context.Tours.Where(t => t.TourCities.Any(tc => tc.CityId == cityId));
            return tours;
        }

        public IQueryable<Tour> GetToursByDateRange(DateTime startDate, DateTime endDate)
        {
            var toursInRange = context.Tours.Where(tour =>
            (EF.Functions.DateDiffDay(startDate, tour.StartDate) >= 0 && EF.Functions.DateDiffDay(tour.StartDate, endDate) >= 0) || // Tours starting within the range
            (EF.Functions.DateDiffDay(startDate, tour.EndDate) >= 0 && EF.Functions.DateDiffDay(tour.EndDate, endDate) >= 0) ||     // Tours ending within the range
            (EF.Functions.DateDiffDay(tour.StartDate, startDate) <= 0 && EF.Functions.DateDiffDay(tour.EndDate, endDate) >= 0))     // Tours spanning the entire range
            .AsQueryable();

            return toursInRange;
        }

        public IQueryable<Tour> GetUpcomingTours(DateTime now)
        {
            var upcomingTours = context.Tours.Where(tour => EF.Functions.DateDiffDay(now, tour.StartDate) >= 0)
            .AsQueryable();

            return upcomingTours;
        }

        public void Update(Tour item)
        {
            context.Entry(item).State = EntityState.Modified;
        }
    }
}
