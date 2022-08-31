using Windows.UI.Xaml.Data;
using System;

namespace DayDayUp.Helpers
{
    public class DurationToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var number = (value as int?);
            if (number == 0) { return "None"; }
            else if (number < 60) { return toTimeString(number,true); }
            else
            {
                var hours = System.Convert.ToInt32(number / 60);
                var minutes = System.Convert.ToInt32(number % 60);
                if (minutes==0) { return toTimeString(hours,false); }
                else
                {
                    return toTimeString(hours, false) + " : " + toTimeString(minutes, true);
                }
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }

        private string toTimeString(int? minutes,bool isMinute)
        {
            if (minutes == 1) { return isMinute==true? minutes.ToString() + " minute" : minutes.ToString()+ " hour"; }
            else return isMinute == true ? minutes.ToString() + " minutes": minutes.ToString() + " hours";
        }
    }
}
