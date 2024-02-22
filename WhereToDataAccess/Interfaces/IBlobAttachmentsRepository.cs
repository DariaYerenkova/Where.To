using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhereToDataAccess.Entities;

namespace WhereToDataAccess.Interfaces
{
    public interface IBlobAttachmentsRepository
    {
        BlobAttachments_feedbackphotos Get(int id);
        void CreateRange(List<BlobAttachments_feedbackphotos> items);
        void Delete(int id);
    }
}
