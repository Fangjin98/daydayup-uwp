using DayDayUp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace DayDayUp.Views
{
    public partial class TodoTimeStamp : UserControl
    {
        public Todo todo
        {
            get { return (Todo)GetValue(TodoProperty); }
            set { setValueDp(TodoProperty, value); }
        }

        public static readonly DependencyProperty TodoProperty = DependencyProperty.Register(
           "Todo", typeof(Todo), typeof(TodoTimeStamp), new PropertyMetadata(null));

        public event PropertyChangedEventHandler PropertyChanged;

        public TodoTimeStamp()
        {
            InitializeComponent();
            todo = new();
        }

        private void ItemsRepeater_DataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        {
            Debug.WriteLine("Datacontext changed", "Expander");
        }

        private void setValueDp(DependencyProperty property, object value,
            [CallerMemberName] string p = null)
        {
            SetValue(property, value);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(p));
            Debug.WriteLine("TimeStamps changed", "Expander");
            if (todo.TimeStamps != null)
            {
                updateStartEndPairs();
            }
        }

        private void updateStartEndPairs()
        {
            startEndPairs.Clear();

            var len = todo.IsFinished ? todo.TimeStamps.Count-1 : todo.TimeStamps.Count;

            for(int i = 0; i < len; i++)
            {
                if( i%2 == 0 )
                {
                    startEndPairs.Add(
                        new TimeStampItem
                        {
                            IsPaused = true,
                            Date = todo.TimeStamps[i]
                        });
                }
                else
                {
                    startEndPairs.Add(
                        new TimeStampItem
                        {
                            IsPaused = false,
                            Date = todo.TimeStamps[i]
                        });
                }
            }
        }

        private ObservableCollection<TimeStampItem> startEndPairs = new();
    }

    public struct TimeStampItem
    {
        public DateTime Date{ get; set; }
        public String DateString { get => Date.ToString("g"); }
        public bool IsPaused { get; set; }
        public bool IsStarted { get => !IsPaused; }
    }
}
