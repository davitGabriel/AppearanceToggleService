# AppearanceToggleService

A simple .NET Worker Service that automatically switches Windows system appearance mode (Light/Dark) based on the time of day.

## 🚀 How it works

- Every 5 minutes, the service checks the current time.
- If the time is between 8 PM and 6 AM, it switches to **Dark Mode**.
- Otherwise, it switches to **Light Mode**.
- The service updates Windows registry keys:
  - `HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Themes\Personalize`
  - `AppsUseLightTheme` and `SystemUsesLightTheme` values are changed.
- After changing the registry, it broadcasts a `WM_SETTINGCHANGE` message so apps that support it can refresh.

> ⚠️ Some apps like Chrome only read the system theme at startup and may require restarting to apply the new appearance.

## 🛠 Technologies

- .NET 6+ Worker Service
- Microsoft.Extensions.Hosting.WindowsServices
- Runs as a native Windows Service.

## 📦 Installation

1. Publish the service:

    ```bash
    dotnet publish -c Release -o "C:\MyServices\AppearanceToggleService"
    ```

2. Open an **elevated Command Prompt** and install the service:

    ```bash
    sc create AppearanceToggleService binPath= "C:\MyServices\AppearanceToggleService\AppearanceToggleService.exe"
    sc start AppearanceToggleService
    ```

3. To stop or uninstall:

    ```bash
    sc stop AppearanceToggleService
    sc delete AppearanceToggleService
    ```

## 📂 Folder Structure

/AppearanceToggleService
├─ Worker.cs # Main background service logic
├─ ThemeManager.cs # Handles registry changes
├─ SystemThemeNotifier.cs # Broadcasts WM_SETTINGCHANGE
├─ Program.cs # Entry point for the worker
└─ README.md

## 📝 Notes

- Since the service runs in the system context, you must ensure it affects the correct user’s registry. This project uses `Registry.CurrentUser`, which works if the service runs under your user account. Otherwise, extra steps are needed to write to the correct `HKEY_USERS\<SID>`.

- Tested on Windows 11. Behavior may vary on older versions.

## 📜 License

Feel free to use and modify this project.
