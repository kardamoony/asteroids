using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine.AddressableAssets;
using Object = UnityEngine.Object;

namespace Asteroids.CoreLayer.AssetsManagement
{
    public static class AssetDatabaseHelper
    {
        public static Dictionary<string, AssetReference> GetAssetReferencesInFolder(string folderPath)
        {
            CheckFolder(folderPath);
            
            var assets = new Dictionary<string, AssetReference>();
            
            var guids = AssetDatabase.FindAssets("", new[] { folderPath });

            foreach (var guid in guids)
            {
                var path = AssetDatabase.GUIDToAssetPath(guid);
                
                if (AssetDatabase.IsValidFolder(path))
                {
                    continue;
                }
                
                var asset = AssetDatabase.LoadAssetAtPath<Object>(path);
                
                if (assets.ContainsKey(asset.name))
                {
                    continue;
                }
                
                assets.Add(asset.name, new AssetReference(guid));
            }

            return assets;
        }

        public static void CheckFolder(string folderPath)
        {
            if (!AssetDatabase.IsValidFolder(folderPath))
            {
                throw new ArgumentException($"{folderPath} is not a valid folder!");
            }
        }
    }
}