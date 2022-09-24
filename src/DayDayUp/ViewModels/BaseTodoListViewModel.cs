using DayDayUp.Models;
using DayDayUp.Services;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Nito.AsyncEx;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace DayDayUp.ViewModels
{
    public abstract class BaseTodoListViewModel : ObservableRecipient
    {

        private TodoListItemViewModel selectedTodoItem=new();

        protected abstract Task LoadTodoAsync();

        protected readonly AsyncLock loadingLock = new AsyncLock();

        protected readonly TodoManager todoManager;

        public IAsyncRelayCommand LoadTodoCommand { get; }

        public ObservableCollection<TodoListItemViewModel> TodoItems { get; set; } = new();

        public TodoListItemViewModel SelectedTodo
        {
            get => selectedTodoItem;
            set => SetProperty(ref selectedTodoItem, value);
        }

        public BaseTodoListViewModel(TodoManager TodoManager)
        {
            todoManager = TodoManager;

            LoadTodoCommand = new AsyncRelayCommand(LoadTodoAsync);
        }

        public void Delete(TodoListItemViewModel todoItem)
        {
            TodoItems.Remove(todoItem);
            todoManager.Remove(todoItem.todo);

            if (SelectedTodo == todoItem)
            {
                SelectedTodo = null;
            }
        }

        public void Update(TodoListItemViewModel todoItem)
        {
            todoManager.Update(todoItem.todo);
        }
    }
}
