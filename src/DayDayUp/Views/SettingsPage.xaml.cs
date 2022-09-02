using DayDayUp.ViewModels;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using Windows.UI.Xaml.Controls;

namespace DayDayUp.Views
{

    public sealed partial class SettingsPage : Page
    {
        public SettingsPage()
        {
            this.InitializeComponent();

            DataContext = Ioc.Default.GetRequiredService<SettingsPageViewModel>();
        }

        public SettingsPageViewModel ViewModel => (SettingsPageViewModel)DataContext;
    }
}
