using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhereToServices.DTOs;

namespace WhereToServices.Interfaces
{
    public interface IQueueMessageService
    {
        Whereto_booking_message GenerateMessageForWhereTo_BookingQueue(int userId, int tourId);
    }
}
