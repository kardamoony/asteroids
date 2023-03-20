namespace Asteroids.CoreLayer.Interfaces
{
    public interface IDependencyResolver
    {
        T Resolve<T>(object[] args = null);
    }
}