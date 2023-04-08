using System;
using Asteroids.CoreLayer.AssetsManagement;
using Asteroids.CoreLayer.Generation;
using UnityEngine;

namespace Asteroids.ServiceLayer.Settings
{
    [Serializable]
    public struct SettingsScope
    {
        public string Id;
        public SettingsEntry[] Settings;
    }
    
    [Serializable]
    public struct SettingsEntry
    {
        public string Id;
        public string Value;
    }
    
    [CreateAssetMenu(fileName = "GameplaySettings", menuName = "Asteroids/Settings/Gameplay")]
    public class GameplaySettings : ScriptableObject
    {
        [SerializeField] private string _settingsDescriptionFolder = "Assets/Scripts/Asteroids/CoreLayer/AssetsManagement/Generated";
        
        public SettingsScope[] Settings;

        public void GenerateIds()
        {
            foreach (var scope in Settings)
            {
                var enumName = scope.Id;
                
                var generator = new EnumGenerator(enumName);

                foreach (var entry in scope.Settings)
                {
                    generator.AppendMember(entry.Id);
                }

                generator.CloseEnum();
                generator.ToString().SaveToFile(_settingsDescriptionFolder, enumName, "cs");
            }
        }
    }
}