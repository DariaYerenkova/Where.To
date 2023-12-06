using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhereToDataAccess.Entities;

namespace WhereToServices.Interfaces
{
    public interface ITourService
    {
        public IEnumerable<Tour> GetTours();
        public Tour GetTourById(int id);
        public IEnumerable<Tour> GetToursByDateRange(DateTime startDate, DateTime endDate);
        public IEnumerable<Tour> GetUpcomingTours();
        public IEnumerable<Tour> GetToursByCity(int cityId);
        public void CreateTour(Tour tour);
        public void Update(Tour tour);
        public void Delete(int id);
    }
}
