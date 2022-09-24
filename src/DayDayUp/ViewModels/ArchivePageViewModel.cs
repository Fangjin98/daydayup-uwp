using DayDayUp.Models;
using DayDayUp.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DayDayUp.ViewModels
{
    public class ArchivePageViewModel : BaseTodoListViewModel
    {
        internal ArchivePageStrings Strings => LanguageManager.Instance.ArchivePage;

        public ArchivePageViewModel(TodoManager TodoManager):
            base(TodoManager)
        {
        }
        
        public void Restore(TodoListItemViewModel todoItem)
        {
            todoItem.todo.IsFinished = false;
            todoItem.todo.TimeStamps.Remove(todoItem.todo.TimeStamps.Last());
            todoManager.Update(todoItem.todo);
            TodoItems.Remove(todoItem);
        }

        protected override async Task LoadTodoAsync()
        {
            using (await loadingLock.LockAsync())
            {
                TodoItems.Clear();
                var tmp = todoManager.FinishedTodos;
                tmp.Reverse();
                foreach (Todo item in tmp)
                {
                    TodoItems.Add(new TodoListItemViewModel(item));
                }
            }
        }
    }

}
