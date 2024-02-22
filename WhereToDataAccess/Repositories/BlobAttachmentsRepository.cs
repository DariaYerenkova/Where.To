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
    public class BlobAttachmentsRepository : IBlobAttachmentsRepository
    {
        private readonly WhereToDataContext context;

        public BlobAttachmentsRepository(WhereToDataContext context)
        {
            this.context = context;
        }

        public void CreateRange(List<BlobAttachments_feedbackphotos> items)
        {
            context.BlobAttachments.AddRange(items);
        }

        public void Delete(int id)
        {
            BlobAttachments_feedbackphotos photo = context.BlobAttachments.Find(id);
            if (photo != null)
            {
                context.BlobAttachments.Remove(photo);
            }
        }

        public BlobAttachments_feedbackphotos Get(int id)
        {
            return context.BlobAttachments.Include(a => a.TourFeedback).FirstOrDefault(a => a.Id == id);
        }
    }
}
