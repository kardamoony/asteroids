using System;
using Generated;
using UnityEditor;

namespace Asteroids.ServiceLayer.Settings.Editor
{
    public abstract class SettingsEditorBase : UnityEditor.Editor
    {
        protected void DrawAssetSelector(SerializedProperty serializedProperty)
        {
            EditorGUI.BeginChangeCheck();
            
            if (!Enum.TryParse<AssetId>(serializedProperty.stringValue, out var enumMember))
            {
                enumMember = AssetId.None;
            }

            var value = EditorGUILayout.EnumPopup(enumMember);

            if (EditorGUI.EndChangeCheck())
            {
                serializedProperty.stringValue = value.ToString();
            }
        }
    }
}