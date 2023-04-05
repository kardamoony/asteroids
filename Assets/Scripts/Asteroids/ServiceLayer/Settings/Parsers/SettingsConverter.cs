using System;

namespace Asteroids.ServiceLayer.Settings.Parsers
{
    public abstract class SettingsConverter<T> : ISettingsConverter
    {
        public Type Type => typeof(T);
        public abstract T Convert(string value);
    }
}