using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhereToServices.DTOs
{
    public record BookingFinishedEvent(string tourId, string passport)
    {
        public string TourId { get; init; } = tourId;
        public string UserPassport { get; init; } = passport;
    }
}
