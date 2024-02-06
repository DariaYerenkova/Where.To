using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhereToDataAccess.Entities;
using WhereToDataAccess.Interfaces;

namespace WhereToDataAccess.Interfaces
{
    public interface IUnitOfWork
    {
        ITourRepository Tours { get; }
        IUserRepository Users { get; }
        ICityRepository Cities { get; }
        ITourCityRepository TourCities { get; }
        IUserTourRepository UserTours { get; }
        ITourFeedbackRepository TourFeedbacks { get; }
        IBlobAttachmentsRepository BlobAttachments { get; }
        void Save();
        Task SaveAsync();
    }
}
