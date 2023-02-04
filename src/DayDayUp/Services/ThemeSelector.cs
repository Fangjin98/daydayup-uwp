#nullable enable

using DayDayUp.Core.Settings;
using System;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;

namespace DayDayUp.Services
{
    public class ThemeSelector
    {
        private readonly ISettingsProvider _settingsProvider;

        public AppTheme CurrentSystemTheme { get; private set; }

        public AppTheme CurrentTheme => _settingsProvider.GetSetting(PredefinedSettings.Theme);

        public event EventHandler? ThemeChanged;

        public ThemeSelector(ISettingsProvider settingsProvider)
        {
            _settingsProvider = settingsProvider;
            CurrentSystemTheme = Application.Current.RequestedTheme == ApplicationTheme.Dark ? AppTheme.Dark : AppTheme.Light;

            _settingsProvider.SettingChanged += SettingsProvider_SettingChanged;
        }

        public void SetRequestedTheme()
        {
            AppTheme theme = CurrentTheme;
            
            if (theme == AppTheme.Default)
            {
                theme = CurrentSystemTheme;
            }

            if (Window.Current.Content is FrameworkElement rootElement)
            {
                rootElement.RequestedTheme = theme == AppTheme.Light ? ElementTheme.Light : ElementTheme.Dark;
            }

            ApplicationViewTitleBar titleBar = ApplicationView.GetForCurrentView().TitleBar;

            if (theme == AppTheme.Dark)
            {
                // Set active window colors
                titleBar.ButtonForegroundColor = Colors.White;
                titleBar.ButtonBackgroundColor = Colors.Transparent;
                titleBar.ButtonHoverForegroundColor = Colors.White;
                titleBar.ButtonHoverBackgroundColor = Color.FromArgb(255, 90, 90, 90);
                titleBar.ButtonPressedForegroundColor = Colors.White;
                titleBar.ButtonPressedBackgroundColor = Color.FromArgb(255, 120, 120, 120);

                // Set inactive window colors
                titleBar.InactiveForegroundColor = Colors.Gray;
                titleBar.InactiveBackgroundColor = Colors.Transparent;
                titleBar.ButtonInactiveForegroundColor = Colors.Gray;
                titleBar.ButtonInactiveBackgroundColor = Colors.Transparent;

                titleBar.BackgroundColor = Color.FromArgb(255, 45, 45, 45);
            }
            else if (theme == AppTheme.Light)
            {
                // Set active window colors
                titleBar.ButtonForegroundColor = Colors.Black;
                titleBar.ButtonBackgroundColor = Colors.Transparent;
                titleBar.ButtonHoverForegroundColor = Colors.Black;
                titleBar.ButtonHoverBackgroundColor = Color.FromArgb(255, 180, 180, 180);
                titleBar.ButtonPressedForegroundColor = Colors.Black;
                titleBar.ButtonPressedBackgroundColor = Color.FromArgb(255, 150, 150, 150);

                // Set inactive window colors
                titleBar.InactiveForegroundColor = Colors.DimGray;
                titleBar.InactiveBackgroundColor = Colors.Transparent;
                titleBar.ButtonInactiveForegroundColor = Colors.DimGray;
                titleBar.ButtonInactiveBackgroundColor = Colors.Transparent;

                titleBar.BackgroundColor = Color.FromArgb(255, 210, 210, 210);
            }
        }

        private void SettingsProvider_SettingChanged(object sender, SettingChangedEventArgs e)
        {
            if (string.Equals(PredefinedSettings.Theme.Name, e.SettingName, StringComparison.Ordinal))
            {
                SetRequestedTheme();
                ThemeChanged?.Invoke(this, EventArgs.Empty);
            }
        }

    }

    public enum AppTheme
    {
        Default,
        Light,
        Dark
    }
}
