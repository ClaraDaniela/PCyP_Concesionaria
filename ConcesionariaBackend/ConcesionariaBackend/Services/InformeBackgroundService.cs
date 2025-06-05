using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;
using ConcesionariaBackend.Services;

public class InformeBackgroundService : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;

    public InformeBackgroundService(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var informeService = scope.ServiceProvider.GetRequiredService<InformeHistoricoService>();
                await informeService.GenerarYGuardarInformesAsync();
            }

            await Task.Delay(TimeSpan.FromHours(24), stoppingToken);
        }
    }
}
