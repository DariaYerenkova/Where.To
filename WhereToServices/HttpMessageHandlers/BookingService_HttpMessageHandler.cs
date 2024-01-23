using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhereToServices.HttpMessageHandlers
{
    public class BookingService_HttpMessageHandler : DelegatingHandler
    {
        public BookingService_HttpMessageHandler():base(new HttpClientHandler())
        {
                
        }
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return await base.SendAsync(request, cancellationToken);
        }
    }
}
