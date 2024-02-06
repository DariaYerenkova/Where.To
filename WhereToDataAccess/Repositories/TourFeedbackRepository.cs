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
    public class TourFeedbackRepository : ITourFeedbackRepository
    {
        private readonly WhereToDataContext context;

        public TourFeedbackRepository(WhereToDataContext context)
        {
            this.context = context;
        }

        public void Create(TourFeedback item)
        {
            context.TourFeedbacks.Add(item);
        }

        public void Delete(int id)
        {
            TourFeedback feedback = context.TourFeedbacks.Find(id);
            if (feedback != null)
            {
                context.TourFeedbacks.Remove(feedback);
            }
        }

        public TourFeedback Get(int id)
        {
            return context.TourFeedbacks.Include(f => f.User).Include(f=>f.Tour).FirstOrDefault(f => f.Id == id);
        }

        public void Update(TourFeedback item)
        {
            context.Entry(item).State = EntityState.Modified;
        }
    }
}
