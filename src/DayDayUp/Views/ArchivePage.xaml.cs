using DayDayUp.ViewModels;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace DayDayUp.Views
{

    public sealed partial class ArchivePage : Page
    {

        public ArchivePageViewModel ViewModel => (ArchivePageViewModel)DataContext;

        private void TaskList_Loaded(object sender, RoutedEventArgs e)
        {
            ViewModel.LoadTodoCommand.ExecuteAsync(null);
        }

        private void TaskList_ItemClick(object sender, TappedRoutedEventArgs e)
        {
            if (((FrameworkElement)e.OriginalSource).DataContext as TodoListItemViewModel == null)
            {
                PageSplitView.IsPaneOpen = false;
            }
            else
            {
                PageSplitView.IsPaneOpen = true;
            }
        }

        private void ListView_RightTapped(object sender, RightTappedRoutedEventArgs e)
        {
            ListView listView = sender as ListView;
            if (((FrameworkElement)e.OriginalSource).DataContext as TodoListItemViewModel != null)
            {
                TodoItemFlyout.ShowAt(listView, e.GetPosition(listView));
                ViewModel.SelectedTodo = ((FrameworkElement)e.OriginalSource).DataContext as TodoListItemViewModel;
            }
        }

        private void DeleteMenuFlyoutItem_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.Delete(ViewModel.SelectedTodo);
        }

        private void RestoreMenuFlyoutItem_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.Restore(ViewModel.SelectedTodo);
        }

        public ArchivePage()
        {
            InitializeComponent();
            DataContext = Ioc.Default.GetRequiredService<ArchivePageViewModel>();
        }

        
    }
}
