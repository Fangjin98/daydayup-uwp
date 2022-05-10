using System;
using System.Diagnostics;
using Windows.UI.Xaml.Data;

namespace DayDayUp.Helpers
{
    public class DateToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null) { return null; }
            var date = (DateTime) value;
            var diffHour = (DateTime.Now - date).TotalHours;
            if (diffHour<(24-date.Hour)) { return date.ToString("t"); }  // today
            else if (diffHour< (48-date.Hour)) { return "Yesterday"; } // yesterday
            else if (date.Year == DateTime.Today.Year) { return date.ToString("M"); } // this year
            else { return date.ToString("d"); }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
