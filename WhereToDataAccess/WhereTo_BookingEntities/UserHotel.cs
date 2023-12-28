using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhereToDataAccess.WhereTo_BookingEntities
{
    public class UserHotel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        public User User { get; set; }

        [Required]
        [MaxLength(500)]
        public string HotelDetails { get; set; }

        [Required]
        public int TourId { get; set; }
    }
}
