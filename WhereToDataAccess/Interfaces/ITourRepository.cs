using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhereToDataAccess.Entities;

namespace WhereToDataAccess.Interfaces
{
    public interface ITourRepository
    {
        IQueryable<Tour> GetAll();
        Tour Get(int id);
        void Create(Tour item);
        void Update(Tour item);
        void Delete(int id);
        IQueryable<Tour> GetToursByDateRange(DateTime startDate, DateTime? endDate);
        IQueryable<Tour> GetToursByCity(int cityId);
    }
}
