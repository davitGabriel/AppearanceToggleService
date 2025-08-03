using System.Runtime.InteropServices;

namespace AppearanceToggleService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var now = DateTime.Now.TimeOfDay;

                if (now.Hours >= 20 || now.Hours < 6)
                {
                    ThemeManager.SetDarkMode();
                }
                else
                {
                    ThemeManager.SetLightMode();
                }

                SystemThemeNotifier.BroadcastThemeChange();

                await Task.Delay(TimeSpan.FromHours(1), stoppingToken);
            }
        }
    }
}
