using DayDayUp.ViewModels;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using Windows.UI.Xaml.Controls;

namespace DayDayUp.Views
{
    public sealed partial class DashboardPage : Page
    {
        public DashboardPageViewModel ViewModel => (DashboardPageViewModel)DataContext;

        public DashboardPage()
        {
            InitializeComponent();

            DataContext = Ioc.Default.GetRequiredService<DashboardPageViewModel>();

            ViewModel.LoadTaskCommand.Execute(null);
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string categoryName = e.AddedItems[0].ToString();
            ViewModel.SetStatics(categoryName);
        }
    }
}
