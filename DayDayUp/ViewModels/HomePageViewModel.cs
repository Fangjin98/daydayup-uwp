using DayDayUp.Helpers;
using DayDayUp.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Nito.AsyncEx;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace DayDayUp.ViewModels
{
    public sealed class HomePageViewModel : ObservableRecipient
    {

        public HomePageViewModel(TodoManagementHelper TaskManager)
        {
            todoManager = TaskManager;

            LoadTaskCommand = new AsyncRelayCommand(LoadTaskAsync);
            AddTaskCommand = new AsyncRelayCommand<Todo>(AddTaskAsync);
        }

        public IAsyncRelayCommand LoadTaskCommand { get; }

        public IAsyncRelayCommand AddTaskCommand { get; }

        public ObservableCollection<Todo> MyTasks { get; set; } = new();

        private Todo selectedTask = new();

        public Todo SelectedTask
        {
            get => selectedTask;
            set => SetProperty(ref selectedTask, value);
        }

        public void DeleteTask(Todo task)
        {
            MyTasks.Remove(task);
            todoManager.RemoveTask(task);

            if (SelectedTask == task)
            {
                SelectedTask = null;
            }
        }

        public void CompleteTask(Todo task)
        {
            MyTasks.Remove(task);
            todoManager.Finish(task);

            if (SelectedTask == task)
            {
                SelectedTask = null;
            }
        }

        public void SwapTodoStatus(Todo task)
        {
           todoManager.SwitchStaus(task);
        }

        public void Update(Todo item)
        {
            todoManager.Update(item);
        }

        private async Task AddTaskAsync(Todo task)
        {
            using (await LoadingLock.LockAsync())
            {
                try
                {
                    todoManager.AddTask(task);
                }
                catch
                {
                    return;
                }
                MyTasks.Add(task);
            }
        }

        private async Task LoadTaskAsync()
        {

            using (await LoadingLock.LockAsync())
            {
                MyTasks.Clear();

                foreach (Todo item in todoManager.DurationAndProgress(todoManager.UnfinishedTodos))
                {
                    MyTasks.Add(item);
                }
            }
        }


        private readonly TodoManagementHelper todoManager;

        private readonly AsyncLock LoadingLock = new AsyncLock();   
    }
}
