using System;
using Generated;
using UnityEditor;

namespace Asteroids.ServiceLayer.Settings.Editor
{
    [CustomEditor(typeof(PlayerSettings))]
    public class PlayerSettingsEditor : SettingsEditorBase
    {
        private PlayerSettings _target;
        private SerializedProperty _projectileIdProperty;
        
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            DrawAssetSelector(_projectileIdProperty);
            serializedObject.ApplyModifiedProperties();
        }

        private void OnEnable()
        {
            _target = serializedObject.targetObject as PlayerSettings;
            
            if (!_target) return;

            _projectileIdProperty = serializedObject.FindProperty(_target.ProjectileIdProperty);
        }
    }
}