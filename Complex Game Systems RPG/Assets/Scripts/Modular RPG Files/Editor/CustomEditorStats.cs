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


    private void OnEnable()
    {
        m_types = serializedObject.FindProperty("m_nameOfType");
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
    private int lengthValue = 1;
    SerializedProperty m_effectiveness;

    private void OnEnable()
    {
        m_effectiveness = serializedObject.FindProperty("effect");
    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        TypesEffected script = (TypesEffected)target;
        for (int i = 0; i < lengthValue; i++)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("Attacking");
            script.indexValue = EditorGUILayout.Popup(script.indexValue, script.nameAttack.ToArray());
            GUILayout.Label("Defending");
            script.indexValue = EditorGUILayout.Popup(script.indexValue, script.nameDefense.ToArray());
            GUILayout.EndHorizontal();
            EditorGUILayout.PropertyField(m_effectiveness, true);
        }
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("More Matchups"))
        {
            lengthValue += 1;
        }
        if (GUILayout.Button("Delete Recent Matchup"))
        {
            lengthValue -= 1;
        }
        GUILayout.EndHorizontal();
        serializedObject.ApplyModifiedProperties();
    }
}
[CustomEditor(typeof(Items))]
[CanEditMultipleObjects]
public class CustomEditorItems : Editor
{
    SerializedProperty m_items;
    private void OnEnable()
    {
        m_items = serializedObject.FindProperty("m_items");
    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(m_items, true);
        serializedObject.ApplyModifiedProperties();
    }
}
