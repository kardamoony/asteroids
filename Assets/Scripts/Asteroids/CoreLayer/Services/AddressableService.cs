using System;
using System.Collections.Generic;
using Asteroids.CoreLayer.AssetsManagement;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Asteroids.CoreLayer.Services
{
    public sealed class AddressableService : IAddressableService
    {
        private readonly Dictionary<string, AssetReference> _assetsMap;
        //private readonly Dictionary<string, AsyncOperationHandle> _handles;

        public AddressableService(IAssetsMap assetsMap)
        {
            _assetsMap = assetsMap.GetAssetsMap();
        }

        public async void Initialize()
        {
            var handle = Addressables.InitializeAsync();
            await handle.Task;
        }
        
        public void LoadAsync<TObject>(string id, Action<AsyncOperationHandle<TObject>> callback)
        {
            var reference = GetAssetReference(id);
            var handle = Addressables.LoadAssetAsync<TObject>(reference);

            handle.Completed += callback;
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