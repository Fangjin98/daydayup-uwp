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

        private ObservableCollection<StartEndPair> startEndPairs = new();

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
                            Start = TimeStamps[i],
                            End = TimeStamps[i + 1]
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
                            Start = TimeStamps[i],
                            End = TimeStamps[i + 1]
                        });
                }
                startEndPairs.Add(
                    new StartEndPair
                    {
                        IsPaused = false,
                        Start = TimeStamps.Last(),
                        End = DateTime.Now
                    });
            }
        }
    }

    public class StartEndPair
    {
        public bool IsPaused { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public int DurationMins
        {
            get => (End - Start).Minutes;
        }
    }
}
