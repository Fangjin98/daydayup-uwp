﻿using DayDayUp.Services;
using System;

namespace DayDayUp.Core.Settings
{
    public static class PredefinedSettings
    {
        public static readonly SettingDefinition<string> Language
            = new(
                name: nameof(Language),
                isRoaming: false,
                defaultValue: "default");
        
        public static readonly SettingDefinition<AppTheme> Theme
            = new(
                name: nameof(Theme),
                isRoaming: false,
                defaultValue: AppTheme.Default);
    }


    /// <summary>
    /// Represents the definition of a setting in the application.
    /// </summary>
    /// <typeparam name="T">The type of value of the setting</typeparam>
    public readonly struct SettingDefinition<T> : IEquatable<SettingDefinition<T>>
    {
        /// <summary>
        /// Gets whether the setting can be synchronized with the user's Microsoft account.
        /// </summary>
        public bool IsRoaming { get; }

        /// <summary>
        /// Gets the name of the setting.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the default value of the setting.
        /// </summary>
        public T DefaultValue { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SettingDefinition"/> structure.
        /// </summary>
        /// <param name="name">The name of the setting. Should be unique.</param>
        /// <param name="isRoaming">Defines whether the setting can be synchronized with the user's Microsoft account.</param>
        /// <param name="defaultValue">The default value of the setting.</param>
        public SettingDefinition(string name, bool isRoaming, T defaultValue)
        {
            if (string.IsNullOrEmpty(name) || name.Length > 255)
            {
                // For both LocalSettings and RoamingSettings, the name of each setting can be 255 characters in length at most.
                // see https://docs.microsoft.com/en-us/uwp/api/windows.storage.applicationdata.localsettings?view=winrt-22000#remarks
                throw new ArgumentOutOfRangeException(nameof(name));
            }

            IsRoaming = isRoaming;
            Name = name;
            DefaultValue = defaultValue;
        }

        public override bool Equals(object? obj)
        {
            if (obj is SettingDefinition<T> definition)
            {
                return Equals(definition);
            }

            return false;
        }

        public bool Equals(SettingDefinition<T> other)
        {
            return other.IsRoaming == IsRoaming
                && string.Equals(other.Name, Name, StringComparison.Ordinal)
                && other.DefaultValue is not null
                && other.DefaultValue.Equals(DefaultValue);
        }

        public override int GetHashCode()
        {
            return (IsRoaming.GetHashCode() ^ 137)
                 * (Name.GetHashCode() ^ 47)
                 * (DefaultValue is null ? 13 : DefaultValue.GetHashCode() ^ 73);
        }

        public static bool operator ==(SettingDefinition<T> left, SettingDefinition<T> right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(SettingDefinition<T> left, SettingDefinition<T> right)
        {
            return !(left == right);
        }
    }
}
