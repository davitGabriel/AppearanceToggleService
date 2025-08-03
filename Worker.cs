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

                await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
            }
        }
    }

    public class SystemThemeNotifier
    {
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr SendMessageTimeout(
            IntPtr hWnd,
            uint Msg,
            UIntPtr wParam,
            string lParam,
            uint fuFlags,
            uint uTimeout,
            out UIntPtr lpdwResult);

        private const int HWND_BROADCAST = 0xffff;
        private const uint WM_SETTINGCHANGE = 0x001A;
        private const uint SMTO_ABORTIFHUNG = 0x0002;

        public static void BroadcastThemeChange()
        {
            UIntPtr result;
            SendMessageTimeout((IntPtr)HWND_BROADCAST,
                WM_SETTINGCHANGE,
                UIntPtr.Zero,
                "ImmersiveColorSet",
                SMTO_ABORTIFHUNG,
                5000,
                out result);
        }
    }
}
