using System;

namespace Asteroids.CoreLayer.Interfaces
{
    public interface IDependencyContainer
    {
        void Register<T>(Func<object[], object> constructor);
        void RegisterInstance<T>(T instance);
    }
}