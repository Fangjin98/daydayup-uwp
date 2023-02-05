using Windows.UI.Xaml.Data;
using System;

namespace DayDayUp.Helpers
{
    public class DurationToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var number = (value as int?);
            var result = TimeSpan.FromMinutes((double)number);
            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
            var durationString = "";

            if(result.TotalMinutes ==0) { return loader.GetString("None"); }

            if(result.Days > 0) { durationString += (result.Days.ToString() + " " + loader.GetString("Day")+" "); }
            if(result.Hours > 0) { durationString += (result.Hours.ToString() + " " + loader.GetString("Hour")+" "); }
            if(result.Minutes > 0) { durationString += (result.Minutes.ToString() + " " + loader.GetString("Minute")); }
            return durationString;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
