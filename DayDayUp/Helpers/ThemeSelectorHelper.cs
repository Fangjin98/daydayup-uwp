using System;
using Windows.Storage;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;

namespace DayDayUp.Helpers
{
    public static class ThemeSelectorHelper
    {
        private const string SettingsKey = "SelectedAppTheme";

        public static ElementTheme Theme { get; set; } = ElementTheme.Default;

        public static void Initialize()
        {
            Theme = LoadThemeFromSettings();
            SetRequestedTheme();
        }

        public static void SetTheme(ElementTheme theme)
        {
            Theme = theme;
            SetRequestedTheme();
            SaveThemeInSettings(Theme);
        }

        public static void SetRequestedTheme()
        {
            
            if (Window.Current.Content is FrameworkElement rootElement)
            {
                rootElement.RequestedTheme = Theme;
                var titleBar = ApplicationView.GetForCurrentView().TitleBar;
                titleBar.ButtonBackgroundColor = titleBar.ButtonInactiveBackgroundColor= Colors.Transparent;
                if (Theme == ElementTheme.Light)
                {
                    titleBar.ButtonForegroundColor = titleBar.ButtonHoverForegroundColor = titleBar.ButtonPressedForegroundColor = Colors.Black;
                }
                else if (Theme == ElementTheme.Dark)
                {
                    titleBar.ButtonForegroundColor = titleBar.ButtonHoverForegroundColor = titleBar.ButtonPressedForegroundColor = Colors.Gray;
                }

                ApplicationData.Current.LocalSettings.Values[SettingsKey] = ApplicationData.Current.LocalSettings.Values[SettingsKey]?.ToString();
            }

        }

        private static ElementTheme LoadThemeFromSettings()
        {
            ElementTheme cacheTheme = ElementTheme.Default;
            string themeName = (string)ApplicationData.Current.LocalSettings.Values[SettingsKey];

            if (!string.IsNullOrEmpty(themeName))
            {
                _ = Enum.TryParse(themeName, out cacheTheme);
            }

            return cacheTheme;
        }

        private static void SaveThemeInSettings(ElementTheme theme)
        {
            ApplicationData.Current.LocalSettings.Values[SettingsKey] = theme.ToString();
        }

    }
}
