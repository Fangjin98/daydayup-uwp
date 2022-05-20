using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using System;

namespace DayDayUp.Helpers
{
    public class CountToVisibilityConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var count = (value as int?);
            return count == 0? Visibility.Collapsed : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    public class CountToVisibilityNegativeConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var count = (value as int?);
            return count == 0 ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
