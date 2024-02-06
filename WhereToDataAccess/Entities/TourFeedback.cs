using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhereToDataAccess.WhereTo_BookingEntities;

namespace WhereToDataAccess.Entities
{
    public class TourFeedback
    {
        [Key]
        public int Id { get; set; }

        public Tour? Tour { get; set; }

        [Required]
        public int TourId { get; set; }

        public User? User { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public string Comment { get; set; }
        public ICollection<BlobAttachments_feedbackphotos>? FeedbackPhotos { get; set; }
    }
}
