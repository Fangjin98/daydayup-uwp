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

            allTodos = new List<Todo>();
            
        }

        public void FinishTodo(Todo item)
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

        public void Update(Todo item)
        {
            _dataAccess.UpdateDataAsync(item);
        }

        public void CalDurationAndProgress(Todo item)
        {
            if (item.ExpectedDurationMins != 0)
            {
                int len = item.TimeStamps.Count;
                double totalDuration = 0;
                if (item.IsFinished==true && len == 1) //only finish date, no duration
                {
                    item.Progress = 100;
                    item.DurationMins = 0;
                    return;
                }
                
                if (len % 2 == 0)
                {
                    for (int i = 0; i < len; i += 2)
                    {
                        totalDuration += (item.TimeStamps[i + 1] - item.TimeStamps[i]).TotalMinutes;
                    }
                }
                else
                {
                    if (item.IsFinished == true) // Last date is the finish date.
                    {
                        for (int i = 0; i < len - 1; i += 2)
                        {
                            totalDuration += (item.TimeStamps[i + 1] - item.TimeStamps[i]).TotalMinutes;
                        }
                    }
                    else
                    {
                        for (int i = 0; i < len - 1; i += 2)
                        {
                            totalDuration += (item.TimeStamps[i + 1] - item.TimeStamps[i]).TotalMinutes;
                        }
                        totalDuration += (DateTime.Now - item.TimeStamps[len - 1]).TotalMinutes;
                    }
                }
                item.Progress = Convert.ToInt32(totalDuration / item.ExpectedDurationMins * 100);
                item.DurationMins = Convert.ToInt32(totalDuration);
            }
            else
            {
                item.Progress = 0;
                item.DurationMins = 0;
            }

        }

        private List<Todo> allTodos;

        private IDataAccess _dataAccess;
    }
}
