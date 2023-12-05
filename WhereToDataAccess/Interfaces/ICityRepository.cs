using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhereToDataAccess.Entities;

namespace WhereToDataAccess.Interfaces
{
    public interface ICityRepository
    {
        IQueryable<City> GetAll();
        City Get(int id);
        void Create(City item);
        void Update(City item);
        void Delete(int id);
    }
}
