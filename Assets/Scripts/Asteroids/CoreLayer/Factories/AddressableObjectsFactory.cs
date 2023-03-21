using Asteroids.CoreLayer.AssetsManagement;
using Asteroids.CoreLayer.Interfaces;

namespace Asteroids.CoreLayer.Factories
{
    public sealed class AddressableObjectsFactory : IObjectsFactory
    {
        private readonly IAddressableService _addressableService;
        
        public AddressableObjectsFactory(IAddressableService addressableService)
        {
            _addressableService = addressableService;
        }

        public T Get<T>(string id)
        {
            var loadResult = _addressableService.LoadAsync<T>(id);
            return loadResult.Result;
        }
    }
}