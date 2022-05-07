using DayDayUp.Models;
using DayDayUp.ViewModels;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace DayDayUp.Views
{

    public sealed partial class ArchivePage : Page
    {
        public ArchivePageViewModel ViewModel => (ArchivePageViewModel)DataContext;

        public ArchivePage()
        {
            InitializeComponent();
            DataContext = Ioc.Default.GetRequiredService<ArchivePageViewModel>();
        }

        private void TaskList_Loaded(object sender, RoutedEventArgs e)
        {
            ViewModel.LoadTodoCommand.ExecuteAsync(null);
        }

        private void TaskList_ItemClick(object sender, TappedRoutedEventArgs e)
        {
            if (((FrameworkElement)e.OriginalSource).DataContext as Todo == null)
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
            if (((FrameworkElement)e.OriginalSource).DataContext as Todo != null)
            {
                TodoItemFlyout.ShowAt(listView, e.GetPosition(listView));
                ViewModel.SelectedTodo = ((FrameworkElement)e.OriginalSource).DataContext as Todo;
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
    }
}
