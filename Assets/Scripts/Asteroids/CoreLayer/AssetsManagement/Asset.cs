using System;
using UnityEngine.AddressableAssets;

namespace Asteroids.CoreLayer.AssetsManagement
{
    [Serializable]
    public struct Asset
    {
        public string Id;
        public AssetReference AssetReference;
    }
}