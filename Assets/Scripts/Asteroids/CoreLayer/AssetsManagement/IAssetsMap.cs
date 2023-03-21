using System.Collections.Generic;
using UnityEngine.AddressableAssets;

namespace Asteroids.CoreLayer.AssetsManagement
{
    public interface IAssetsMap
    {
        Dictionary<string, AssetReference> GetAssetsMap();
    }
}