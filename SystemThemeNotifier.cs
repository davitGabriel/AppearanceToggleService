using System.Runtime.InteropServices;

namespace AppearanceToggleService
{
    public class SystemThemeNotifier
    {
        [DllImport("user32.dll", SetLastError = true)]
        public static extern nint SendMessageTimeout(
            nint hWnd,
            uint Msg,
            nuint wParam,
            string lParam,
            uint fuFlags,
            uint uTimeout,
            out nuint lpdwResult);

        private const int HWND_BROADCAST = 0xffff;
        private const uint WM_SETTINGCHANGE = 0x001A;
        private const uint SMTO_ABORTIFHUNG = 0x0002;

        public static void BroadcastThemeChange()
        {
            nuint result;
            SendMessageTimeout(HWND_BROADCAST,
                WM_SETTINGCHANGE,
                nuint.Zero,
                "ImmersiveColorSet",
                SMTO_ABORTIFHUNG,
                5000,
                out result);
        }
    }
}
