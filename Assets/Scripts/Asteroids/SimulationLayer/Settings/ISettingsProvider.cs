namespace Asteroids.SimulationLayer.Settings
{
    public interface ISettingsProvider
    {
        T GetValue<T>(string id);
    }
}