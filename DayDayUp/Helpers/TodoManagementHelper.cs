using DayDayUp.Models;
using DayDayUp.Services;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using System;
using System.Collections.Generic;

namespace DayDayUp.Helpers
{
    public class TodoManagementHelper
    {
        public List<Todo> UnfinishedTodos
        {
            get { return allTodos.FindAll(t => t.IsFinished == false); }
        }


        public List<Todo> FinishedTodos
        {
            get { return allTodos.FindAll(t => t.IsFinished == true); }
        }

        public TodoManagementHelper()
        {

            _dataAccess = Ioc.Default.GetRequiredService<IDataAccess>();

            allTodos = _dataAccess.GetData();
            
        }

        public void Finish(Todo item)
        {
            item.IsFinished = true;
            item.TimeStamps.Add(DateTime.Now);
            _dataAccess.UpdateDataAsync(item);
        }

        public void RemoveTask(Todo item)
        {
            allTodos.Remove(item);
            _dataAccess.DeleteDataAsync(item);
        }

        public void AddTask(Todo item)
        {
            allTodos.Add(item);
            _dataAccess.AddDataAsync(item);
        }

        public void SwitchStaus(Todo item)
        {
            item.Status = item.Status == TodoStatus.Doing ? TodoStatus.Pause : TodoStatus.Doing;
            item.TimeStamps.Add(DateTime.Now);
            Update(item);
        }
        public void Update(Todo item)
        {
            _dataAccess.UpdateDataAsync(item);
        }

        public void UpdateAll()
        {
            foreach (Todo todo in allTodos)
            {
                _dataAccess.UpdateDataAsync(todo);
            }
        }

        private List<Todo> allTodos;

        private IDataAccess _dataAccess;
    }
}
