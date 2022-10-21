using DayDayUp.Models;
using DayDayUp.Services;

namespace DayDayUp.ViewModels
{
    public class TodoListItemViewModel
    {
        public string BiasString => LanguageManager.Instance.ArchivePage.Bias;

        public string ProgressString => LanguageManager.Instance.HomePage.Progress;

        public Todo todo = new();

        public TodoListItemViewModel(Todo todo)
        {
            this.todo = todo;
        }

        public TodoListItemViewModel()
        {
        }
    }
}
