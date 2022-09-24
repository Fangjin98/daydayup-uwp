using DayDayUp.Models;
using DayDayUp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
