﻿using DayDayUp.Models;
using DayDayUp.Services;
using DayDayUp.ViewModels;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            Debug.WriteLine("Navigated from", "Homepage");
        }

        private void newTaskTextBoxKeyUp(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Enter && newTaskTextBox.Text != "")
            {
                ViewModel.AddTodoCommand.ExecuteAsync(new Todo
                {
                    Name = newTaskTextBox.Text,
                    ExpectedDurationMins = 0,
                    IsFinished = false,
                    Status = TodoStatus.Pause,
                    CreationDate = DateTime.Now,
                    TimeStamps = new List<DateTime>(),
                });

                resetTaskInput();
            }
        }

        private void AlignmentMenuFlyoutItem0_Click(object sender, RoutedEventArgs e)
        {
            if (newTaskTextBox.Text != "")
            {
                ViewModel.AddTodoCommand.ExecuteAsync(new Todo
                {
                    Name = newTaskTextBox.Text,
                    ExpectedDurationMins = 30,
                    IsFinished = false,
                    Status = TodoStatus.Pause,
                    CreationDate = DateTime.Now,
                    TimeStamps = new List<DateTime>(),
                });

                resetTaskInput();
            }
        }

        private void AlignmentMenuFlyoutItem1_Click(object sender, RoutedEventArgs e)
        {
            if (newTaskTextBox.Text != "")
            {
                ViewModel.AddTodoCommand.ExecuteAsync(new Todo
                {
                    Name = newTaskTextBox.Text,
                    ExpectedDurationMins = 60,
                    IsFinished = false,
                    Status = TodoStatus.Pause,
                    CreationDate = DateTime.Now,
                    TimeStamps = new List<DateTime>()
                });

                resetTaskInput();
            }
        }

        private void AlignmentMenuFlyoutItem2_Click(object sender, RoutedEventArgs e)
        {
            if (newTaskTextBox.Text != "")
            {
                ViewModel.AddTodoCommand.ExecuteAsync(new Todo
                {
                    Name = newTaskTextBox.Text,
                    ExpectedDurationMins = 6 * 60,
                    IsFinished = false,
                    Status = TodoStatus.Pause,
                    CreationDate = DateTime.Now,
                    TimeStamps = new List<DateTime>()
                });

                resetTaskInput();
            }
        }

        private void AlignmentMenuFlyoutItem3_Click(object sender, RoutedEventArgs e)
        {
            if (newTaskTextBox.Text != "")
            {
                ViewModel.AddTodoCommand.ExecuteAsync(new Todo
                {
                    Name = newTaskTextBox.Text,
                    ExpectedDurationMins = 12 * 60,
                    IsFinished = false,
                    Status = TodoStatus.Pause,
                    CreationDate = DateTime.Now,
                    TimeStamps = new List<DateTime>()
                });

                resetTaskInput();
            }
        }

        private void AlignmentMenuFlyoutItem4_Click(object sender, RoutedEventArgs e)
        {
            if (newTaskTextBox.Text != "")
            {
                ViewModel.AddTodoCommand.ExecuteAsync(new Todo
                {
                    Name = newTaskTextBox.Text,
                    ExpectedDurationMins = 24 * 60,
                    IsFinished = false,
                    Status = TodoStatus.Pause,
                    CreationDate = DateTime.Now,
                    TimeStamps = new List<DateTime>()
                });

                resetTaskInput();
            }
        }

        private void AlignmentMenuFlyoutItem5_Click(object sender, RoutedEventArgs e)
        {
            if (newTaskTextBox.Text != "")
            {
                ViewModel.AddTodoCommand.ExecuteAsync(new Todo
                {
                    Name = newTaskTextBox.Text,
                    ExpectedDurationMins = 0,
                    IsFinished = false,
                    Status = TodoStatus.Pause,
                    CreationDate = DateTime.Now,
                    TimeStamps = new List<DateTime>()
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
            ViewModel.LoadTodoCommand.ExecuteAsync(null);
        }

        private void TaskList_ItemClick(object sender, TappedRoutedEventArgs e)
        {
            if (((FrameworkElement)e.OriginalSource).DataContext as TodoListItemViewModel == null)
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
            if (((FrameworkElement)e.OriginalSource).DataContext as TodoListItemViewModel != null)
            {
                TodoItemFlyout.ShowAt(listView, e.GetPosition(listView));
                ViewModel.SelectedTodo = ((FrameworkElement)e.OriginalSource).DataContext as TodoListItemViewModel;
            }
        }

        private void DeleteTaskFlyoutButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.Delete(ViewModel.SelectedTodo);

            resetDetailPanel();
        }

        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            TodoListItemViewModel task = (TodoListItemViewModel) checkBox.DataContext;
            ViewModel.Complete(task);
            resetDetailPanel();
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            TodoListItemViewModel todo = (TodoListItemViewModel)button.DataContext;
            ViewModel.SwapTodoStatus(todo);
        }

        private void DetailPanelTaskName_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            var textbox=(TextBox)sender;
            ViewModel.SelectedTodo.todo.Name=textbox.Text;
            ViewModel.Update(ViewModel.SelectedTodo);
        }

        private async void ExpectedDurationMinsButton_Click(object sender, RoutedEventArgs e)
        {
            ContentDialog dialog = new ContentDialog();
            dialog.Title = ViewModel.Strings.SetTheDuration;
            dialog.PrimaryButtonText = ViewModel.Strings.Save;
            dialog.CloseButtonText = ViewModel.Strings.Cancel;
            dialog.DefaultButton = ContentDialogButton.Primary;
            dialog.Content = new DurationSettingPage(ViewModel.SelectedTodo.todo.ExpectedDurationMins);

            var result = await dialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                DurationSettingPage tmp = (DurationSettingPage)dialog.Content;
                ViewModel.SelectedTodo.todo.ExpectedDurationMins = tmp.DurationResult;
                ViewModel.Update(ViewModel.SelectedTodo);
            }
        }
        private void StatusButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.SwapTodoStatus(ViewModel.SelectedTodo);
        }

        private async void PredictionButton_Click(object sender, RoutedEventArgs e)
        {
            ContentDialog dialog = new ContentDialog();
            dialog.PrimaryButtonText = ViewModel.Strings.OK;
            dialog.Content = new DurationPredictionPage(
                Ioc.Default.GetRequiredService<TodoManager>(),
                ViewModel.SelectedTodo.todo.ExpectedDurationMins);

            await dialog.ShowAsync();
        }
    }
}
