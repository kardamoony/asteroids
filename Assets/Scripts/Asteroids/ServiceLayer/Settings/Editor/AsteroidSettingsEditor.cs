using UnityEditor;

namespace Asteroids.ServiceLayer.Settings.Editor
{
    [CustomEditor(typeof(AsteroidSettings))]
    public class AsteroidSettingsEditor : SettingsEditorBase
    {
        private AsteroidSettings _target;
        private SerializedProperty _asteroidIdProperty;
        
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            DrawAssetSelector(_asteroidIdProperty);
            serializedObject.ApplyModifiedProperties();
        }

        private void OnEnable()
        {
            _target = serializedObject.targetObject as AsteroidSettings;
            
            if (!_target) return;

            _asteroidIdProperty = serializedObject.FindProperty(_target.AsteroidAssetPropertyName);
        }
    }
}