using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhereToServices.DTOs
{
    public class Whereto_booking_message
    {
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string PassportNumber { get; set; }
        public string TourName { get; set; }

        public Whereto_booking_message(string userFirstName, string userLastName, string passportNumber, string tourName)
        {
            UserFirstName = userFirstName;
            UserLastName = userLastName;
            PassportNumber = passportNumber;
            TourName = tourName;
        }
    }
}
