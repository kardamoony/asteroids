using System;

namespace Asteroids.ServiceLayer.Settings.Converters
{
    public interface ISettingsConverter
    {
        Type Type { get; }
    }
}