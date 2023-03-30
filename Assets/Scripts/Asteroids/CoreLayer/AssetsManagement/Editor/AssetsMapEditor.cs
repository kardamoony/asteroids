using UnityEditor;
using UnityEngine;

namespace Asteroids.CoreLayer.AssetsManagement.Editor
{
    [CustomEditor(typeof(AssetsMap))]
    public class AssetsMapEditor : UnityEditor.Editor
    {
        private AssetsMap _target;
        private SerializedProperty _assetsList;
        private SerializedProperty _assetsFolder;
        private SerializedProperty _assetsDescriptionFolder;

        private bool _showAssetsFoldout;

        public override void OnInspectorGUI()
        {
            DrawString(_assetsFolder);
            DrawString(_assetsDescriptionFolder);

            serializedObject.ApplyModifiedProperties();

            if (GUILayout.Button("Collect Assets"))
            {
                _target.CollectAssets();
            }

            _showAssetsFoldout = EditorGUILayout.Foldout(_showAssetsFoldout, "Assets");

            if (_showAssetsFoldout)
            {
                EditorGUI.indentLevel = 1;
                DrawList(_assetsList);
            }
        }

        private void DrawString(SerializedProperty property)
        {
            EditorGUI.BeginChangeCheck();

            var value = EditorGUILayout.TextField(property.stringValue);

            if (EditorGUI.EndChangeCheck())
            {
                property.stringValue = value;
            }
        }

        private void DrawList(SerializedProperty property)
        {
            if (!property.isArray)
            {
                return;
            }

            var size = property.arraySize;

            for (var i = 0; i < size; i++)
            {
                var element = property.GetArrayElementAtIndex(i);
                EditorGUILayout.PropertyField(element);
            }
        }

        private void OnEnable()
        {
            _target = serializedObject.targetObject as AssetsMap;
            
            if (!_target) return;

            _assetsList = serializedObject.FindProperty(_target.AssetsPropertyName);
            _assetsFolder = serializedObject.FindProperty(_target.AssetsFolder);
            _assetsDescriptionFolder = serializedObject.FindProperty(_target.AssetsDescriptionFolder);
        }
    }
}