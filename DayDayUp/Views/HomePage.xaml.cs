using DayDayUp.Models;
using DayDayUp.ViewModels;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

namespace DayDayUp.Views
{

    public sealed partial class HomePage : Page
    {
        public HomePageViewModel ViewModel => (HomePageViewModel)DataContext;

        public HomePage()
        {
            InitializeComponent();
            DataContext = Ioc.Default.GetRequiredService<HomePageViewModel>();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            Debug.WriteLine("Homepage navigated from", "Update all");
            ViewModel.UpdateAll();
        }

        private void newTaskTextBoxKeyUp(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Enter && newTaskTextBox.Text != "")
            {
                ViewModel.AddTaskCommand.ExecuteAsync(new Todo
                {
                    Name = newTaskTextBox.Text,
                    ExpectedDurationMins = 0,
                    IsFinished = false,
                    Status = TodoStatus.Pause,
                    CreationDate = DateTime.Now,
                    TimeStamps = new List<DateTime>(),
                    Progress = 0
                });

                resetTaskInput();
            }
        }

        private void AlignmentMenuFlyoutItem0_Click(object sender, RoutedEventArgs e)
        {
            if (newTaskTextBox.Text != "")
            {
                ViewModel.AddTaskCommand.ExecuteAsync(new Todo
                {
                    Name = newTaskTextBox.Text,
                    ExpectedDurationMins = 30,
                    IsFinished = false,
                    Status = TodoStatus.Pause,
                    CreationDate = DateTime.Now,
                    TimeStamps = new List<DateTime>(),
                    Progress = 0
                });

                resetTaskInput();
            }
        }

        private void AlignmentMenuFlyoutItem1_Click(object sender, RoutedEventArgs e)
        {
            if (newTaskTextBox.Text != "")
            {
                ViewModel.AddTaskCommand.ExecuteAsync(new Todo
                {
                    Name = newTaskTextBox.Text,
                    ExpectedDurationMins = 60,
                    IsFinished = false,
                    Status = TodoStatus.Pause,
                    CreationDate = DateTime.Now,
                    TimeStamps = new List<DateTime>(),
                    Progress = 0
                });

                resetTaskInput();
            }
        }

        private void AlignmentMenuFlyoutItem2_Click(object sender, RoutedEventArgs e)
        {
            if (newTaskTextBox.Text != "")
            {
                ViewModel.AddTaskCommand.ExecuteAsync(new Todo
                {
                    Name = newTaskTextBox.Text,
                    ExpectedDurationMins = 6 * 60,
                    IsFinished = false,
                    Status = TodoStatus.Pause,
                    CreationDate = DateTime.Now,
                    TimeStamps = new List<DateTime>(),
                    Progress = 0
                });

                resetTaskInput();
            }
        }

        private void AlignmentMenuFlyoutItem3_Click(object sender, RoutedEventArgs e)
        {
            if (newTaskTextBox.Text != "")
            {
                ViewModel.AddTaskCommand.ExecuteAsync(new Todo
                {
                    Name = newTaskTextBox.Text,
                    ExpectedDurationMins = 12 * 60,
                    IsFinished = false,
                    Status = TodoStatus.Pause,
                    CreationDate = DateTime.Now,
                    TimeStamps = new List<DateTime>(),
                    Progress = 0
                });

                resetTaskInput();
            }
        }

        private void AlignmentMenuFlyoutItem4_Click(object sender, RoutedEventArgs e)
        {
            if (newTaskTextBox.Text != "")
            {
                ViewModel.AddTaskCommand.ExecuteAsync(new Todo
                {
                    Name = newTaskTextBox.Text,
                    ExpectedDurationMins = 24 * 60,
                    IsFinished = false,
                    Status = TodoStatus.Pause,
                    CreationDate = DateTime.Now,
                    TimeStamps = new List<DateTime>(),
                    Progress = 0
                });

                resetTaskInput();
            }
        }

        private void AlignmentMenuFlyoutItem5_Click(object sender, RoutedEventArgs e)
        {
            if (newTaskTextBox.Text != "")
            {
                ViewModel.AddTaskCommand.ExecuteAsync(new Todo
                {
                    Name = newTaskTextBox.Text,
                    ExpectedDurationMins = 0,
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
                TodoItemFlyout.ShowAt(listView, e.GetPosition(listView));
                ViewModel.SelectedTask = ((FrameworkElement)e.OriginalSource).DataContext as Todo;
            }
        }

        private void DeleteTaskFlyoutButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.DeleteTask(ViewModel.SelectedTask);

            resetDetailPanel();
        }

        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            Todo task = (Todo)checkBox.DataContext;
            ViewModel.CompleteTask(task);
            resetDetailPanel();
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            Todo task = (Todo)button.DataContext;
            ViewModel.SwapTodoStatus(task);
        }

        private void DetailPanelTaskName_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            var textbox=(TextBox)sender;
            ViewModel.SelectedTask.Name=textbox.Text;
            ViewModel.Update(ViewModel.SelectedTask);
        }

        private async void ExpectedDurationMinsButton_Click(object sender, RoutedEventArgs e)
        {
            ContentDialog dialog = new ContentDialog();
            dialog.Title = "Set the duration";
            dialog.PrimaryButtonText = "Save";
            dialog.CloseButtonText = "Cancel";
            dialog.DefaultButton = ContentDialogButton.Primary;
            dialog.Content = new DurationSettingPage(ViewModel.SelectedTask.ExpectedDurationMins);

            var result = await dialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                DurationSettingPage tmp = (DurationSettingPage)dialog.Content;
                ViewModel.SelectedTask.ExpectedDurationMins = tmp.DurationResult;
            }
        }
    }
}
