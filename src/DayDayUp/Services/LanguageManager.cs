using DayDayUp.Helpers;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources;
using Windows.Globalization;
using Windows.UI.Xaml;

namespace DayDayUp.Services
{
    public partial class LanguageManager : ObservableObject
    {
        private static LanguageManager? _languageManager;

        private readonly SettingsPageStrings settingsPage=new SettingsPageStrings();

        public static LanguageManager Instance => _languageManager ?? (_languageManager = new LanguageManager());

        public List<LanguageDefinition> AvailableLanguages { get; }
        
        public FlowDirection FlowDirection { get; private set; }

        public SettingsPageStrings SettingsPage => settingsPage;

        public LanguageManager()
        {
            AvailableLanguages = new List<LanguageDefinition>();
            AvailableLanguages.Add(new LanguageDefinition()); // default language
            IReadOnlyList<string> supportedLanguageIdentifiers = ApplicationLanguages.ManifestLanguages;
            for (int i = 0; i < supportedLanguageIdentifiers.Count; i++)
            {
                AvailableLanguages.Add(new LanguageDefinition(supportedLanguageIdentifiers[i]));
            }
        }

        /// <summary>
        /// Retrieves the current culture.
        /// </summary>
        public CultureInfo GetCurrentCulture()
        {
            return CultureInfo.CurrentUICulture;
        }

        /// <summary>
        /// Change the current culture of the application
        /// </summary>
        public void SetCurrentCulture(LanguageDefinition language)
        {
            CultureInfo.DefaultThreadCurrentCulture = language.Culture;
            CultureInfo.DefaultThreadCurrentUICulture = language.Culture;
            ApplicationLanguages.PrimaryLanguageOverride = language.Identifier;

            if (language.Culture.TextInfo.IsRightToLeft)
            {
                FlowDirection = FlowDirection.RightToLeft;
            }
            else
            {
                FlowDirection = FlowDirection.LeftToRight;
            }

            // All the properties changed.
            OnPropertyChanged(new PropertyChangedEventArgs(string.Empty));
        }
    }

    public class SettingsPageStrings : ObservableObject
    {
        private readonly ResourceLoader _resources = ResourceLoader.GetForViewIndependentUse("SettingsPage");

        public string About => _resources.GetString("About");

        public string AppTheme => _resources.GetString("AppTheme");

        public string AppThemeDescription => _resources.GetString("AppThemeDescription");

        public string AppTitle => _resources.GetString("AppTitle");

        public string Dark => _resources.GetString("Dark");

        public string DefaultLanguage => _resources.GetString("DefaultLanguage");

        public string Language => _resources.GetString("Language");

        public string Light => _resources.GetString("Light");

        public string AboutDescription => _resources.GetString("AboutDescription");

        public string UseSystemSettings => _resources.GetString("UseSystemSettings");

        public string LanguageDescription => _resources.GetString("LanguageDescription");

    }

}
