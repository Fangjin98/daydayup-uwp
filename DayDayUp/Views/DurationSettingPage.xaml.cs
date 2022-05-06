using DayDayUp.ViewModels;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;


namespace DayDayUp.Views
{
    public sealed partial class DurationSettingPage : Page
    {
        public DurationSettingPage(int duration)
        {
            this.InitializeComponent();

            durationPicker = new DurationPicker(duration);
            
            for(int i = 0; i <= 60; i++)
            {
                offsetValues.Add(i.ToString()); 
            }

            for (int i = 0; i < 8; i++)
            {
                dayOffsetValues.Add(i.ToString());
            }

        }
        public int DurationResult
        {
            get => durationPicker.Duration;
        }
        private DurationPicker durationPicker;

        private List<string> offsetValues = new List<string>();

        private List<string> dayOffsetValues = new List<string>();

    }
    internal class DurationPicker : ObservableObject
    {
        public DurationPicker(int duration)
        {
            this.duration = duration;
        }

        private int duration;
        public int Duration
        {
            get => duration;
            set=> SetProperty(ref duration, value);
        }

        private int daysOffset;
        public int DaysOffset
        {
            get {
                return Duration / 24 / 60;
            }
            set
            {
                daysOffset = value;
                Duration = daysOffset * 24 * 60 + HoursOffset * 60 + MinsOffset;
            }
        }

        private int hoursOffset;
        public int HoursOffset
        {
            get
            {
                return (Duration-DaysOffset*24*60)/ 60;
            }
            set
            {
                hoursOffset = value;
                Duration = DaysOffset * 24 * 60 + hoursOffset * 60 + MinsOffset;
            }
        }

        private int minsOffset;
        public int MinsOffset
        {
            get
            {
                return Duration - DaysOffset * 24 * 60 - HoursOffset * 60;
            }
            set
            {
                minsOffset = value;
                Duration = DaysOffset * 24 * 60 + HoursOffset * 60 + minsOffset;
            }
        }

    }
}
