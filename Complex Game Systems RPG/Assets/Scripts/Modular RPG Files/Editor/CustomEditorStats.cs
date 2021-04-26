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
[CustomEditor(typeof(Entities))]
[CanEditMultipleObjects]
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
[CustomEditor(typeof(StatusEffects))]
[CanEditMultipleObjects]
public class CustomEditorStatus : Editor
{
    SerializedProperty m_status;
    private void OnEnable()
    {
        m_status = serializedObject.FindProperty("m_statusEffects");
    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(m_status, true);
        serializedObject.ApplyModifiedProperties();
    }
}

[CustomEditor(typeof(MoveSets))]
[CanEditMultipleObjects]
public class CustomEditorMoveSets : Editor
{
    SerializedProperty m_moveSets;
    private void OnEnable()
    {
        m_moveSets = serializedObject.FindProperty("m_moveSets");
    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(m_moveSets, true);
        serializedObject.ApplyModifiedProperties();
    }
}
[CustomEditor(typeof(TypeChart))]
[CanEditMultipleObjects]
public class CustomEditorTypeChart : Editor
{
    SerializedProperty m_types;
    SerializedProperty m_typeAttackVsDefend;

    private void OnEnable()
    {
        m_types = serializedObject.FindProperty("m_typing");
    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(m_types, true);
        serializedObject.ApplyModifiedProperties();
    }
}
[CustomEditor(typeof(TypesEffected))]
[CanEditMultipleObjects]
public class CustomEditorTypesEffected : Editor
{
    SerializedProperty m_types;

    private void OnEnable()
    {
        m_types = serializedObject.FindProperty("m_typeAttackVsDefend");
    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(m_types, true);
        serializedObject.ApplyModifiedProperties();
    }
}
