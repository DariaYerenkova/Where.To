using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Refit;
using WhereToServices.DTOs;

namespace ServicesTests.IntergationTests
{
    public interface IFeedbackApi
    {
        [Get("/api/feedback/{feedbackId}")]
        Task<FeddbackResponseModel> GetFeedback(int feedbackId);
    }
}
