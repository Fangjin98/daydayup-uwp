using DayDayUp.Helpers;
using DayDayUp.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Nito.AsyncEx;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
            taskManager.Finish(task);

            if (selectedTask == task)
            {
                selectedTask = null;
            }
        }

        public void SwapTodoStatus(Todo task)
        {
            task.Status = task.Status == TodoStatus.Doing ? TodoStatus.Pause : TodoStatus.Doing;
            taskManager.Update(task);
        }

        public void Update(Todo item)
        {
            taskManager.Update(item);
        }

        public void UpdateAll()
        {
            foreach(Todo todo in MyTasks)
            {
                Debug.WriteLine("Update todo", "VIEWMODEL");
                taskManager.Update(todo);
            }
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

        private readonly TodoManagementHelper taskManager;

        private Todo selectedTask = new ();

        private readonly AsyncLock LoadingLock = new AsyncLock();   
    }
}
