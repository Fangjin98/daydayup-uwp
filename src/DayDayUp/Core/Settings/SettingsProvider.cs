#nullable enable

using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Composition;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace DayDayUp.Core.Settings
{
    public interface ISettingsProvider
    {
        /// <summary>
        /// Raised when a setting value has changed.
        /// </summary>
        event EventHandler<SettingChangedEventArgs>? SettingChanged;

        /// <summary>
        /// Gets the value of a defined setting.
        /// </summary>
        /// <typeparam name="T">The type of value that will be retrieved.</typeparam>
        /// <param name="settingDefinition">The <see cref="SettingDefinition{T}"/> that defines the targetted setting.</param>
        /// <returns>Return the value of the setting or its default value.</returns>
        T GetSetting<T>(SettingDefinition<T> settingDefinition);

        /// <summary>
        /// Sets the value of a given setting.
        /// </summary>
        /// <typeparam name="T">The type of value that will be set.</typeparam>
        /// <param name="settingDefinition">The <see cref="SettingDefinition{T}"/> that defines the targetted setting.</param>
        /// <param name="value">The value to set</param>
        void SetSetting<T>(SettingDefinition<T> settingDefinition, T value);

        /// <summary>
        /// Resets a given setting to its default value.
        /// </summary>
        void ResetSetting<T>(SettingDefinition<T> settingDefinition);
    }

    [Export(typeof(ISettingsProvider))]
    [Shared]
    internal sealed class SettingsProvider : ISettingsProvider
    {
        private readonly ApplicationDataContainer _roamingSettings = ApplicationData.Current.RoamingSettings;
        private readonly ApplicationDataContainer _localSettings = ApplicationData.Current.LocalSettings;

        public event EventHandler<SettingChangedEventArgs>? SettingChanged;

        public T GetSetting<T>(SettingDefinition<T> settingDefinition)
        {
            ApplicationDataContainer applicationDataContainer;
            if (settingDefinition.IsRoaming)
            {
                applicationDataContainer = _roamingSettings;
            }
            else
            {
                applicationDataContainer = _localSettings;
            }

            if (applicationDataContainer.Values.ContainsKey(settingDefinition.Name))
            {
                if (typeof(T).IsEnum)
                {
                    return (T)Enum.Parse(typeof(T), applicationDataContainer.Values[settingDefinition.Name]?.ToString() ?? string.Empty);
                }
                else if (typeof(IList).IsAssignableFrom(typeof(T)))
                {
                    return JsonConvert.DeserializeObject<T>(applicationDataContainer.Values[settingDefinition.Name]?.ToString() ?? string.Empty)!;
                }

                return (T)Convert.ChangeType(applicationDataContainer.Values[settingDefinition.Name], typeof(T), CultureInfo.InvariantCulture);
            }

            SetSetting(settingDefinition, settingDefinition.DefaultValue);
            return settingDefinition.DefaultValue;
        }

        public void SetSetting<T>(SettingDefinition<T> settingDefinition, T value)
        {
            object? valueToSave = value;
            if (value is Enum valueEnum)
            {
                valueToSave = valueEnum.ToString();
            }
            else if (value is IList list)
            {
                valueToSave = JsonConvert.SerializeObject(list, Formatting.None);
            }

            if (settingDefinition.IsRoaming)
            {
                _roamingSettings.Values[settingDefinition.Name] = valueToSave;
            }
            else
            {
                _localSettings.Values[settingDefinition.Name] = valueToSave;
            }

            SettingChanged?.Invoke(this, new SettingChangedEventArgs(settingDefinition.Name, value));
        }

        public void ResetSetting<T>(SettingDefinition<T> settingDefinition)
        {
            if (settingDefinition.IsRoaming)
            {
                _roamingSettings.Values.Remove(settingDefinition.Name);
            }
            else
            {
                _localSettings.Values.Remove(settingDefinition.Name);
            }

            SettingChanged?.Invoke(this, new SettingChangedEventArgs(settingDefinition.Name, settingDefinition.DefaultValue));
        }
    }
}
