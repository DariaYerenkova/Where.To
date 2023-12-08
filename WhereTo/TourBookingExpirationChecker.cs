using Microsoft.EntityFrameworkCore;
using WhereToDataAccess;
using WhereToDataAccess.Interfaces;

namespace WhereTo
{
    public class TourBookingExpirationChecker : BackgroundService
    {
        private readonly IServiceScopeFactory scopeFactory;
        private readonly IUserTourService userTourService;

        public TourBookingExpirationChecker(IServiceScopeFactory scopeFactory, IUserTourService userTourService)
        {
            this.scopeFactory = scopeFactory;
            this.userTourService = userTourService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = scopeFactory.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<WhereToDataContext>();

                    // Calculate the date 3 days ago
                    var threeDaysAgo = DateTime.UtcNow.AddDays(-3).Date;

                    // Retrieve bookings that are 3 days overdue and not payed
                    var overdueBookings = await dbContext.UserTours
                        .Where(ut => ut.DateRegistered < threeDaysAgo && !ut.IsPayed)
                        .ToListAsync();

                    // Process overdue bookings 
                    foreach (var booking in overdueBookings)
                    {
                        //remove row from UserTour table
                        userTourService.RemoveUserFromTour(booking);

                    }

                    await dbContext.SaveChangesAsync(); // Save changes to the database
                }

                // Wait for the next interval
                await Task.Delay(TimeSpan.FromHours(24), stoppingToken);
            }
        }
    }
}
