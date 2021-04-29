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
    public void IntOrPercentageConvertPrim(Stats a_script, int a_parameter, bool isPercent)
    {
        GUILayout.BeginHorizontal();
        GUILayout.Label("Stat Value", GUILayout.Width(150));
        a_script.m_primaryStatistic[a_parameter].stats = EditorGUILayout.DoubleField(
            a_script.m_primaryStatistic[a_parameter].stats);
        if (isPercent)
            GUILayout.Label("%", GUILayout.Width(150));
        else
            GUILayout.Label("Units", GUILayout.Width(150));
        GUILayout.EndHorizontal();
    }
    public void IntOrPercentageConvertSec(Stats a_script, int a_parameter, bool isPercent)
    {
        GUILayout.BeginHorizontal();
        GUILayout.Label("Stat Value", GUILayout.Width(150));
        a_script.m_secondaryStatistic[a_parameter].stats = EditorGUILayout.DoubleField(
            a_script.m_secondaryStatistic[a_parameter].stats);
        if (isPercent)
            GUILayout.Label("%", GUILayout.Width(150));
        else
            GUILayout.Label("Units", GUILayout.Width(150));
        GUILayout.EndHorizontal();
    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        Stats script = (Stats)target;
        #region PrimaryStats
        //Primary Stats
        GUILayout.Label("Primary Stats", EditorStyles.boldLabel);

        for (int i = 0; i < m_primStats.arraySize; i++)
        {
            script.m_primaryStatistic[i].showItem = EditorGUILayout.Foldout(script.m_primaryStatistic[i].showItem, script.m_primaryStatistic[i].name, true);
            if (script.m_primaryStatistic[i].showItem)
            {
                GUILayout.Label("Stat Description");
                GUILayout.BeginHorizontal();
                GUILayout.Label("Stat Name", GUILayout.Width(150));
                script.m_primaryStatistic[i].name = GUILayout.TextField(script.m_primaryStatistic[i].name);
                GUILayout.EndHorizontal();
            }
        }
        GUILayout.Space(20f);
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Add More Primary Stats"))
        {
            script.AddToPrimaryArray();
        }
        if (GUILayout.Button("Delete More Stats"))
        {
            if (script.m_primaryStatistic.Count > 1)
                script.m_primaryStatistic.RemoveAt(script.m_primaryStatistic.Count - 1);
        }
        GUILayout.EndHorizontal();
        #endregion
        #region SecondaryStats
        GUILayout.Space(20f);
        GUILayout.Label("Secondary Stats", EditorStyles.boldLabel);
        for (int i = 0; i < m_secStats.arraySize; i++)
        {
            script.m_secondaryStatistic[i].showItem = EditorGUILayout.Foldout(script.m_secondaryStatistic[i].showItem, script.m_secondaryStatistic[i].name, true);
            if (script.m_secondaryStatistic[i].showItem)
            {
                GUILayout.Label("Stat Description");
                GUILayout.BeginHorizontal();
                GUILayout.Label("Stat Name", GUILayout.Width(150));
                script.m_secondaryStatistic[i].name = GUILayout.TextField(script.m_secondaryStatistic[i].name);
                GUILayout.EndHorizontal();
            }
        }
        GUILayout.Space(20f);
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Add More Secondary Stats"))
        {
            script.AddToSecondaryArray();
        }
        if (GUILayout.Button("Delete More Stats"))
        {
            if (script.m_secondaryStatistic.Count > 1)
                script.m_secondaryStatistic.RemoveAt(script.m_secondaryStatistic.Count - 1);
        }
        GUILayout.EndHorizontal();
        #endregion
    }
}
[CustomEditor(typeof(Entities))]
[CanEditMultipleObjects]
public class CustomEditorEntities : Editor
{
    SerializedProperty m_entity;
    private string percentage = "Percentage";
    private void OnEnable()
    {
        m_entity = serializedObject.FindProperty("entities");
    }
    public void IntOrPercentageConvertPrim(Entities a_script, int a_parameterStat, int a_parameterEntity, bool isPercent)
    {
        GUILayout.BeginHorizontal();
        GUILayout.Label("Stat Value", GUILayout.Width(150));
        a_script.entities[a_parameterEntity].m_stats.m_primaryStatistic[a_parameterStat].stats = EditorGUILayout.DoubleField(
    a_script.entities[a_parameterEntity].m_stats.m_primaryStatistic[a_parameterStat].stats);
        if (isPercent)
            GUILayout.Label("%", GUILayout.Width(150));
        else
            GUILayout.Label("Units", GUILayout.Width(150));
        GUILayout.EndHorizontal();
    }
    public void IntOrPercentageConvertSec(Entities a_script, int a_parameterStat, int a_parameterEntity, bool isPercent)
    {
        GUILayout.BeginHorizontal();
        GUILayout.Label("Stat Value", GUILayout.Width(150));
        a_script.entities[a_parameterEntity].m_stats.m_secondaryStatistic[a_parameterStat].stats = EditorGUILayout.DoubleField(
            a_script.entities[a_parameterEntity].m_stats.m_secondaryStatistic[a_parameterStat].stats);
        if (isPercent)
            GUILayout.Label("%", GUILayout.Width(150));
        else
            GUILayout.Label("Units", GUILayout.Width(150));
        GUILayout.EndHorizontal();
    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        Entities script = (Entities)target;

        GUILayout.Space(20f);
        GUILayout.Label("Entities:", EditorStyles.boldLabel);
        GUILayout.Space(10f);
        for (int i = 0; i < m_entity.arraySize; i++)
        {
            script.entities[i].showItem = EditorGUILayout.Foldout(script.entities[i].showItem, script.entities[i].m_name, true);
            if (script.entities[i].showItem)
            {
                #region EntityStart
                GUILayout.Label("Entity Description");
                GUILayout.BeginHorizontal();
                GUILayout.Label("Entity Name", GUILayout.Width(150));
                script.entities[i].m_name = GUILayout.TextField(script.entities[i].m_name);
                GUILayout.EndHorizontal();
                //Asking the developer to put in a gameobject.

                GUILayout.BeginHorizontal();
                GUILayout.Label("Health", GUILayout.Width(150));
                script.entities[i].m_health = EditorGUILayout.FloatField(script.entities[i].m_health, GUILayout.Width(150));
                GUILayout.EndHorizontal();
                #endregion
                #region PrimaryStats
                GUILayout.Label(script.entities[i].m_name + "'s Primary Stats", EditorStyles.boldLabel);
                GUILayout.Space(10f);

                for (int j = 0; j < script.entities[i].m_primaryStats.Count; j++)
                {
                    script.entities[i].m_primaryStats[j].showItem = EditorGUILayout.Foldout(script.entities[i].m_primaryStats[j].showItem,
                        script.entities[i].m_primaryStats[j].name, true);
                    if (script.entities[i].m_primaryStats[j].showItem)
                    {
                        GUILayout.Label("Stat Description");
                        GUILayout.Label(script.entities[i].m_primaryStats[j].name + " Stat", GUILayout.Width(150));
                        //Determining if its a percentage or integer it is effecting.
                        GUILayout.BeginHorizontal();
                        GUILayout.Label("Is it a Percentage or Whole Number", GUILayout.Width(250));
                        GUILayout.EndHorizontal();
                        GUILayout.BeginHorizontal();
                        //Button for converting to a percentage
                        if (GUILayout.Button(percentage, GUILayout.Width(250)))
                        {
                            if (script.entities[i].m_primaryStats[j].isItAPercent == true)
                            {
                                percentage = "Percentage";
                                script.entities[i].m_primaryStats[j].isItAPercent = false;
                            }
                            else if (script.entities[i].m_primaryStats[j].isItAPercent == false)
                            {
                                percentage = "Whole Number";
                                script.entities[i].m_primaryStats[j].isItAPercent = true;
                            }
                        }
                        GUILayout.EndHorizontal();
                        GUILayout.BeginHorizontal();
                        if (script.entities[i].m_primaryStats[j].isItAPercent)
                        {
                            IntOrPercentageConvertPrim(script, j, i, script.entities[i].m_primaryStats[j].isItAPercent);
                        }
                        else if (!script.entities[i].m_primaryStats[j].isItAPercent)
                        {
                            IntOrPercentageConvertPrim(script, j, i, script.entities[i].m_primaryStats[j].isItAPercent);
                        }
                        GUILayout.EndHorizontal();
                    }
                }
                #endregion
                #region SecondaryStats
                GUILayout.Space(20f);
                GUILayout.Label("Secondary Stats", EditorStyles.boldLabel);
                GUILayout.Space(10f);

                for (int j = 0; j < script.entities[i].m_secondaryStats.Count; j++)
                {
                    script.entities[i].m_secondaryStats[j].showItem = EditorGUILayout.Foldout(script.entities[i].m_secondaryStats[j].showItem,
                        script.entities[i].m_secondaryStats[j].name, true);
                    if (script.entities[i].m_secondaryStats[j].showItem)
                    {
                        GUILayout.Label("Stat Description");
                        GUILayout.Label(script.entities[i].m_secondaryStats[j].name + " Stat", GUILayout.Width(150));
                        //Determining if its a percentage or integer it is effecting.
                        GUILayout.BeginHorizontal();
                        GUILayout.Label("Is it a Percentage or Whole Number", GUILayout.Width(250));
                        GUILayout.EndHorizontal();
                        GUILayout.BeginHorizontal();
                        //Button for converting to a percentage
                        if (GUILayout.Button(percentage, GUILayout.Width(250)))
                        {
                            if (script.entities[i].m_secondaryStats[j].isItAPercent == true)
                            {
                                percentage = "Percentage";
                                script.entities[i].m_secondaryStats[j].isItAPercent = false;
                            }
                            else if (script.entities[i].m_secondaryStats[j].isItAPercent == false)
                            {
                                percentage = "Whole Number";
                                script.entities[i].m_secondaryStats[j].isItAPercent = true;
                            }
                        }
                        GUILayout.EndHorizontal();
                        GUILayout.BeginHorizontal();
                        if (script.entities[i].m_secondaryStats[j].isItAPercent)
                        {
                            IntOrPercentageConvertSec(script, j, i, script.entities[i].m_secondaryStats[j].isItAPercent);
                        }
                        else if (!script.entities[i].m_secondaryStats[j].isItAPercent)
                        {
                            IntOrPercentageConvertSec(script, j, i, script.entities[i].m_secondaryStats[j].isItAPercent);
                        }
                        GUILayout.EndHorizontal();
                    }
                }
                #endregion
            }
        }
        GUILayout.Space(20f);
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Add More Entities"))
        {
            script.AddEntity();
        }
        if (GUILayout.Button("Delete More Entities"))
        {
            if (script.entities.Count > 1)
                script.entities.RemoveAt(script.entities.Count - 1);
        }
        GUILayout.EndHorizontal();
        serializedObject.ApplyModifiedProperties();
    }
}
[CustomEditor(typeof(StatusEffects))]
[CanEditMultipleObjects]
public class CustomEditorStatus : Editor
{
    private string percentage = "Percentage";

