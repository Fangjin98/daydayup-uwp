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
        private ObservableCollection<StartEndPair> startEndPairs = new();

        public event PropertyChangedEventHandler PropertyChanged;

        public TodoTimeStamp()
        {
            this.InitializeComponent();
            TimeStamps=new();
        }

        public static readonly DependencyProperty TimeStampProperty = DependencyProperty.Register(
            "TimeStamp", typeof(List<DateTime>), typeof(TodoTimeStamp), new PropertyMetadata(null));

        public List<DateTime> TimeStamps
        {
            get { return (List<DateTime>)GetValue(TimeStampProperty); }
            set { setValueDp(TimeStampProperty, value); }
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
            if (TimeStamps != null)
            {
                updateStartEndPairs();
            }
        }

        private void updateStartEndPairs()
        {
            startEndPairs.Clear();

            if (TimeStamps.Count != 0 && TimeStamps.Count % 2 == 0)
            {
                for (int i = 0; i < TimeStamps.Count; i += 2)
                {
                    startEndPairs.Add(
                        new StartEndPair
                        {
                            IsPaused = true,
                            StartDate = TimeStamps[i],
                            EndDate = TimeStamps[i + 1]
                        });
                }
            }
            else if (TimeStamps.Count != 0 && TimeStamps.Count % 2 == 1)
            {
                for (int i = 0; i < TimeStamps.Count - 1; i += 2)
                {
                    startEndPairs.Add(
                        new StartEndPair
                        {
                            IsPaused = true,
                            StartDate = TimeStamps[i],
                            EndDate = TimeStamps[i + 1]
                        });
                }
                startEndPairs.Add(
                    new StartEndPair
                    {
                        IsPaused = false,
                        StartDate = TimeStamps.Last(),
                        EndDate = DateTime.Now
                    });
            }
        }
    }

    public class StartEndPair
    {
        public bool IsPaused { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string StartTime { get => StartDate.ToString("T"); }
        public string EndTime { get => EndDate.ToString("T"); }

        public bool IsTodayStart { get=> DateTime.Now.ToString("d") != StartDate.ToString("d"); }
        public bool IsTodayEnd { get => DateTime.Now.ToString("d") != EndDate.ToString("d"); }
        public int DurationMins
        {
            get => (EndDate - StartDate).Minutes;
        }
    }
}
