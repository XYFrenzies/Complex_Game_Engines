using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Stats))]
[CanEditMultipleObjects]
public class CustomEditorStats : Editor
{
    SerializedProperty m_primStats;
    SerializedProperty m_secStats;
    private void OnEnable()
    {
        m_primStats = serializedObject.FindProperty("m_primaryStatistic");
        m_secStats = serializedObject.FindProperty("m_secondaryStatistic");
    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(m_primStats, true);
        EditorGUILayout.PropertyField(m_secStats, true);
        serializedObject.ApplyModifiedProperties();

    }
}

public class CustomEditorEntities : Editor
{
    SerializedProperty m_entity;
    private void OnEnable()
    {
        m_entity = serializedObject.FindProperty("entities");
    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(m_entity, true);
        serializedObject.ApplyModifiedProperties();
    }
}
