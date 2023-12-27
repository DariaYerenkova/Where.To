
using System.Threading;
using WhereToServices.DTOs;
using WhereToServices.Interfaces;

namespace BookingHostedService
{
    public class WhereTo_BookingQueueSubscriber : BackgroundService
    {
        private readonly IServiceScopeFactory scopeFactory;

        public WhereTo_BookingQueueSubscriber(IServiceScopeFactory scopeFactory)
        {
            this.scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                using var scope = scopeFactory.CreateScope();

                var bookingService = scope.ServiceProvider.GetRequiredService<IBookingService>();
                await bookingService.InitBookingProcess();
            }
        }
    }
}
