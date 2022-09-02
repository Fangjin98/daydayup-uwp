#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayDayUp.Core.Settings
{
    public sealed class SettingChangedEventArgs : EventArgs
    {
        public string SettingName { get; }

        public object? NewValue { get; }

        public SettingChangedEventArgs(string settingName, object? newValue)
        {
            SettingName = settingName;
            NewValue = newValue;
        }
    }
}
