using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesTests.IntergationTests.Models
{
    public class RegisterControllerCommand(int TourId, int UserId)
    {
        public int TourId { get; init; } = TourId;
        public int UserId { get; init; } = UserId;
    }
}
