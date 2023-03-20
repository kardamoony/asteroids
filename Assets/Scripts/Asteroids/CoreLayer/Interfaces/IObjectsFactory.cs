namespace Asteroids.CoreLayer.Interfaces
{
    public interface IObjectsFactory
    {
        T Get<T>(string id);
    }
}