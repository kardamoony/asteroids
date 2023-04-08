namespace Asteroids.IoC
{
    public class Locator
    {
        private static Locator _instance;

        public static Locator Instance
        {
            get
            {
                _instance ??= new Locator();
                return _instance;
            }
        }
        
        public IDependencyContainer Container { get; private set; }
        public IDependencyResolver Resolver { get; private set; }

        public Locator SetContainer(IDependencyContainer container)
        {
            Container = container;
            return this;
        }

        public Locator SetResolver(IDependencyResolver resolver)
        {
            Resolver = resolver;
            return this;
        }
    }
}