using BlockCountriesTask.IServices;

namespace BlockCountriesTask.Services
{
    public class TempBlockCleanupService : BackgroundService
    {
        private readonly IServiceProvider _provider;

        public TempBlockCleanupService(IServiceProvider provider)
        {
            _provider = provider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _provider.CreateScope();
                var svc = scope.ServiceProvider.GetRequiredService<ITemporalBlockService>();
                svc.CleanupExpiredBlocks();
                await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
            }
        }
    }

}
