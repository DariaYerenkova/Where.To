using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhereToServices.DTOs
{
    public class FeddbackResponseModel
    {
        public int FeedbackId { get; set; }
        public string Comment { get; set; }
        public List<string> AttachmentsSasUrls { get; set; }
    }
}
