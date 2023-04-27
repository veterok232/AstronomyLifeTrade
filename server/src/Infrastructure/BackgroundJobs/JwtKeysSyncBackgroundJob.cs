using Infrastructure.Interfaces.Jwt.SigningKeys;
using Infrastructure.Settings;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace Infrastructure.BackgroundJobs;

public sealed class JwtKeysSyncBackgroundJob : IHostedService, IDisposable
{
    private readonly IJwtKeySyncService _jwtKeySyncService;
    private readonly JwtSettings _jwtSettings;
    private Timer _timer;

    public JwtKeysSyncBackgroundJob(
        IJwtKeySyncService jwtKeySyncService,
        IOptions<JwtSettings> jwtSettings)
    {
        _jwtKeySyncService = jwtKeySyncService;
        _jwtSettings = jwtSettings.Value;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _timer = new Timer(
            callback: state => _jwtKeySyncService.Sync(),
            state: null,
            dueTime: TimeSpan.Zero,
            period: _jwtSettings.SigningKeysSettings.KeysSyncInterval);

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;

    public void Dispose()
    {
        _timer?.Dispose();
    }
}