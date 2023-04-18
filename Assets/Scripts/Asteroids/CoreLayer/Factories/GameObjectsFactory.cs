using System;
using System.Collections.Generic;
using Asteroids.CoreLayer.Services;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Object = UnityEngine.Object;

namespace Asteroids.CoreLayer.Factories
{
    public class GameObjectsFactory : IObjectsFactory<GameObject>
    {
        private readonly Transform _poolParent;
        private readonly IAddressableService _addressableService;

        private readonly IDictionary<GameObject, string> _inUse = new Dictionary<GameObject, string>();
        private readonly IDictionary<string, Stack<GameObject>> _notInUse = new Dictionary<string, Stack<GameObject>>();

        private readonly IDictionary<string, AsyncOperationHandle<GameObject>> _handles =
            new Dictionary<string, AsyncOperationHandle<GameObject>>();

        public GameObjectsFactory(IAddressableService addressableService, Transform poolParent)
        {
            _addressableService = addressableService;
            _poolParent = poolParent;
        }

        public void Get<T>(string id, Action<T> callback, params object[] args)
        {
            var pool = GetPoolStack(id);

            if (pool.Count > 0)
            {
                var gameObject = pool.Pop();
                _inUse.Add(gameObject, id);

                var obj = gameObject.GetComponent<T>();
                gameObject.transform.SetParent(null);
                callback.Invoke(obj);
                return;
            }

            if (_handles.TryGetValue(id, out var handle))
            {
                Instantiate(handle, id, callback);
                return;
            }
            
            _addressableService.LoadAsync<GameObject>(id, handle =>
            {
                _handles.TryAdd(id, handle);
                Instantiate(handle, id, callback);
            });
        }

        private void Instantiate<T>(AsyncOperationHandle<GameObject> handle, string id, Action<T> callback)
        {
            var gameObject = Object.Instantiate(handle.Result);
            
            var poolBehaviour = gameObject.AddComponent<PoolBehaviour>();
            poolBehaviour.AssetId = id;
            
            _inUse.Add(gameObject, id);
            var obj = gameObject.GetComponent<T>();
            callback.Invoke(obj);
        }

        public void Release(GameObject gameObject, bool dispose)
        {
            if (_inUse.TryGetValue(gameObject, out var id))
            {
                _inUse.Remove(gameObject);
            }
            else if (gameObject.TryGetComponent<PoolBehaviour>(out var poolBehaviour))
            {
                id = poolBehaviour.AssetId;
            }
            else
            {
                throw new Exception($"{gameObject.name} wasn't pooled and cannot be returned");
            }

            if (dispose)
            {
                Object.Destroy(gameObject);
                DestroyPooledObjects(id);

                if (_handles.TryGetValue(id, out var handle))
                {
                    Addressables.Release(handle);
                    _handles.Remove(id);
                }
                
                return;
            }

            gameObject.SetActive(false);
            gameObject.transform.SetParent(_poolParent);
            GetPoolStack(id).Push(gameObject);
        }

        private void DestroyPooledObjects(string id)
        {
            if (_notInUse.TryGetValue(id, out var stack))
            {
                while (stack.Count > 0)
                {
                    Object.Destroy(stack.Pop());
                }

                _notInUse.Remove(id);
            }
        }

        private Stack<GameObject> GetPoolStack(string id)
        {
            if (!_notInUse.TryGetValue(id, out var pool))
            {
                pool = new Stack<GameObject>();
                _notInUse.Add(id, pool);
            }

            return pool;
        }
    }
}