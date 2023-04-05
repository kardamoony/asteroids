using System;
using UnityEngine;

namespace Asteroids.ServiceLayer.Settings
{
    [Serializable]
    public struct SettingsEntry
    {
        public string Id;
        public string Value;
    }
    
    [CreateAssetMenu(fileName = "GameplaySettings", menuName = "Asteroids/Settings/Gameplay")]
    public class GameplaySettings : ScriptableObject
    {
        public SettingsEntry[] Settings;
    }
}