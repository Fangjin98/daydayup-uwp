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

        public void CalDurationAndProgress(Todo item)
        {
            if (item.ExpectedDurationMins != 0)
            {
                int len = item.TimeStamps.Count;
                double totalDuration = 0;
                if (item.IsFinished == true) 
                {
                    if (len == 1) // directly finish the todo, only finish date
                    {
                        item.Progress = 100;
                        item.DurationMins = 0;
                        return;
                    }
                    else if (len %2 == 0) // todo is finished under the doing status
                    {
                        for (int i = 0; i < len; i += 2)
                        {
                            totalDuration += (item.TimeStamps[i + 1] - item.TimeStamps[i]).TotalMinutes;
                        }
                    }
                    else // todo is finished under the pause status, the finish date can't be counted
                    {
                        for (int i = 0; i < len - 1; i += 2)
                        {
                            totalDuration += (item.TimeStamps[i + 1] - item.TimeStamps[i]).TotalMinutes;
                        }
                    }
                }
                else // unfinished todos, no finish date
                {
                    if (len %2 == 0)
                    {
                        for (int i = 0; i < len; i += 2)
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
