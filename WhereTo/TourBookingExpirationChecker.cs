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

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = scopeFactory.CreateScope())
                {
                    var userTourService = scope.ServiceProvider.GetRequiredService<IUserTourService>();

                    // Retrieve bookings that are 3 days overdue and not payed
                    var overdueBookings = await userTourService.GetNotPayedAndOverdueUserToursAsync();

                    // Process overdue bookings 
                    foreach (var booking in overdueBookings)
                    {
                        //remove row from UserTour table
                        await userTourService.RemoveUserFromTourAsync(booking);

                    }
                }

                // Wait for the next interval
                await Task.Delay(TimeSpan.FromSeconds(20), stoppingToken);
            }
        }
    }
}
