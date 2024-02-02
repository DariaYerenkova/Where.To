using ServicesTests.IntergationTests.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhereToDataAccess.Entities;

namespace ServicesTests.IntergationTests
{
    public static class RequestFactory
    {
        public static RegisterControllerCommand CreateCommand(
            int UserId = 1,
            int TourId = 1)
            => new(UserId, TourId);
    }
}
