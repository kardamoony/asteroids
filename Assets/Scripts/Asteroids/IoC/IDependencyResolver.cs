namespace Asteroids.IoC
{
    public interface IDependencyResolver
    {
        T Resolve<T>(params object[] args);
    }
}