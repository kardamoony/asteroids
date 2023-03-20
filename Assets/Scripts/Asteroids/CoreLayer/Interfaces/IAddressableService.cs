﻿using System.Threading.Tasks;
using UnityEngine;

namespace Asteroids.CoreLayer.Interfaces
{
    public interface IAddressableService
    {
        Task<T> LoadAsync<T>(string id);
        void Release<TObject>(TObject asset);
        void ReleaseInstance(GameObject instance);
        
    }
}