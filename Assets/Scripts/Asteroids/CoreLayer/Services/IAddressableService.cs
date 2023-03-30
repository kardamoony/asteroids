using System;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Asteroids.CoreLayer.Services
{
    public interface IAddressableService
    {
        void Initialize();
        void LoadAsync<TObject>(string id, Action<AsyncOperationHandle<TObject>> callback);
        void Release<TObject>(TObject asset);
        void ReleaseInstance(GameObject instance);
    }
}