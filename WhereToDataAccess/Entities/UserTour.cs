using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhereToDataAccess.Enums;

namespace WhereToDataAccess.Entities
{
    public class UserTour
    {
        public int TourId { get; set; }

        [ForeignKey("TourId")]
        public Tour? Tour { get; set; }

        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User? User { get; set; }
        public bool IsPayed { get; set; }
        public UserTourStatus Status { get; set; }
        public DateTime DateRegistered { get; set; }
    }
}
