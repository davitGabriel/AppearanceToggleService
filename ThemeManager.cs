using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppearanceToggleService
{
    public static class ThemeManager
    {
        private const string PersonalizeKeyPath = @"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize";

        public static void SetDarkMode()
        {
            using var key = Registry.CurrentUser.OpenSubKey(PersonalizeKeyPath, writable: true);
            key?.SetValue("AppsUseLightTheme", 0, RegistryValueKind.DWord);
            key?.SetValue("SystemUsesLightTheme", 0, RegistryValueKind.DWord);
        }

        public static void SetLightMode()
        {
            using var key = Registry.CurrentUser.OpenSubKey(PersonalizeKeyPath, writable: true);
            key?.SetValue("AppsUseLightTheme", 1, RegistryValueKind.DWord);
            key?.SetValue("SystemUsesLightTheme", 1, RegistryValueKind.DWord);
        }
    }
}
