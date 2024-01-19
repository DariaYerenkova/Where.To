
using WhereToServices.Interfaces;

namespace WhereTo
{
    public class WhereTo_BookingFinishedQueueSubscriber : BackgroundService
    {
        private readonly IServiceScopeFactory scopeFactory;
        public WhereTo_BookingFinishedQueueSubscriber(IServiceScopeFactory scopeFactory)
        {
            this.scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                using var scope = scopeFactory.CreateScope();

                var userTourService = scope.ServiceProvider.GetRequiredService<IUserTourService>();
                await userTourService.ApplyForTourAsync();
            }
        }
    }
}
