using System.Collections.Generic;
using UnityEngine.AddressableAssets;

namespace Asteroids.CoreLayer.Interfaces
{
    public interface IAssetsMap
    {
        Dictionary<string, AssetReference> GetAssetsMap();
    }
}