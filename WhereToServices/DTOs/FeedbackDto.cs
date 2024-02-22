using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhereToServices.DTOs
{
    public class FeedbackDto
    {
        public int TourId { get; set; }
        public int UserId { get; set; }
        public string Comment  { get; set; }
        public List<string> FilePaths { get; set; }
    }
}
