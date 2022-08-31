using DayDayUp.Helpers;
using DayDayUp.Services;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.ApplicationModel;
using Windows.UI.Xaml;

namespace DayDayUp.ViewModels
{
    public class SettingsPageViewModel: ObservableRecipient
    {
        private ElementTheme _elementTheme;
        
        private ICommand _switchThemeCommand;

        public ElementTheme ElementTheme
        {
            get => _elementTheme;

            set { SetProperty(ref _elementTheme, value); }
        }

        public ICommand SwitchThemeCommand => _switchThemeCommand;

        internal SettingsPageStrings Strings => LanguageManager.Instance.SettingsPage;

        internal List<LanguageDefinition> AvailableLanguages => LanguageManager.Instance.AvailableLanguages;

        public SettingsPageViewModel()
        {
            _elementTheme = ThemeSelectorHelper.Theme;
            _switchThemeCommand = new RelayCommand<object>(
                        (param) =>
                        {
                            ElementTheme = (ElementTheme)param;
                            ThemeSelectorHelper.SetTheme((ElementTheme)param);
                        });
        }

    }
}
