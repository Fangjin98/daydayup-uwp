

#nullable enable

using Microsoft.Toolkit.Mvvm.ComponentModel;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using Windows.ApplicationModel.Resources;
using Windows.Globalization;
using Windows.UI.Xaml;
using DayDayUp.Helpers;

namespace DayDayUp.Services
{
    public partial class LanguageManager : ObservableObject
    {
        private static LanguageManager? _languageManager;

        private readonly ArchivePageStrings _archivepage = new ArchivePageStrings();
        private readonly HomePageStrings _homepage = new HomePageStrings();
        private readonly MainPageStrings _mainpage = new MainPageStrings();
        private readonly SettingsPageStrings _settingspage = new SettingsPageStrings();

        /// <summary>
        /// Gets an instance of <see cref="LanguageManager"/>.
        /// </summary>
        public static LanguageManager Instance => _languageManager ?? (_languageManager = new LanguageManager());

        /// <summary>
        /// Gets if the text must be written from left to right or from right to left.
        /// </summary>
        public FlowDirection FlowDirection { get; private set; }

        /// <summary>
        /// Gets the <see cref="ArchivePageStrings"/>.
        /// </summary>
        public ArchivePageStrings ArchivePage => _archivepage;

        /// <summary>
        /// Gets the <see cref="HomePageStrings"/>.
        /// </summary>
        public HomePageStrings HomePage => _homepage;

        /// <summary>
        /// Gets the <see cref="MainPageStrings"/>.
        /// </summary>
        public MainPageStrings MainPage => _mainpage;

        /// <summary>
        /// Gets the <see cref="SettingsPageStrings"/>.
        /// </summary>
        public SettingsPageStrings SettingsPage => _settingspage;

        /// <summary>
        /// Gets the list of available languages in the app.
        /// </summary>
        public List<LanguageDefinition> AvailableLanguages { get; }

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

    public class ArchivePageStrings : ObservableObject
    {
        private readonly ResourceLoader _resources = ResourceLoader.GetForViewIndependentUse("ArchivePage");

        /// <summary>
        /// Gets the resource Archive.
        /// </summary>
        public string Archive => _resources.GetString("Archive");

        /// <summary>
        /// Gets the resource Bias.
        /// </summary>
        public string Bias => _resources.GetString("Bias");

        /// <summary>
        /// Gets the resource Delete.
        /// </summary>
        public string Delete => _resources.GetString("Delete");

        /// <summary>
        /// Gets the resource Duration.
        /// </summary>
        public string Duration => _resources.GetString("Duration");

        /// <summary>
        /// Gets the resource EmptyList.
        /// </summary>
        public string EmptyList => _resources.GetString("EmptyList");

        /// <summary>
        /// Gets the resource EstimatedDuration.
        /// </summary>
        public string EstimatedDuration => _resources.GetString("EstimatedDuration");

        /// <summary>
        /// Gets the resource Finish.
        /// </summary>
        public string Finish => _resources.GetString("Finish");

        /// <summary>
        /// Gets the resource Restore.
        /// </summary>
        public string Restore => _resources.GetString("Restore");
    }

    public class HomePageStrings : ObservableObject
    {
        private readonly ResourceLoader _resources = ResourceLoader.GetForViewIndependentUse("HomePage");

        /// <summary>
        /// Gets the resource AddNewTodo.
        /// </summary>
        public string AddNewTodo => _resources.GetString("AddNewTodo");

        /// <summary>
        /// Gets the resource CreatedIn.
        /// </summary>
        public string CreatedIn => _resources.GetString("CreatedIn");

        /// <summary>
        /// Gets the resource Delete.
        /// </summary>
        public string Delete => _resources.GetString("Delete");

        /// <summary>
        /// Gets the resource DropDown12Hours.
        /// </summary>
        public string DropDown12Hours => _resources.GetString("DropDown12Hours");

        /// <summary>
        /// Gets the resource DropDown1Day.
        /// </summary>
        public string DropDown1Day => _resources.GetString("DropDown1Day");

