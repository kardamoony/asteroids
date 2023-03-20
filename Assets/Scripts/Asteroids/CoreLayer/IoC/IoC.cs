using Asteroids.CoreLayer.Interfaces;

namespace Asteroids.CoreLayer.IoC
{
    public class IoC
    {
        private static IoC _instance;

        public static IoC Instance
        {
            get
            {
                _instance ??= new IoC();
                return _instance;
            }
        }
        
        public IDependencyContainer Container { get; private set; }
        public IDependencyResolver Resolver { get; private set; }

        public IoC SetContainer(IDependencyContainer container)
        {
            Container = container;
            return this;
        }

        public IoC SetResolver(IDependencyResolver resolver)
        {
            Resolver = resolver;
            return this;
        }
    }
}