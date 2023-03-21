namespace Asteroids.CoreLayer.Factories
{
    public interface IObjectsFactory
    {
        T Get<T>(string id);
    }
}