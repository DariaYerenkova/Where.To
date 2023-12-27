using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhereToServices.DTOs
{
    public class WhereToBookingMessage(string FirstName, string LastName, string PassportNumber, int TourId)
    {
        public string FirstName { get; init; } = FirstName;
        public string LastName { get; init; } = LastName;
        public string PassportNumber { get; init; } = PassportNumber;
        public int TourId { get; init; } = TourId;

    }
}
