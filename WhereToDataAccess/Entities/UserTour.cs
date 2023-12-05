using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhereToDataAccess.Entities
{
    public class UserTour
    {
        public int TourId { get; set; }

        [ForeignKey("TourId")]
        public Tour Tour { get; set; }

        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}
