using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhereToServices.Interfaces
{
    public interface IQueueMessageSubscriber<T>
    {
        Task<T> ReadMessageFromQueueAsync();
        Task DeleteMessageAsync();
    }
}
