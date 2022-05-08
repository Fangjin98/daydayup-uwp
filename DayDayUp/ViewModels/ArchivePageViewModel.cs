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
    public class ArchivePageViewModel : BaseTodoListViewModel
    {

        public ArchivePageViewModel(TodoManagementHelper TodoManager):
            base(TodoManager)
        {
        }
        
        public void Restore(Todo item)
        {
            item.IsFinished = false;
            item.TimeStamps.Remove(item.TimeStamps.Last());
            todoManager.Update(item);
            Todos.Remove(item);
        }

        protected override async Task LoadTodoAsync()
        {
            using (await loadingLock.LockAsync())
            {
                Todos.Clear();

                foreach (Todo item in todoManager.FinishedTodos)
                {
                    Todos.Add(item);
                }
            }
        }
    }
}
