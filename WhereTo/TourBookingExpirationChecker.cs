using Microsoft.EntityFrameworkCore;
using WhereToDataAccess;
using WhereToDataAccess.Interfaces;
using WhereToServices.Interfaces;

namespace WhereTo
{
    public class TourBookingExpirationChecker : BackgroundService
    {
        private readonly IServiceScopeFactory scopeFactory;

        public TourBookingExpirationChecker(IServiceScopeFactory scopeFactory)
        {
            this.scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                using var scope = scopeFactory.CreateScope();

                var userTourService = scope.ServiceProvider.GetRequiredService<IUserTourService>();

                //Find and remove overdue bookings
                await userTourService.RemoveExpiredBookingsAsync();


                // Wait for the next interval
                await Task.Delay(TimeSpan.FromSeconds(20), cancellationToken);
            }
        }
    }
}
