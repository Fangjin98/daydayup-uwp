using DayDayUp.Models;
using DayDayUp.Services;
using Microsoft.Toolkit.Mvvm.Input;
using System.Threading.Tasks;

namespace DayDayUp.ViewModels
{
    public sealed class HomePageViewModel : BaseTodoListViewModel
    {
        public IAsyncRelayCommand AddTodoCommand { get; }

        internal HomePageStrings Strings = LanguageManager.Instance.HomePage;

        public HomePageViewModel(TodoManager TodoManager):
            base(TodoManager)
        {
            AddTodoCommand = new AsyncRelayCommand<Todo>(AddTodoAsync);
        }

        public void SwapTodoStatus(TodoListItemViewModel todoItem)
        {
            todoManager.SwitchStaus(todoItem.todo);
        }

        public void Complete(TodoListItemViewModel todoItem)
        {
            TodoItems.Remove(todoItem);
            todoManager.Finish(todoItem.todo);

            if (SelectedTodo == todoItem)
            {
                SelectedTodo = null;
            }
        }

        private async Task AddTodoAsync(Todo todo)
        {
            using (await loadingLock.LockAsync())
            {
                try
                {
                    todoManager.Add(todo);
                }
                catch
                {
                    return;
                }
                TodoItems.Add(new TodoListItemViewModel(todo));
            }
        }

        protected override async Task LoadTodoAsync()
        {

            using (await loadingLock.LockAsync())
            {
                TodoItems.Clear();

                foreach (Todo todo in todoManager.UnfinishedTodos)
                {
                    TodoItems.Add(new TodoListItemViewModel(todo));
                }
            }
        }
    }
}
