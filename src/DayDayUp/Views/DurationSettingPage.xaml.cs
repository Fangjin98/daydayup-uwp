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
        public int DurationResult{ get => durationPicker.Duration; }

        public DurationSettingPage(int duration)
        {
            this.InitializeComponent();
            durationPicker = new DurationPicker(duration);
        }
        
        private DurationPicker durationPicker;

        private List<string> minuteValues = (from minute in Enumerable.Range(0,60) select minute.ToString()).ToList();

        private List<string> hourValues = (from hour in Enumerable.Range(0, 24) select hour.ToString()).ToList();

        private List<string> dayValues = (from day in Enumerable.Range(0, 8) select day.ToString()).ToList();


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
