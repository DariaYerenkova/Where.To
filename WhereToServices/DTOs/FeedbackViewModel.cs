using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhereToServices.DTOs
{
    public class FeedbackViewModel
    {
        public string UserName { get; set; }
        public string TourDescription { get; set; }
        public string Comment { get; set; }
        public List<File> Attachments { get; set; }
    }
}
