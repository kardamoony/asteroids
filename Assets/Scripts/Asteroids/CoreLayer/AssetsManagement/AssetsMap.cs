using System;
using System.Collections.Generic;
using System.Linq;
using Asteroids.CoreLayer.Interfaces;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Asteroids.CoreLayer.AssetsManagement
{
    [CreateAssetMenu(fileName = "AssetsMap", menuName = "Asteroids/DataHolders/AssetsMap")]
    public sealed class AssetsMap : ScriptableObject, IAssetsMap
    {
        [SerializeField] private List<Asset> _assets;   

        public Dictionary<string, AssetReference> GetAssetsMap()
        {
            var map = new Dictionary<string, AssetReference>();
            
            foreach (var asset in _assets)
            {
                map.Add(asset.Id, asset.AssetReference);
            }

            return map;
        }
    }
}