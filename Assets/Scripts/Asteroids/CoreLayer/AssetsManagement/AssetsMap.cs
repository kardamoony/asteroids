using System.Collections.Generic;
using System.IO;
using Asteroids.CoreLayer.Generation;
using Unity.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Asteroids.CoreLayer.AssetsManagement
{
    [CreateAssetMenu(fileName = "AssetsMap", menuName = "Asteroids/DataHolders/AssetsMap")]
    public sealed class AssetsMap : ScriptableObject, IAssetsMap
    {
        private const string AssetsEnumName = "AssetId";
        
        [SerializeField] private string _assetsFolder = "Assets/Content/AssetsMap/AddressableAssets";
        [SerializeField] private string _assetsDescriptionFolder;
        [SerializeField] [ReadOnly] private List<Asset> _assets;  
        
        public string AssetsPropertyName => nameof(_assets);
        public string AssetsFolder => nameof(_assetsFolder);
        public string AssetsDescriptionFolder => nameof(_assetsDescriptionFolder);

        public Dictionary<string, AssetReference> GetAssetsMap()
        {
            var map = new Dictionary<string, AssetReference>();
            
            foreach (var asset in _assets)
            {
                map.Add(asset.Id, asset.AssetReference);
            }

            return map;
        }
        
        public void CollectAssets()
        {
            if (Application.isPlaying) return;
            
            AssetDatabaseHelper.CheckFolder(_assetsDescriptionFolder);

            var references = AssetDatabaseHelper.GetAssetReferencesInFolder(_assetsFolder);
            
            _assets.Clear();
            
            var generatedDescriptionPath = _assetsDescriptionFolder + "/" + AssetsEnumName + ".cs";

            if (File.Exists(generatedDescriptionPath))
            {
                AssetDatabase.DeleteAsset(generatedDescriptionPath);
            }

            var generator = new EnumGenerator(AssetsEnumName, true);

            foreach (var pair in references)
            {
                generator.AppendMember(pair.Key);
                _assets.Add(new Asset { Id = pair.Key, AssetReference = pair.Value });
            }
            
            generator.CloseEnum();

            File.WriteAllText(generatedDescriptionPath, generator.ToString());
            AssetDatabase.ImportAsset(generatedDescriptionPath);
            EditorUtility.SetDirty(this);
        }

    }
}