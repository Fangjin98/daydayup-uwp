using DayDayUp.Helpers;
using DayDayUp.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Nito.AsyncEx;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayDayUp.ViewModels
{
    public sealed class HomePageViewModel : ObservableRecipient
    {

        public HomePageViewModel(TodoManagementHelper TaskManager)
        {
            taskManager = TaskManager;

            LoadTaskCommand = new AsyncRelayCommand(LoadTaskAsync);
            AddTaskCommand = new AsyncRelayCommand<Todo>(AddTaskAsync);
        }

        public IAsyncRelayCommand LoadTaskCommand { get; }
        public IAsyncRelayCommand AddTaskCommand { get; }

        public ObservableCollection<Todo> MyTasks { get; set; } = new();

        public Todo SelectedTask
        {
            get => selectedTask;
            set => SetProperty(ref selectedTask, value);
        }

        public void DeleteTask(Todo task)
        {
            MyTasks.Remove(task);
            taskManager.RemoveTask(task);
        }

        public void CompleteTask(Todo task)
        {
            MyTasks.Remove(task);
            taskManager.FinishTodo(task);

            if (selectedTask == task)
            {
                selectedTask = null;
            }
        }

        public void StartTask(Todo task)
        {
            if (task.Status == TodoStatus.Pause)
            {
                task.Status = TodoStatus.Doing;
                moveToTop(task);
            }
            else
            {
                task.Status = TodoStatus.Pause;
            }
            task.TimeStamps.Add(DateTime.Now);
            taskManager.Update(task);
        }

        private async Task AddTaskAsync(Todo task)
        {
            using (await LoadingLock.LockAsync())
            {
                try
                {
                    taskManager.AddTask(task);
                }
                catch
                {
                    return;
                }
            }
            MyTasks.Add(task);
        }

        private async Task LoadTaskAsync()
        {

            using (await LoadingLock.LockAsync())
            {
                try
                {

                    MyTasks.Clear();

                    foreach (Todo item in taskManager.UnfinishedTodos)
                    {
                        taskManager.CalDurationAndProgress(item);
                        MyTasks.Add(item);
                    }
                }
                catch
                {
                    // Whoops!
                }
            }
        }

        private void moveToTop(Todo task)
        {
            if (MyTasks.IndexOf(task) != 0)
            {
                MyTasks.Move(MyTasks.IndexOf(task), 0);
                taskManager.Update(task);
            }
        }


        private readonly TodoManagementHelper taskManager;

        private Todo selectedTask = new Todo();

        private readonly AsyncLock LoadingLock = new AsyncLock();

    }
}
