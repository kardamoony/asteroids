using System;
using System.Collections.Generic;

namespace Asteroids.CoreLayer.IoC
{
    public class MinimalisticIoCContainer : IDependencyContainer, IDependencyResolver
    {
        private readonly Dictionary<Type, Func<object[], object>> _constructors = new Dictionary<Type, Func<object[], object>>();
        private readonly Dictionary<Type, object> _instances = new Dictionary<Type, object>();

        public void Register<T>(Func<object[], object> constructor)
        {
            var type = typeof(T);

            if (_constructors.ContainsKey(type))
            {
                throw new ArgumentException($"[{GetType().Name}] {typeof(T)} is already registered!");
            }
            
            _constructors.Add(type, constructor);
        }

        public void RegisterInstance<T>(T instance)
        {
            var type = typeof(T);
            
            if (_instances.ContainsKey(type))
            {
                throw new ArgumentException($"[{GetType().Name}] {typeof(T)} is already registered!");
            }
            
            _instances.Add(type, instance);
        }

        public T Resolve<T>(params object[] args)
        {
            var type = typeof(T);
            
            if (_instances.TryGetValue(type, out var instance))
            {
                return (T)instance;
            }

            if (_constructors.TryGetValue(type, out var ctor))
            {
                return (T)ctor.Invoke(args);
            }
            
            throw new ArgumentException($"[{GetType().Name}] {typeof(T)} dependency not found!");
        }
    }
}