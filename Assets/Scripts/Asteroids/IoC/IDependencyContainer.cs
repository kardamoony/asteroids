using System;

namespace Asteroids.IoC
{
    public interface IDependencyContainer
    {
        void Register<T>(Func<object[], object> constructor);
        void Unregister(Type type);
        void RegisterInstance<T>(T instance);
    }
}