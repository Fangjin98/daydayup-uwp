using LiteDB;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DayDayUp.Models
{
  
    public class Todo : ObservableObject
    {

        public int Id { get; set; }

        // init by users
        private string name;

        public string Name
        {
            get { return name; }
            set { SetProperty(ref name, value); }
        }

        public int ExpectedDurationMins { get; set; }

        // set by users
        public string Description { get; set; }

        public bool IsFinished { get; set; }

        private TodoStatus status;

        public TodoStatus Status
        {
            get { return status; }
            set { SetProperty(ref status, value); }
        }

        // record by app
        public DateTime CreationDate { get; set; }

        public List<DateTime> TimeStamps { get; set; }

        // init by program
        [BsonIgnore]
        public int DurationMins { get; set; }
        [BsonIgnore]
        public DateTime ExpectedFinishDate { get; set; }
        [BsonIgnore]
        public int Progress { get; set; } //[0,>100]
    }

    public enum TodoStatus
    {
        Doing = 0,
        Pause = 1
    }
}
