using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhereToDataAccess.Entities
{
    public class Tour
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string TourName { get; set; }

        [Required]
        public string TourDescription { get; set;}

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Range(0.0, double.MaxValue)]
        public decimal Price { get; set; }

        public int? TotalPlaces { get; set; }

        
        public ICollection<TourCity> TourCities { get; set; }

        public ICollection<UserTour>? UserTours { get; set; }
    }
}
