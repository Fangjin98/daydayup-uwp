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
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class HomePage : Page
    {
        public HomePageViewModel ViewModel => (HomePageViewModel)DataContext;

        public HomePage()
        {
            InitializeComponent();
            DataContext = Ioc.Default.GetRequiredService<HomePageViewModel>();

            taskDuration = 0;
        }

        private int taskDuration;

        private void AddTaskButton_Click(object sender, RoutedEventArgs e)
        {
            if (newTaskTextBox.Text != "")
            {
                ViewModel.AddTaskCommand.ExecuteAsync(new Todo
                {
                    Name = newTaskTextBox.Text,
                    ExpectedDurationMins = taskDuration,
                    IsFinished = false,
                    Status = TodoStatus.Pause,
                    CreationDate = DateTime.Now,
                    TimeStamps = new List<DateTime>(),
                    Progress = 0
                });

                resetTaskInput();
            }
        }

        private void resetTaskInput()
        {
            newTaskTextBox.Text = "";
            TaskDurationSelectionButton.Content = "Duration";
            taskDuration = 0;
        }

        private void AlignmentMenuFlyoutItem0_Click(object sender, RoutedEventArgs e)
        {
            taskDuration = 30;
            TaskDurationSelectionButton.Content = "30 mins";
        }

        private void AlignmentMenuFlyoutItem1_Click(object sender, RoutedEventArgs e)
        {
            taskDuration = 60;
            TaskDurationSelectionButton.Content = "1 hour";
        }

        private void AlignmentMenuFlyoutItem2_Click(object sender, RoutedEventArgs e)
        {
            taskDuration = 6 * 60;
            TaskDurationSelectionButton.Content = "6 hours";
        }

        private void AlignmentMenuFlyoutItem3_Click(object sender, RoutedEventArgs e)
        {
            taskDuration = 12 * 60;
            TaskDurationSelectionButton.Content = "12 hours";
        }

        private void AlignmentMenuFlyoutItem4_Click(object sender, RoutedEventArgs e)
        {
            taskDuration = 24 * 60;
            TaskDurationSelectionButton.Content = "1 day";
        }

        private void resetDetailPanel()
        {
            HomeSplitView.IsPaneOpen = false;
        }

        private void TaskList_Loaded(object sender, RoutedEventArgs e)
        {
            ViewModel.LoadTaskCommand.ExecuteAsync(null);
        }

        private void TaskList_ItemClick(object sender, TappedRoutedEventArgs e)
        {
            ListView listView = sender as ListView;
            if (((FrameworkElement)e.OriginalSource).DataContext as Todo == null)
            {
                HomeSplitView.IsPaneOpen = false;
            }
            else
            {
                HomeSplitView.IsPaneOpen = true;
            }

        }

        private void ListView_RightTapped(object sender, RightTappedRoutedEventArgs e)
        {
            ListView listView = sender as ListView;
            if (((FrameworkElement)e.OriginalSource).DataContext as Todo != null)
            {
                ItemFlyout.ShowAt(listView, e.GetPosition(listView));
                ViewModel.SelectedTask = ((FrameworkElement)e.OriginalSource).DataContext as Todo;
            }
        }

        private void DeleteTaskFlyoutButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.DeleteTask(ViewModel.SelectedTask);

            resetDetailPanel();
        }

        private void TaskCheckBox_Click(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            Todo task = (Todo)checkBox.DataContext;
            ViewModel.CompleteTask(task);
            resetDetailPanel();
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            AppBarButton button = (AppBarButton)sender;
            Todo task = (Todo)button.DataContext;
            ViewModel.StartTask(task);
        }

        private void FinishButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.CompleteTask(ViewModel.SelectedTask);
            resetDetailPanel();
        }

        private void DetailMenuFlyoutItem_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.StartTask(ViewModel.SelectedTask);
        }
    }
}
