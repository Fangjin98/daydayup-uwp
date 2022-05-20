using DayDayUp.Helpers;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.ApplicationModel;
using Windows.UI.Xaml;

namespace DayDayUp.ViewModels
{
    public class SettingsPageViewModel: ObservableRecipient
    {

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

        private ElementTheme _elementTheme; 

        public ElementTheme ElementTheme
        {
            get => _elementTheme;

            set { SetProperty(ref _elementTheme, value); }
        }

        private ICommand _switchThemeCommand;

        public ICommand SwitchThemeCommand
        {
            get => _switchThemeCommand;
        }


    }
}
