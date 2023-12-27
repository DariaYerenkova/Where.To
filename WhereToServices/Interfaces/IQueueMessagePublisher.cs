using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhereToServices.DTOs;

namespace WhereToServices.Interfaces
{
    public interface IQueueMessagePublisher<T>
    {
        Task SendMessageToQueueAsync(string message);
        string GenerateSerializedMesssageForQueue(T model);
    }
}
