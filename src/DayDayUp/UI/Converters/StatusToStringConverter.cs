using DayDayUp.Models;
using System;
using Windows.UI.Xaml.Data;

namespace DayDayUp.Helpers
{
    public sealed class StatusToStringConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var status = (value as TodoStatus?);
            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();

            return status == TodoStatus.Doing ? loader.GetString("DoingStatus"): loader.GetString("PauseStatus");
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
