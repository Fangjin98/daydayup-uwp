using Windows.UI.Xaml.Data;
using System;

namespace DayDayUp.Helpers
{
    public class DurationToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var number = (value as int?);
            TimeSpan result = TimeSpan.FromMinutes((double) number);
            return result.ToString("dd':'hh':'mm");
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
