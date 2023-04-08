using System;

namespace Asteroids.SimulationLayer.Settings
{
    public interface ISettingsProvider
    {
        T GetValue<T>(Enum settingId);
    }
}