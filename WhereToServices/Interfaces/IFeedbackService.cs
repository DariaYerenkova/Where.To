using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhereToDataAccess.Entities;
using WhereToServices.DTOs;

namespace WhereToServices.Interfaces
{
    public interface IFeedbackService
    {
        TourFeedback GetTourFeedbackByUserId(int id);
        Task CreateFeedback(FeedbackDto tourFeedback);
    }
}
