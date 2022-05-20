using Windows.UI.Xaml.Data;
using System;

namespace DayDayUp.Helpers
{
    public class DoubleToRatioConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var ratio = (value as double?);
            return Math.Round((decimal)(ratio * 100),0).ToString()+"%";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }

    }
}
