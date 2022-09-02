#nullable enable

using DayDayUp.Core.Settings;
using System;
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
