using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhereToDataAccess.Entities;

namespace WhereToDataAccess.Interfaces
{
    public interface ITourCityRepository
    {
        IQueryable<TourCity> GetAll();
        TourCity Get(int id);
        void Create(TourCity item);
        void Update(TourCity item);
        void Delete(int id);
    }
}