    SerializedProperty m_status;

    public void IntOrPercentageConvertPrim(StatusEffects a_script, int a_parameterEntity, bool isPercent)
    {
        GUILayout.BeginHorizontal();
        GUILayout.Label("Stat Value", GUILayout.Width(150));
        a_script.m_statusEffects[a_parameterEntity].valueToChange = EditorGUILayout.DoubleField(
    a_script.m_statusEffects[a_parameterEntity].valueToChange);
        if (isPercent)
            GUILayout.Label("%", GUILayout.Width(150));
        else
            GUILayout.Label("Units", GUILayout.Width(150));
        GUILayout.EndHorizontal();
    }

    private void OnEnable()
    {
        m_status = serializedObject.FindProperty("m_statusEffects");
    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        StatusEffects script = (StatusEffects)target;

        GUILayout.Space(20f);
        GUILayout.Label("Status Effects:", EditorStyles.boldLabel);
        GUILayout.Space(10f);
        for (int i = 0; i < m_status.arraySize; i++)
        {
            script.m_statusEffects[i].showItem = EditorGUILayout.Foldout(
                script.m_statusEffects[i].showItem, script.m_statusEffects[i].name, true);
            if (script.m_statusEffects[i].showItem)
            {
                #region Status
                GUILayout.Label("Status Description");
                GUILayout.BeginHorizontal();
                GUILayout.Label("Status Name", GUILayout.Width(150));
                script.m_statusEffects[i].name = GUILayout.TextField(script.m_statusEffects[i].name);
                GUILayout.EndHorizontal();
                //Asking the developer to put in a gameobject.

                GUILayout.BeginHorizontal();
                GUILayout.Label("Is it a Percentage or Whole Number", GUILayout.Width(250));
                GUILayout.EndHorizontal();

                //Button for converting to a percentage

                //Work on the enum values for the effectivenss as well as the percent to value.
                GUILayout.BeginHorizontal();
                if (GUILayout.Button(percentage, GUILayout.Width(250)))
                {
                    if (script.m_statusEffects[i].isAPercentage == true)
                    {
                        percentage = "Percentage";
                        script.m_statusEffects[i].isAPercentage = false;
                    }
                    else if (script.m_statusEffects[i].isAPercentage == false)
                    {
                        percentage = "Whole Number";
                        script.m_statusEffects[i].isAPercentage = true;
                    }
                }
                GUILayout.EndHorizontal();
                GUILayout.BeginHorizontal();
                if (script.m_statusEffects[i].isAPercentage)
                {
                    IntOrPercentageConvertPrim(script, i, script.m_statusEffects[i].isAPercentage);
                }
                else if (!script.m_statusEffects[i].isAPercentage)
                {
                    IntOrPercentageConvertPrim(script, i, script.m_statusEffects[i].isAPercentage);
                }
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Label("Item Properties", GUILayout.Width(150));
                for (int j = 0; j < script.m_statusEffects[i].effectiveness.Count; j++)
                {
                    script.m_statusEffects[i].effectiveness[j] = (HowItEffects)EditorGUILayout.EnumPopup(
                        script.m_statusEffects[i].effectiveness[j], GUILayout.Width(150));
                }
                GUILayout.EndHorizontal();
                GUILayout.Space(10f);
                GUILayout.BeginHorizontal();
                if (GUILayout.Button("Add More effectiveness"))
                {
                    if (script.m_statusEffects[i].effectiveness.Count < 6)
                        script.m_statusEffects[i].effectiveness.Add(new HowItEffects());
                }
                if (GUILayout.Button("Delete Recent Effectiveness"))
                {
                    if (script.m_statusEffects[i].effectiveness.Count > 1)
                        script.m_statusEffects[i].effectiveness.RemoveAt(script.m_statusEffects[i].effectiveness.Count - 1);
                }
                GUILayout.EndHorizontal();
                #endregion 
            }
        }
        GUILayout.Space(10f);
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Add More Status Effects"))
        {
            script.m_statusEffects.Add(new Status());
        }
        if (GUILayout.Button("Delete Recent Status Effect"))
        {
            if (script.m_statusEffects.Count > 1)
                script.m_statusEffects.RemoveAt(script.m_statusEffects.Count - 1);
        }
        GUILayout.EndHorizontal();
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
        m_types = serializedObject.FindProperty("m_types");
    }
    public override void OnInspectorGUI()
    {
        TypeChart script = (TypeChart)target;

        serializedObject.Update();
        EditorGUILayout.PropertyField(m_types, true);
        GUILayout.Space(10f);
        if (GUILayout.Button("Add More Types"))
        {
            script.AddType();
        }
        if (GUILayout.Button("Delete More Types"))
        {
            if (script.m_types.Count > 1)
                script.m_types.RemoveAt(script.m_types.Count - 1);
        }
        serializedObject.ApplyModifiedProperties();
    }
}
[CustomEditor(typeof(TypesEffected))]
public class CustomEditorTypesEffected : Editor
{
    TypesEffected script;
    private void OnEnable()
    {
        script = (TypesEffected)target;
    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        for (int i = 0; i < script.typeEffective.Count; i++)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("Attack");
            script.typeEffective[i].indexValueAtt = EditorGUILayout.Popup(
                script.typeEffective[i].indexValueAtt, script.typeEffective[i].nameAttack.ToArray());
            GUILayout.Label("Defense");
            script.typeEffective[i].indexValueDef = EditorGUILayout.Popup(
                script.typeEffective[i].indexValueDef, script.typeEffective[i].nameDefense.ToArray());
            GUILayout.EndHorizontal();
            GUILayout.Label("Effectiveness");
            script.typeEffective[i].effect =
                    (Effectiveness)EditorGUILayout.EnumPopup(script.typeEffective[i].effect, GUILayout.Width(150));
        }
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("More Matchups"))
        {
            script.AddNewTypeEffect();
        }
        if (GUILayout.Button("Delete Recent Matchup"))
        {
            if (script.typeEffective.Count > 1)
                script.typeEffective.RemoveAt(script.typeEffective.Count - 1);
        }
        GUILayout.EndHorizontal();
        serializedObject.ApplyModifiedProperties();
    }
}
[CustomEditor(typeof(Items))]
[CanEditMultipleObjects]
public class CustomEditorItems : Editor
{

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
        GUILayout.Label("Items", EditorStyles.boldLabel);
        GUILayout.Space(10f);


        for (int i = 0; i < m_items.arraySize; i++)
        {
            script.m_items[i].showItem = EditorGUILayout.Foldout(script.m_items[i].showItem, "Item " + script.m_items[i].name, true);
            if (script.m_items[i].showItem)
            {
                GUILayout.Label("Item Description");
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
                    if (lengthValueProperties < 4)
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

                for (int j = 0; j < script.m_items[i].variation.Count; j++)
                {
                    script.m_items[i].itemIndex =
                    EditorGUILayout.Popup(script.m_items[i].itemIndex,
                    script.m_items[i].variation[j].m_typeVariation.ToArray(), GUILayout.Width(150));
                }
                GUILayout.EndHorizontal();
                GUILayout.Space(10f);
                GUILayout.BeginHorizontal();
                if (GUILayout.Button("Add More Types"))
                {
                    script.m_items[i].variation.Add(new TypeVariation());
                }
                if (GUILayout.Button("Delete More Types"))
                {
                    if (script.m_items[i].variation.Count > 1)
                        script.m_items[i].variation.RemoveAt(script.m_items[i].variation.Count - 1);
                }
                GUILayout.EndHorizontal();
            }
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
