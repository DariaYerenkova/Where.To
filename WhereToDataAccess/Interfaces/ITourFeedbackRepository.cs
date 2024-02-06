using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhereToDataAccess.Entities;

namespace WhereToDataAccess.Interfaces
{
    public interface ITourFeedbackRepository
    {
        TourFeedback Get(int id);
        void Create(TourFeedback item);
        void Update(TourFeedback item);
        void Delete(int id);
    }
}
