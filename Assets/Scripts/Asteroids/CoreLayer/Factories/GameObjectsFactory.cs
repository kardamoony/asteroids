using System;
using System.Collections.Generic;
using Asteroids.CoreLayer.Services;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Asteroids.CoreLayer.Factories
{
    public class GameObjectsFactory : IObjectsFactory<GameObject>
    {
        private readonly Transform _poolParent;
        private readonly IAddressableService _addressableService;

        private readonly IDictionary<GameObject, string> _inUse = new Dictionary<GameObject, string>();
        private readonly IDictionary<string, Stack<GameObject>> _notInUse = new  Dictionary<string, Stack<GameObject>>();

        public GameObjectsFactory(IAddressableService addressableService, Transform poolParent)
        {
            _addressableService = addressableService;
            _poolParent = poolParent;
        }

        public void Get<T>(string id, Action<T> callback)
        {
            var pool = GetPoolStack(id);

            if (pool.Count > 0)
            {
                var gameObject = pool.Pop();
                _inUse.Add(gameObject, id);

                var obj = gameObject.GetComponent<T>();
                callback.Invoke(obj);
            }
            
            _addressableService.LoadAsync<GameObject>(id, handle =>
            {
                var gameObject = Object.Instantiate(handle.Result);
                var obj = gameObject.GetComponent<T>();
                callback.Invoke(obj);
            });
        }

        public void Release(GameObject gameObject)
        {
            if (!_inUse.TryGetValue(gameObject, out var id))
            {
                throw new Exception($"{gameObject.name} wasn't pooled and cannot be returned");
            }

            _inUse.Remove(gameObject);
            gameObject.SetActive(false);
            gameObject.transform.SetParent(_poolParent);
            GetPoolStack(id).Push(gameObject);
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