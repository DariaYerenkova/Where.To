using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhereToDataAccess.Entities
{
    public class BlobAttachments_feedbackphotos
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string FileName { get; set; }

        [Required]
        public int TourFeedbackId { get; set; }

        public TourFeedback? TourFeedback { get; set; }
    }
}
