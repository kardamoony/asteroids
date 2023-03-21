namespace Asteroids.CoreLayer.IoC
{
    public interface IDependencyResolver
    {
        T Resolve<T>(object[] args = null);
    }
}