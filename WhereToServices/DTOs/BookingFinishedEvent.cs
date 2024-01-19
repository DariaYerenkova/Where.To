using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhereToServices.DTOs
{
    public record BookingFinishedEvent(string TourId, string Passport)
    {
        public string TourId { get; init; } = TourId;
        public string Passport { get; init; } = Passport;
    }
}
