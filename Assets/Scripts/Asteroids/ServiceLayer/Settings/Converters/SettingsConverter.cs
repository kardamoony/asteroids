using System;

namespace Asteroids.ServiceLayer.Settings.Converters
{
    public abstract class SettingsConverter<T> : ISettingsConverter
    {
        public Type Type => typeof(T);
        public abstract T Convert(string value);
    }
}