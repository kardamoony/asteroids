using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Asteroids.CoreLayer.AssetsManagement;
using Asteroids.CoreLayer.Interfaces;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Asteroids.CoreLayer.Services
{
    public sealed class AddressableService : IAddressableService
    {
        private readonly Dictionary<string, AssetReference> _assetsMap;

        public AddressableService(IAssetsMap assetsMap)
        {
            _assetsMap = assetsMap.GetAssetsMap();
        }
        
        public async Task<T> LoadAsync<T>(string id)
        {
            var reference = GetAssetReference(id);
            var handle = Addressables.LoadAssetAsync<T>(reference);
            
            await handle.Task;
            return handle.Result;
        }
        
        public void Release<TObject>(TObject asset)
        {
            Addressables.Release(asset);
        }

        public void ReleaseInstance(GameObject instance)
        {
            Addressables.ReleaseInstance(instance);
        }

        private AssetReference GetAssetReference(string id)
        {
            if (!_assetsMap.TryGetValue(id, out var reference))
            {
                throw new Exception("Unable to find asset with id=" + id);
            }

            return reference;
        }
    }
}