using System;

namespace Asteroids.ServiceLayer.Settings.Parsers
{
    public interface ISettingsConverter
    {
        Type Type { get; }
    }
}