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
    private int lengthValue = 1;
    private int lengthValueProperties = 1;
    SerializedProperty m_items;
    private void OnEnable()
    {
        m_items = serializedObject.FindProperty("m_items");
    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        Items script = (Items)target;
        GUILayout.Space(20f);
        GUILayout.Label("Items", EditorStyles.boldLabel);
        GUILayout.Space(10f);
        GUILayout.Label("Item Description");

        for (int i = 0; i < m_items.arraySize; i++)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("Player Name", GUILayout.Width(150));
            script.m_items[i].name = GUILayout.TextField(script.m_items[i].name);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("Item Value", GUILayout.Width(150));
            script.m_items[i].valueOfItem = EditorGUILayout.IntField(script.m_items[i].valueOfItem);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("Is it a Percentage or Integer", GUILayout.Width(150));
            script.m_items[i].isAPercentage = EditorGUILayout.Toggle(script.m_items[i].isAPercentage);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("Item Properties", GUILayout.Width(150));
            for (int j = 0; j < lengthValueProperties; j++)
            {
                script.m_items[i].properties = (ItemProperties)EditorGUILayout.EnumPopup(script.m_items[i].properties);
            }
            GUILayout.EndHorizontal();
            GUILayout.Space(10f);
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Add More Properties"))
            {
                lengthValueProperties += 1;
            }
            if (GUILayout.Button("Delete More Properties"))
            {
                if (lengthValueProperties > 1)
                    lengthValueProperties -= 1;
            }
            GUILayout.EndHorizontal();
            GUILayout.Space(10f);
            GUILayout.BeginHorizontal();
            GUILayout.Label("Typing");
            for (int j = 0; j < lengthValue; j++)
            {
                script.indexValue = EditorGUILayout.Popup(script.indexValue, script.m_items[i].m_typeVariation.ToArray());
            }
            GUILayout.EndHorizontal();
            GUILayout.Space(10f);
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Add More Types"))
            {
                lengthValue += 1;
            }
            if (GUILayout.Button("Delete More Types"))
            {
                if (lengthValue > 1)
                    lengthValue -= 1;
            }
            GUILayout.EndHorizontal();
        }
        GUILayout.Space(10f);
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Add More Items"))
        {
            script.AddToList();
        }
        if (GUILayout.Button("Delete More Items"))
        {
            if (script.m_items.Count > 1)
                script.m_items.RemoveAt(script.m_items.Count - 1);
        }
        GUILayout.EndHorizontal();
        serializedObject.ApplyModifiedProperties();
    }
}
