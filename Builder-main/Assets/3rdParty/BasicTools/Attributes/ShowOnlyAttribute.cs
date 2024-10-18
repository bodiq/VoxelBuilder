using UnityEditor;
using UnityEngine;

public class ShowOnlyAttribute : PropertyAttribute
{
}

#if UNITY_EDITOR

[CustomPropertyDrawer(typeof(ShowOnlyAttribute))]
public class ShowOnlyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty prop, GUIContent label)
    {
        var valueStr = prop.propertyType switch
        {
            SerializedPropertyType.Integer => prop.intValue.ToString(),
            SerializedPropertyType.Boolean => prop.boolValue.ToString(),
            SerializedPropertyType.Float => prop.floatValue.ToString("0.0000"),
            SerializedPropertyType.String => prop.stringValue,
            _ => "(not supported)"
        };

        EditorGUI.LabelField(position, label.text, valueStr);
    }
}

#endif