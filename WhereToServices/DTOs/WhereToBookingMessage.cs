using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhereToServices.DTOs
{
    public class WhereToBookingMessage(string userFirstName, string userLastName, string passportNumber, string tourName)
    {
        public string UserFirstName { get; init; } = userFirstName;
        public string UserLastName { get; init; } = userLastName;
        public string PassportNumber { get; init; } = passportNumber;
        public string TourName { get; init; } = tourName;

    }
}