        /// <summary>
        /// Gets the resource DropDown1Hour.
        /// </summary>
        public string DropDown1Hour => _resources.GetString("DropDown1Hour");

        /// <summary>
        /// Gets the resource DropDown30Mins.
        /// </summary>
        public string DropDown30Mins => _resources.GetString("DropDown30Mins");

        /// <summary>
        /// Gets the resource DropDown6Hours.
        /// </summary>
        public string DropDown6Hours => _resources.GetString("DropDown6Hours");

        /// <summary>
        /// Gets the resource DropDownNone.
        /// </summary>
        public string DropDownNone => _resources.GetString("DropDownNone");

        /// <summary>
        /// Gets the resource Duration.
        /// </summary>
        public string Duration => _resources.GetString("Duration");

        /// <summary>
        /// Gets the resource EmptyList.
        /// </summary>
        public string EmptyList => _resources.GetString("EmptyList");

        /// <summary>
        /// Gets the resource EstimatedDuration.
        /// </summary>
        public string EstimatedDuration => _resources.GetString("EstimatedDuration");

        /// <summary>
        /// Gets the resource EstimatedDurationToolTip.
        /// </summary>
        public string EstimatedDurationToolTip => _resources.GetString("EstimatedDurationToolTip");

        /// <summary>
        /// Gets the resource PredictionDuration.
        /// </summary>
        public string PredictionDuration => _resources.GetString("PredictionDuration");

        /// <summary>
        /// Gets the resource Status.
        /// </summary>
        public string Status => _resources.GetString("Status");
    }

    public class MainPageStrings : ObservableObject
    {
        private readonly ResourceLoader _resources = ResourceLoader.GetForViewIndependentUse("MainPage");

        /// <summary>
        /// Gets the resource AppDisplayName.
        /// </summary>
        public string AppDisplayName => _resources.GetString("AppDisplayName");

        /// <summary>
        /// Gets the resource Archive.
        /// </summary>
        public string Archive => _resources.GetString("Archive");

        /// <summary>
        /// Gets the resource Home.
        /// </summary>
        public string Home => _resources.GetString("Home");
    }

    public class SettingsPageStrings : ObservableObject
    {
        private readonly ResourceLoader _resources = ResourceLoader.GetForViewIndependentUse("SettingsPage");

        /// <summary>
        /// Gets the resource About.
        /// </summary>
        public string About => _resources.GetString("About");

        /// <summary>
        /// Gets the resource AboutDescription.
        /// </summary>
        public string AboutDescription => _resources.GetString("AboutDescription");

        /// <summary>
        /// Gets the resource AppTheme.
        /// </summary>
        public string AppTheme => _resources.GetString("AppTheme");

        /// <summary>
        /// Gets the resource AppThemeDescription.
        /// </summary>
        public string AppThemeDescription => _resources.GetString("AppThemeDescription");

        /// <summary>
        /// Gets the resource AppTitle.
        /// </summary>
        public string AppTitle => _resources.GetString("AppTitle");

        /// <summary>
        /// Gets the resource Dark.
        /// </summary>
        public string Dark => _resources.GetString("Dark");

        /// <summary>
        /// Gets the resource DefaultLanguage.
        /// </summary>
        public string DefaultLanguage => _resources.GetString("DefaultLanguage");

        /// <summary>
        /// Gets the resource Language.
        /// </summary>
        public string Language => _resources.GetString("Language");

        /// <summary>
        /// Gets the resource LanguageDescription.
        /// </summary>
        public string LanguageDescription => _resources.GetString("LanguageDescription");

        /// <summary>
        /// Gets the resource Light.
        /// </summary>
        public string Light => _resources.GetString("Light");

        /// <summary>
        /// Gets the resource MenuDisplayName.
        /// </summary>
        public string MenuDisplayName => _resources.GetString("MenuDisplayName");

        /// <summary>
        /// Gets the resource Settings.
        /// </summary>
        public string Settings => _resources.GetString("Settings");

        /// <summary>
        /// Gets the resource UseSystemSettings.
        /// </summary>
        public string UseSystemSettings => _resources.GetString("UseSystemSettings");
    }
}
