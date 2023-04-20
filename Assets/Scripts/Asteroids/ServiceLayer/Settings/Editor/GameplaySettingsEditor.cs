#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

namespace Asteroids.ServiceLayer.Settings.Editor
{
    [CustomEditor(typeof(GameplaySettings))]
    public class GameplaySettingsEditor : UnityEditor.Editor
    {
        private GameplaySettings _target;

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            DrawGenerateButton();
        }

        private void DrawGenerateButton()
        {
            if (GUILayout.Button("Generate Id Enum"))
            {
                _target.GenerateIds();
            }
        }
        
        private void OnEnable()
        {
            _target = serializedObject.targetObject as GameplaySettings;
        }
    }
}

#endif