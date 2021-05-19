﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
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
                GUILayout.Label("Stat Type", GUILayout.Width(150));
                GUILayout.BeginHorizontal();
                script.m_primaryStatistic[i].typeOfStat = (TypeOfStat)EditorGUILayout.EnumPopup(
                    script.m_primaryStatistic[i].typeOfStat, GUILayout.Width(150));
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
                GUILayout.Label("Stat Type", GUILayout.Width(150));
                GUILayout.BeginHorizontal();
                script.m_secondaryStatistic[i].typeOfStat = (TypeOfStat)EditorGUILayout.EnumPopup(
                    script.m_secondaryStatistic[i].typeOfStat, GUILayout.Width(150));
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
        EditorUtility.SetDirty(target);
        serializedObject.ApplyModifiedProperties();
        #endregion

    }
}
[CustomEditor(typeof(StatsTypeAgainst))]
[CanEditMultipleObjects]
public class CustomEditorStatsType : Editor
{
    StatsTypeAgainst typeAgainst;

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
        typeAgainst = (StatsTypeAgainst)target;
        if (typeAgainst != null)
        {
            GUILayout.Label("Effectiveness of Stats");
            for (int i = 0; i < typeAgainst.stats.Count; i++)
            {
                typeAgainst.stats[i].showItem = EditorGUILayout.Foldout(typeAgainst.stats[i].showItem, "Stat", true);
                if (typeAgainst.stats[i].showItem)
                {
                    if (typeAgainst.stats[i].isOtherActive == false)
                    {
                        GUILayout.BeginHorizontal();
                        GUILayout.Label("Stats Impacting for:");
                        typeAgainst.stats[i].indexAttack = EditorGUILayout.Popup(
                            typeAgainst.stats[i].indexAttack, typeAgainst.stats[i].m_attack.ToArray());
                        GUILayout.Label("Stats Impacted against:");
                        typeAgainst.stats[i].indexDefense = EditorGUILayout.Popup(
                            typeAgainst.stats[i].indexDefense, typeAgainst.stats[i].m_defense.ToArray());
                        GUILayout.EndHorizontal();
                        GUILayout.BeginHorizontal();
                        if (GUILayout.Button("Attack and Defense", GUILayout.Width(250)))
                        {
                            typeAgainst.stats[i].isOtherActive = false;
                        }
                        if (GUILayout.Button("Other", GUILayout.Width(250)))
                        {
                            typeAgainst.stats[i].isOtherActive = true;
                        }
                        GUILayout.EndHorizontal();
                    }
                    else
                    {
                        GUILayout.BeginHorizontal();
                        GUILayout.Label("Stats Impacting for:");
                        typeAgainst.stats[i].indexOtherFor = EditorGUILayout.Popup(
                            typeAgainst.stats[i].indexOtherFor, typeAgainst.stats[i].m_other.ToArray());
                        GUILayout.Label("Stats Impacted against:");
                        typeAgainst.stats[i].indexOtherAgainst = EditorGUILayout.Popup(
                            typeAgainst.stats[i].indexOtherAgainst, typeAgainst.stats[i].m_other.ToArray());
                        GUILayout.EndHorizontal();
                        GUILayout.BeginHorizontal();
                        if (GUILayout.Button("Attack and Defense", GUILayout.Width(250)))
                        {
                            typeAgainst.stats[i].isOtherActive = false;
                        }
                        if (GUILayout.Button("Other", GUILayout.Width(250)))
                        {
                            typeAgainst.stats[i].isOtherActive = true;
                        }
                        GUILayout.EndHorizontal();
                    }
                }
            }
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Add More Effected Stats", GUILayout.Width(250)))
            {
                typeAgainst.AddStats();
            }
            if (GUILayout.Button("Delete Recent Effected Stats", GUILayout.Width(250)))
            {
                typeAgainst.stats.RemoveAt(typeAgainst.stats.Count - 1);
            }
            GUILayout.EndHorizontal();
            EditorUtility.SetDirty(target);
            serializedObject.ApplyModifiedProperties();
        }
    }
}
[CustomEditor(typeof(Entity))]
[CanEditMultipleObjects]
public class CustomEditorEntity : Editor
{
    private string primpercentage = "Percentage";
    private string secpercentage = "Percentage";
    Entity m_entity;
    private void OnEnable()
    {
        m_entity = (Entity)target;
        if (m_entity.m_nameOfItems.Count == 0)
        {
            foreach (var item in Items.item.m_items)
            {
                m_entity.m_nameOfItems.Add(item.name);
            }
        }
    }
    public void IntOrPercentageConvertPrim(int a_parameterStat, bool isPercent)
    {

        if (m_entity.m_primStat[a_parameterStat].diminishingReturns == true)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("Stat Value Min", GUILayout.Width(150));
            m_entity.m_primStat[a_parameterStat].min = EditorGUILayout.IntField(
            m_entity.m_primStat[a_parameterStat].min);
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.Label("Stat Value Current", GUILayout.Width(150));
            m_entity.m_primStat[a_parameterStat].stats = EditorGUILayout.DoubleField(
            m_entity.m_primStat[a_parameterStat].stats);
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.Label("Stat Value Max", GUILayout.Width(150));
            m_entity.m_primStat[a_parameterStat].max = EditorGUILayout.IntField(
           m_entity.m_primStat[a_parameterStat].max);
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            if (isPercent)
                GUILayout.Label("%", GUILayout.Width(150));
            else
                GUILayout.Label("Units", GUILayout.Width(150));
            GUILayout.EndHorizontal();

        }
        if (m_entity.m_primStat[a_parameterStat].diminishingReturns == false)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("Stat Value", GUILayout.Width(150));
            m_entity.m_primStat[a_parameterStat].stats = EditorGUILayout.DoubleField(
            m_entity.m_primStat[a_parameterStat].stats);
            if (isPercent)
                GUILayout.Label("%", GUILayout.Width(150));
            else
                GUILayout.Label("Units", GUILayout.Width(150));
            GUILayout.EndHorizontal();
        }

    }
    public void IntOrPercentageConvertSec(Entity a_script, int a_parameterEntity, bool isPercent)
    {
        GUILayout.BeginHorizontal();
        GUILayout.Label("Stat Value", GUILayout.Width(150));
        m_entity.m_secStat[a_parameterEntity].stats = EditorGUILayout.DoubleField(
             m_entity.m_secStat[a_parameterEntity].stats);
        if (isPercent)
            GUILayout.Label("%", GUILayout.Width(150));
        else
            GUILayout.Label("Units", GUILayout.Width(150));
        GUILayout.EndHorizontal();
    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        #region Main Stats
        m_entity.entityMainShow = EditorGUILayout.Foldout(m_entity.entityMainShow,
                 "Entity Main Stats", true);
        if (m_entity.entityMainShow)
        {
            //Per scriptableObject
            //When a new scriptable object occurs, the developer gets the options that are preset with the
            //types, stats, items and movesets.
            GUILayout.BeginHorizontal();
            GUILayout.Label("Name", GUILayout.Width(150));
            m_entity.m_name = GUILayout.TextField(m_entity.m_name, GUILayout.Width(250));
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.Label(m_entity.m_name + " description", GUILayout.Width(150));
            m_entity.m_descriptionEntity = GUILayout.TextField(m_entity.m_descriptionEntity);
            GUILayout.EndHorizontal();
            GUILayout.Space(10f);
            GUILayout.BeginHorizontal();
            GUILayout.Label("Health", GUILayout.Width(150));
            m_entity.m_health = EditorGUILayout.DoubleField(m_entity.m_health);
            GUILayout.EndHorizontal();
            GUILayout.Space(10f);
            GUILayout.BeginHorizontal();
            GUILayout.Label("Current Level", GUILayout.Width(150));
            m_entity.level = EditorGUILayout.IntField(m_entity.level);
            GUILayout.EndHorizontal();
            GUILayout.Space(10f);
            GUILayout.BeginHorizontal();
            GUILayout.Label("Max Level", GUILayout.Width(150));
            m_entity.maxLevel = EditorGUILayout.IntField(m_entity.maxLevel);
            GUILayout.EndHorizontal();
            GUILayout.Space(10f);
            GUILayout.BeginHorizontal();
            GUILayout.Label("Base Experience Yield", GUILayout.Width(150));
            m_entity.baseEXPYield = EditorGUILayout.FloatField(m_entity.baseEXPYield);
            GUILayout.EndHorizontal();
            GUILayout.Space(10f);
            GUILayout.BeginHorizontal();
            GUILayout.Label("Max Experience", GUILayout.Width(150));
            m_entity.maxEXP = EditorGUILayout.FloatField(m_entity.maxEXP);
            GUILayout.EndHorizontal();
        }
        #endregion
        #region Typing
        m_entity.entityTypesShow = EditorGUILayout.Foldout(m_entity.entityTypesShow,
                 "Entity Typing", true);
        if (m_entity.entityTypesShow)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("Typing");
            GUILayout.EndHorizontal();
            for (int i = 0; i < m_entity.m_typeEffectiveness.Count; i++)
            {
                if (m_entity.m_typeEffectiveness[i].m_types.Count == 0)
                {
                    m_entity.AddAllTypes(i);
                }
                m_entity.m_typeEffectiveness[i].typeIndex =
                    EditorGUILayout.Popup(m_entity.m_typeEffectiveness[i].typeIndex,
                m_entity.m_typeEffectiveness[i].m_types.ToArray(), GUILayout.Width(150));
            }
            GUILayout.Space(10f);
            GUILayout.BeginHorizontal();
            GUILayout.Label("Capped at 3");
            if (GUILayout.Button("Add More Types"))
            {
                ///Can be changed for later however we dont want to many typings on an entity
                if (m_entity.m_typeEffectiveness.Count < 3)
                    m_entity.AddNewType();
            }
            if (GUILayout.Button("Delete More Types"))
            {
                if (m_entity.m_typeEffectiveness.Count > 1)
                    m_entity.m_typeEffectiveness.RemoveAt(m_entity.m_typeEffectiveness.Count - 1);
            }
            GUILayout.EndHorizontal();
        }
        #endregion
        #region MoveSets
        ///Starting movesets for the entity
        m_entity.entityMoveSetShow = EditorGUILayout.Foldout(m_entity.entityMoveSetShow,
        "Entity MoveSet", true);
        if (m_entity.entityMoveSetShow)
        {
            if (m_entity.m_currentMoveSets.Count == 0)
            {
                m_entity.AddMoreCurrentMove();
            }
            GUILayout.BeginHorizontal();
            GUILayout.Label("Starting Movesets");
            GUILayout.EndHorizontal();
            for (int i = 0; i < m_entity.m_currentMoveSets.Count; i++)
            {
                if (m_entity.m_nameOfMovesCurrent.Count == 0)
                {
                    m_entity.AddMoveset();
                }
                m_entity.m_currentMoveSets[i].moveIndex =
                EditorGUILayout.Popup(m_entity.m_currentMoveSets[i].moveIndex,
                m_entity.m_nameOfMovesCurrent.ToArray(), GUILayout.Width(150));
                //This might be the issue in terms of the list of string names of moves.
            }
            GUILayout.Space(10f);
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Add More Starting Moves"))
            {
                m_entity.AddMoreCurrentMove();
            }
            if (GUILayout.Button("Delete Recent Move"))
            {
                if (m_entity.m_currentMoveSets.Count > 1)
                    m_entity.m_currentMoveSets.RemoveAt(m_entity.m_currentMoveSets.Count - 1);
            }
            GUILayout.EndHorizontal();

            if (m_entity.m_learnableMoves.Count == 0)
            {
                m_entity.AddNewLearnableMove();
            }
            GUILayout.BeginHorizontal();
            GUILayout.Label("Learnable Movesets");
            GUILayout.EndHorizontal();

            for (int i = 0; i < m_entity.m_learnableMoves.Count; i++)
            {
                if (m_entity.m_nameOfMovesExternal.Count == 0)
                {
                    m_entity.AddMoveset();
                }
                m_entity.m_learnableMoves[i].moveIndex =
                EditorGUILayout.Popup(m_entity.m_learnableMoves[i].moveIndex,
                m_entity.m_nameOfMovesExternal.ToArray(), GUILayout.Width(150));

                GUILayout.BeginHorizontal();

                if (GUILayout.Button("Learnable By Level"))
                {
                    m_entity.m_learntPerLevel[i] = true;
                }
                if (GUILayout.Button("Not Learnable By Level"))
                {
                    m_entity.m_learntPerLevel[i] = false;
                }
                GUILayout.EndHorizontal();
                if (m_entity.m_learntPerLevel[i] == true)
                {
                    GUILayout.BeginHorizontal();
                    GUILayout.Label("What Level does it learn the move?");
                    GUILayout.EndHorizontal();
                    m_entity.levelForeachMoveset[i] = EditorGUILayout.IntField(
                        m_entity.levelForeachMoveset[i]);
                }
                GUILayout.BeginHorizontal();
                if (GUILayout.Button("Learnt Externally"))
                {
                    m_entity.m_learntExternally[i] = true;
                }
                if (GUILayout.Button("Not learnt externally"))
                {
                    m_entity.m_learntExternally[i] = false;

                }
                GUILayout.EndHorizontal();
                if (m_entity.m_learntExternally[i] == true)
                {
                    GUILayout.BeginHorizontal();
                    GUILayout.Label("The move can now be learnt externally.");
                    GUILayout.EndHorizontal();
                }
                else
                {
                    GUILayout.BeginHorizontal();
                    GUILayout.Label("Cant be learnt externally!");
                    GUILayout.EndHorizontal();
                }
                //This might be the issue in terms of the list of string names of moves.
            }
            GUILayout.Space(10f);
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Add More Learnable Moves"))
            {
                m_entity.AddNewLearnableMove();
            }
            if (GUILayout.Button("Delete Recent Move"))
            {
                if (m_entity.m_learnableMoves.Count > 1 && m_entity.m_learntPerLevel.Count > 1
                    && m_entity.m_learntExternally.Count > 1)
                {
                    m_entity.m_learnableMoves.RemoveAt(m_entity.m_learnableMoves.Count - 1);
                    m_entity.m_learntPerLevel.RemoveAt(m_entity.m_learntPerLevel.Count - 1);
                    m_entity.m_learntExternally.RemoveAt(m_entity.m_learntExternally.Count - 1);
                }
            }
            GUILayout.EndHorizontal();
        }
        #endregion
        #region Items
        m_entity.entityItemShow = EditorGUILayout.Foldout(m_entity.entityItemShow,
        "Entity Items", true);
        if (m_entity.entityItemShow)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("Items");
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.Label("What Item is on the player?", GUILayout.Width(150));
            GUILayout.EndHorizontal();
            for (int i = 0; i < m_entity.m_itemsOnPlayer.Count; i++)
            {
                if (m_entity.m_itemsOnPlayer.Count >= 0 &&
                    (m_entity.m_nameOfItems.Count == 0 || m_entity.numOfItemsOnEntity.Count == 0))
                {
                    m_entity.AddItem(i);
                }
                GUILayout.BeginHorizontal();
                m_entity.itemIndex[i] =
                    EditorGUILayout.Popup(m_entity.itemIndex[i],
                m_entity.m_nameOfItems.ToArray(), GUILayout.Width(150));
                GUILayout.EndHorizontal();
                GUILayout.BeginHorizontal();
                GUILayout.Label("How many on the player?", GUILayout.Width(150));
                m_entity.numOfItemsOnEntity[i] = EditorGUILayout.IntField(m_entity.numOfItemsOnEntity[i]);
                GUILayout.EndHorizontal();
            }
            if (GUILayout.Button("Add More Items"))
            {
                m_entity.AddNewItem();
            }
            if (GUILayout.Button("Delete Recent Item"))
            {
                if (m_entity.m_itemsOnPlayer.Count > 0 && m_entity.numOfItemsOnEntity.Count > 0)
                {
                    m_entity.m_itemsOnPlayer.RemoveAt(m_entity.m_itemsOnPlayer.Count - 1);
                    m_entity.numOfItemsOnEntity.RemoveAt(m_entity.numOfItemsOnEntity.Count - 1);
                    m_entity.m_nameOfItems.RemoveAt(m_entity.m_nameOfItems.Count - 1);
                }
            }
        }
        #endregion
        #region PrimStat
        m_entity.entityPrimStatShow = EditorGUILayout.Foldout(m_entity.entityPrimStatShow,
        "Entity Primary Stats", true);
        if (m_entity.entityPrimStatShow)
        {
            if (m_entity.m_primStat.Count == 0)
            {
                m_entity.AddPrimStat();
            }
            for (int i = 0; i < m_entity.m_primStat.Count; i++)
            {
                GUILayout.Label(m_entity.m_primStat[i].name + "'s Primary Stats", EditorStyles.boldLabel);
                GUILayout.Space(10f);
                m_entity.m_primStat[i].showItem = EditorGUILayout.Foldout(m_entity.m_primStat[i].showItem,
                    m_entity.m_primStat[i].name, true);
                if (m_entity.m_primStat[i].showItem)
                {
                    GUILayout.Label("Stat Description");
                    GUILayout.Label(m_entity.m_primStat[i].name + " Stat", GUILayout.Width(150));
                    //Determining if its a percentage or integer it is effecting.
                    GUILayout.BeginHorizontal();
                    GUILayout.Label("Is it a Percentage or Whole Number", GUILayout.Width(250));
                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal();
                    //Button for converting to a percentage
                    if (GUILayout.Button(primpercentage, GUILayout.Width(250)))
                    {
                        if (m_entity.m_primStat[i].isItAPercent == true)
                        {
                            primpercentage = "Percentage";
                            m_entity.m_primStat[i].isItAPercent = false;
                        }
                        else if (m_entity.m_primStat[i].isItAPercent == false)
                        {
                            primpercentage = "Whole Number";
                            m_entity.m_primStat[i].isItAPercent = true;
                        }
                    }
                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal();
                    if (GUILayout.Button("Diminishing Returns"))
                    {
                        m_entity.m_primStat[i].diminishingReturns = true;
                    }
                    if (GUILayout.Button("No Diminishing Returns"))
                    {
                        m_entity.m_primStat[i].diminishingReturns = false;
                    }
                    GUILayout.EndHorizontal();

                    if (m_entity.m_primStat[i].isItAPercent)
                    {
                        IntOrPercentageConvertPrim(i, m_entity.m_primStat[i].isItAPercent);
                    }
                    else if (!m_entity.m_primStat[i].isItAPercent)
                    {
                        IntOrPercentageConvertPrim(i, m_entity.m_primStat[i].isItAPercent);
                    }
                }
            }
        }
        #endregion
        #region SecStat
        m_entity.entitySecStatShow = EditorGUILayout.Foldout(m_entity.entitySecStatShow,
        "Entity Secondary Stats", true);
        if (m_entity.entitySecStatShow)
        {
            if (m_entity.m_secStat.Count == 0)
            {
                m_entity.AddSecStat();
            }
            GUILayout.Label("Secondary Stats", EditorStyles.boldLabel);
            GUILayout.Space(10f);

            for (int i = 0; i < m_entity.m_secStat.Count; i++)
            {
                m_entity.m_secStat[i].showItem = EditorGUILayout.Foldout(m_entity.m_secStat[i].showItem,
                     m_entity.m_secStat[i].name, true);
                if (m_entity.m_secStat[i].showItem)
                {
                    GUILayout.Label("Stat Description");
                    GUILayout.Label(m_entity.m_secStat[i].name + " Stat", GUILayout.Width(150));
                    //Determining if its a percentage or integer it is effecting.
                    GUILayout.BeginHorizontal();
                    GUILayout.Label("Is it a Percentage or Whole Number", GUILayout.Width(250));
                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal();
                    //Button for converting to a percentage
                    if (GUILayout.Button(secpercentage, GUILayout.Width(250)))
                    {
                        if (m_entity.m_secStat[i].isItAPercent == true)
                        {
                            secpercentage = "Percentage";
                            m_entity.m_secStat[i].isItAPercent = false;
                        }
                        else if (m_entity.m_secStat[i].isItAPercent == false)
                        {
                            secpercentage = "Whole Number";
                            m_entity.m_secStat[i].isItAPercent = true;
                        }
                    }
                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal();
                    if (m_entity.m_secStat[i].isItAPercent)
                    {
                        IntOrPercentageConvertSec(m_entity, i, m_entity.m_secStat[i].isItAPercent);
                    }
                    else if (!m_entity.m_secStat[i].isItAPercent)
                    {
                        IntOrPercentageConvertSec(m_entity, i, m_entity.m_secStat[i].isItAPercent);
                    }
                    GUILayout.EndHorizontal();
                }
            }
        }
        #endregion
        #region EffectingStats
        m_entity.entityEffectStatShow = EditorGUILayout.Foldout(m_entity.entityEffectStatShow,
        "Entity Effectiveness Stats", true);
        if (m_entity.entityEffectStatShow)
        {
            GUILayout.Space(10f);
            GUILayout.BeginHorizontal();
            GUILayout.Label("Stat Effectiveness:");
            GUILayout.EndHorizontal();
            for (int i = 0; i < m_entity.m_statsEffecting.Count; i++)
            {
                m_entity.m_statsEffecting[i].showStat = EditorGUILayout.Foldout(m_entity.m_statsEffecting[i].showStat,
                     "Effecting Type " + i, true);
                if (m_entity.m_statsEffecting[i].showStat)
                {
                    GUILayout.BeginHorizontal();
                    GUILayout.Label("Effecting stat:");
                    m_entity.m_statsEffecting[i].indexValueEffecting =
                    EditorGUILayout.Popup(m_entity.m_statsEffecting[i].indexValueEffecting,
                    m_entity.m_statsEffecting[i].m_effectingStats.ToArray(), GUILayout.Width(150));
                    if (m_entity.m_statsEffecting[i].m_statChanges == StatChange.Additive)
                    {
                        GUILayout.Label("+");
                        m_entity.m_statsEffecting[i].operate = StatsEffected.add;
                    }
                    if (m_entity.m_statsEffecting[i].m_statChanges == StatChange.Subtraction)
                    {
                        GUILayout.Label("-");
                        m_entity.m_statsEffecting[i].operate = StatsEffected.sub;
                    }
                    if (m_entity.m_statsEffecting[i].m_statChanges == StatChange.Multiplicative)
                    {
                        GUILayout.Label("x");
                        m_entity.m_statsEffecting[i].operate = StatsEffected.mul;
                    }
                    if (m_entity.m_statsEffecting[i].m_statChanges == StatChange.Division)
                    {
                        GUILayout.Label("/");
                        m_entity.m_statsEffecting[i].operate = StatsEffected.div;
                    }
                    if (m_entity.m_statsEffecting[i].m_statChanges == StatChange.Average)
                    {
                        GUILayout.Label("+/2");
                        m_entity.m_statsEffecting[i].operate = StatsEffected.avg;
                    }
                    if (m_entity.m_statsEffecting[i].m_statChanges == StatChange.MultiplicativeAverage)
                    {
                        GUILayout.Label("*sqrt");
                        m_entity.m_statsEffecting[i].operate = StatsEffected.mulAvg;
                    }
                    GUILayout.Label("Effected stat:");
                    m_entity.m_statsEffecting[i].indexValueEffected =
                    EditorGUILayout.Popup(m_entity.m_statsEffecting[i].indexValueEffected,
                    m_entity.m_statsEffecting[i].m_statsEffected.ToArray(), GUILayout.Width(150));
                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal();
                    if (GUILayout.Button("+", GUILayout.Width(100)))
                    {
                        m_entity.m_statsEffecting[i].m_statChanges = StatChange.Additive;
                    }
                    if (GUILayout.Button("-", GUILayout.Width(100)))
                    {
                        m_entity.m_statsEffecting[i].m_statChanges = StatChange.Subtraction;
                    }
                    if (GUILayout.Button("x", GUILayout.Width(100)))
                    {
                        m_entity.m_statsEffecting[i].m_statChanges = StatChange.Multiplicative;
                    }
                    if (GUILayout.Button("/", GUILayout.Width(100)))
                    {
                        m_entity.m_statsEffecting[i].m_statChanges = StatChange.Division;
                    }
                    if (GUILayout.Button("Avg", GUILayout.Width(100)))
                    {
                        m_entity.m_statsEffecting[i].m_statChanges = StatChange.Average;
                    }
                    if (GUILayout.Button("Multi Avg", GUILayout.Width(100)))
                    {
                        m_entity.m_statsEffecting[i].m_statChanges = StatChange.MultiplicativeAverage;
                    }
                    GUILayout.EndHorizontal();
                }
            }
            if (GUILayout.Button("Add More Effectiveness"))
            {
                m_entity.AddNewStatChange();
            }
            if (GUILayout.Button("Delete Recent Effectiveness"))
            {
                if (m_entity.m_statsEffecting.Count > 0)
                    m_entity.m_statsEffecting.RemoveAt(m_entity.m_statsEffecting.Count - 1);
            }
        }
        #endregion
        #region Format
        m_entity.entitySpriteOrObj = EditorGUILayout.Foldout(m_entity.entitySpriteOrObj,
         "Entity Sprite/Object", true);
        if (m_entity.entitySpriteOrObj)
        {

            if (m_entity.is2DSprite == true)
            {
                EditorGUILayout.ObjectField(m_entity.sprite2D, typeof(Sprite), false);
            }
            if (m_entity.is2DSprite == false)
            {
                EditorGUILayout.ObjectField(m_entity.entityOBJ, typeof(GameObject), false);
            }
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("3D Object", GUILayout.Width(100)))
            {
                m_entity.is2DSprite = false;
            }
            if (GUILayout.Button("2D Sprite", GUILayout.Width(100)))
            {
                m_entity.is2DSprite = true;
            }
            GUILayout.EndHorizontal();
        }
        #endregion
        EditorUtility.SetDirty(target);
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
                GUILayout.BeginHorizontal();
                GUILayout.Label("Status Name", GUILayout.Width(150));
                script.m_statusEffects[i].name = GUILayout.TextField(script.m_statusEffects[i].name, GUILayout.Width(250));
                GUILayout.EndHorizontal();
                GUILayout.BeginHorizontal();
                GUILayout.Label("Status Description", GUILayout.Width(150));
                script.m_statusEffects[i].description = GUILayout.TextField(script.m_statusEffects[i].description);
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
                GUILayout.EndHorizontal();
                for (int j = 0; j < script.m_statusEffects[i].effectiveness.Count; j++)
                {
                    GUILayout.BeginHorizontal();
                    script.m_statusEffects[i].effectiveness[j] = (HowItEffects)EditorGUILayout.EnumPopup(
                        script.m_statusEffects[i].effectiveness[j], GUILayout.Width(150));
                    GUILayout.EndHorizontal();
                }
//                script.m_items[i].m_statIndex[j] =
//EditorGUILayout.Popup(script.m_items[i].m_statIndex[j],
//script.m_items[i].allStatsEffected[j].ToArray(), GUILayout.Width(150));
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
        EditorUtility.SetDirty(target);
        serializedObject.ApplyModifiedProperties();
    }
}

[CustomEditor(typeof(MoveSets))]
[CanEditMultipleObjects]
public class CustomEditorMoveSets : Editor
{
    SerializedProperty m_moveSets;
    private string percentage = "Percentage";
    public void IntOrPercentageConvert(MoveSets a_script, int a_parameterEntity, bool isPercent)
    {
        GUILayout.BeginHorizontal();
        GUILayout.Label("Moveset Power", GUILayout.Width(150));
        a_script.m_moveSets[a_parameterEntity].power = EditorGUILayout.DoubleField(
            a_script.m_moveSets[a_parameterEntity].power);
        if (isPercent)
            GUILayout.Label("%", GUILayout.Width(150));
        else
            GUILayout.Label("Units", GUILayout.Width(150));
        GUILayout.EndHorizontal();
    }

    private void OnEnable()
    {
        m_moveSets = serializedObject.FindProperty("m_moveSets");
    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        MoveSets script = (MoveSets)target;
        GUILayout.Label("Moves", EditorStyles.boldLabel);
        GUILayout.Space(10f);


        for (int i = 0; i < m_moveSets.arraySize; i++)
        {
            script.m_moveSets[i].showItem = EditorGUILayout.Foldout(script.m_moveSets[i].showItem,
                "Item " + script.m_moveSets[i].name, true);
            if (script.m_moveSets[i].showItem)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Label("Move Name", GUILayout.Width(150));
                script.m_moveSets[i].name = GUILayout.TextField(script.m_moveSets[i].name, GUILayout.Width(150));
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Label("Move Description", GUILayout.Width(150));
                script.m_moveSets[i].description = GUILayout.TextField(script.m_moveSets[i].description);
                GUILayout.EndHorizontal();
                GUILayout.BeginHorizontal();
                //Button for converting to a percentage
                if (GUILayout.Button(percentage, GUILayout.Width(250)))
                {
                    if (script.m_moveSets[i].isItAPercentage == true)
                    {
                        percentage = "Percentage";
                        script.m_moveSets[i].isItAPercentage = false;
                    }
                    else if (script.m_moveSets[i].isItAPercentage == false)
                    {
                        percentage = "Whole Number";
                        script.m_moveSets[i].isItAPercentage = true;
                    }
                }
                GUILayout.EndHorizontal();
                GUILayout.BeginHorizontal();
                if (script.m_moveSets[i].isItAPercentage)
                {
                    IntOrPercentageConvert(script, i, script.m_moveSets[i].isItAPercentage);
                }
                else if (!script.m_moveSets[i].isItAPercentage)
                {
                    IntOrPercentageConvert(script, i, script.m_moveSets[i].isItAPercentage);
                }
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Label("Accuracy", GUILayout.Width(150));
                script.m_moveSets[i].power = EditorGUILayout.IntSlider(script.m_moveSets[i].Accuracy, 1, 100);
                GUILayout.EndHorizontal();

                
                GUILayout.Label("Item Properties", GUILayout.Width(150));

                for (int j = 0; j < script.m_moveSets[i].type.Count; j++)
                {
                    GUILayout.BeginHorizontal();
                    script.m_moveSets[i].type[j] = (Type)EditorGUILayout.EnumPopup(
                        script.m_moveSets[i].type[j], GUILayout.Width(350));
                    if (script.m_moveSets[i].type[j] == Type.Attack)
                    {
                        script.m_moveSets[i].statsIndex = EditorGUILayout.Popup(script.m_moveSets[i].statsIndex,
                        script.m_moveSets[i].statsAttack.ToArray(), GUILayout.Width(150));
                    }
                    if (script.m_moveSets[i].type[j] == Type.Status)
                    {
                        script.m_moveSets[i].status[j].index = EditorGUILayout.Popup(script.m_moveSets[i].status[j].index,
                        script.m_moveSets[i].statusNames.ToArray(), GUILayout.Width(150));
                    }
                    GUILayout.EndHorizontal();
                }
                GUILayout.Space(10f);
                GUILayout.BeginHorizontal();
                if (GUILayout.Button("Add More effectiveness"))
                {
                    if (script.m_moveSets[i].type.Count < 3)
                    {
                        script.m_moveSets[i].type.Add(new Type());
                        script.AddNewStatus(i);
                    }

                }
                if (GUILayout.Button("Delete Recent Effectiveness"))
                {
                    if (script.m_moveSets[i].type.Count > 1)
                    {
                        script.m_moveSets[i].type.RemoveAt(script.m_moveSets[i].type.Count - 1);
                        script.m_moveSets[i].statusNames.RemoveAt(script.m_moveSets[i].statusNames.Count);
                        script.m_moveSets[i].status.RemoveAt(script.m_moveSets[i].status.Count - 1);
                    }
                }
                GUILayout.EndHorizontal();
                GUILayout.Space(10f);

                GUILayout.BeginHorizontal();
                GUILayout.Label("Typing");
                GUILayout.EndHorizontal();

                for (int j = 0; j < script.m_moveSets[i].m_typeEffectiveness.Count; j++)
                {

                    GUILayout.BeginHorizontal();
                    script.m_moveSets[i].m_typeEffectiveness[j].typeIndex =
                    EditorGUILayout.Popup(script.m_moveSets[i].m_typeEffectiveness[j].typeIndex,
                    script.m_moveSets[i].m_typeEffectiveness[j].m_types.ToArray(), GUILayout.Width(150));
                    GUILayout.EndHorizontal();
                }

                GUILayout.Space(10f);
                GUILayout.BeginHorizontal();
                if (GUILayout.Button("Add More Types"))
                {
                    if (script.m_moveSets[i].m_typeEffectiveness.Count < 3)
                        script.AddNewTyping(i);
                }
                if (GUILayout.Button("Delete More Types"))
                {
                    if (script.m_moveSets[i].m_typeEffectiveness.Count > 1)
                        script.m_moveSets[i].m_typeEffectiveness.RemoveAt(script.m_moveSets[i].m_typeEffectiveness.Count - 1);
                }
                GUILayout.EndHorizontal();
            }
        }
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Add More Moves"))
        {
            if (script.m_moveSets.Count < 3)
                script.m_moveSets.Add(new Moves());
        }
        if (GUILayout.Button("Delete More Moves"))
        {
            if (script.m_moveSets.Count > 1)
                script.m_moveSets.RemoveAt(script.m_moveSets.Count - 1);
        }
        GUILayout.EndHorizontal();
        EditorUtility.SetDirty(target);
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
        if (script.typeEffective != null)
        {
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
    private string percentage = "Percentage";
    private string durability = "Durability System";
    SerializedProperty m_items;
    private void OnEnable()
    {
        m_items = serializedObject.FindProperty("m_items");
    }

    public void IntOrPercentageConvert(Items a_script, int a_parameterEntity, bool isPercent)
    {
        GUILayout.BeginHorizontal();
        GUILayout.Label("Item Power", GUILayout.Width(150));
        a_script.m_items[a_parameterEntity].valueOfItem = EditorGUILayout.DoubleField(
            a_script.m_items[a_parameterEntity].valueOfItem);
        if (isPercent)
            GUILayout.Label("%", GUILayout.Width(150));
        else
            GUILayout.Label("Units", GUILayout.Width(150));
        GUILayout.EndHorizontal();
    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        Items script = (Items)target;
        GUILayout.Label("Items", EditorStyles.boldLabel);
        GUILayout.Space(10f);

        for (int i = 0; i < m_items.arraySize; i++)
        {
            script.m_items[i].showItem = EditorGUILayout.Foldout(script.m_items[i].showItem,
                "Item " + script.m_items[i].name, true);
            if (script.m_items[i].showItem)
            {

                GUILayout.Label("Item Description");
                if (!script.m_items[i].customizedItem)
                {
                    GUILayout.BeginHorizontal();
                    GUILayout.Label("Item Name", GUILayout.Width(150));
                    script.m_items[i].name = GUILayout.TextField(script.m_items[i].name, GUILayout.Width(250));
                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal();
                    GUILayout.Label("Item Description", GUILayout.Width(150));
                    script.m_items[i].description = GUILayout.TextField(script.m_items[i].description);
                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal();
                    if (GUILayout.Button("Active Item"))
                    {
                        script.m_items[i].staticItem = false;
                    }
                    if (GUILayout.Button("Static"))
                    {
                        script.m_items[i].staticItem = true;
                    }
                    GUILayout.EndHorizontal();
                    if (!script.m_items[i].staticItem)
                    {
                        GUILayout.BeginHorizontal();
                        if (GUILayout.Button("Potion"))
                        {
                            script.m_items[i].itemType = ItemType.Potion;
                        }
                        if (GUILayout.Button("Armour"))
                        {
                            script.m_items[i].itemType = ItemType.Armour;
                        }
                        if (GUILayout.Button("Weapon"))
                        {
                            script.m_items[i].itemType = ItemType.Weapon;
                        }
                        GUILayout.EndHorizontal();
                        //Percentage and Int modifier
                        GUILayout.BeginHorizontal();
                        //Button for converting to a percentage
                        if (GUILayout.Button(percentage, GUILayout.Width(250)))
                        {
                            if (script.m_items[i].isAPercentage == true)
                            {
                                percentage = "Percentage";
                                script.m_items[i].isAPercentage = false;
                            }
                            else if (script.m_items[i].isAPercentage == false)
                            {
                                percentage = "Whole Number";
                                script.m_items[i].isAPercentage = true;
                            }
                        }
                        GUILayout.EndHorizontal();
                        GUILayout.BeginHorizontal();
                        if (script.m_items[i].isAPercentage)
                        {
                            IntOrPercentageConvert(script, i, script.m_items[i].isAPercentage);
                        }
                        else if (!script.m_items[i].isAPercentage)
                        {
                            IntOrPercentageConvert(script, i, script.m_items[i].isAPercentage);
                        }
                        GUILayout.EndHorizontal();
                    }
                    //Durability
                    GUILayout.BeginHorizontal();
                    if (GUILayout.Button(durability, GUILayout.Width(250)))
                    {
                        if (script.m_items[i].isDurability == true)
                        {
                            script.m_items[i].isDurability = false;
                        }
                        else if (script.m_items[i].isDurability == false)
                        {

                            script.m_items[i].isDurability = true;
                        }
                    }
                    if (GUILayout.Button("Is Stackable"))
                    {
                        script.m_items[i].isStackable = true;
                    }
                    if (GUILayout.Button("Not Stackable"))
                    {
                        script.m_items[i].isStackable = false;
                    }
                    GUILayout.EndHorizontal();
                    if (script.m_items[i].isStackable)
                    {
                        GUILayout.Label("Item is stackable", GUILayout.Width(150));
                    }
                    else if (!script.m_items[i].isStackable)
                    {
                        GUILayout.Label("Item is not stackable", GUILayout.Width(150));
                    }

                    if (script.m_items[i].isDurability)
                    {
                        GUILayout.Label("This Item Has Durability", GUILayout.Width(150));
                        script.m_items[i].valueOfItem = EditorGUILayout.DoubleField(
                            script.m_items[i].durability);
                    }
                    else
                        GUILayout.Label("This Item does not have Durability", GUILayout.Width(250));
                    if (!script.m_items[i].staticItem)
                    {
                        GUILayout.BeginHorizontal();
                        GUILayout.Label("Item Properties", GUILayout.Width(150));
                        GUILayout.EndHorizontal();
                        for (int j = 0; j < script.m_items[i].properties.Count; j++)
                        {
                            if (script.m_items[i].properties.Count != script.m_items[i].allStatsEffected.Count)
                                script.AddNewStats(i);
                            if (script.m_items[i].allStatsEffected[j].Count <= 0)
                            {
                                script.AddStats(i);
                            }
                            GUILayout.BeginHorizontal();
                            script.m_items[i].properties[j] = (ItemProperties)EditorGUILayout.EnumPopup(script.m_items[i].properties[j], GUILayout.Width(150));
                            if (script.m_items[i].properties[j] == ItemProperties.IncreaseStats ||
                                script.m_items[i].properties[j] == ItemProperties.DecreaseStats)
                                script.m_items[i].m_statIndex[j] =
                            EditorGUILayout.Popup(script.m_items[i].m_statIndex[j],
                            script.m_items[i].allStatsEffected[j].ToArray(), GUILayout.Width(150));

                            if (script.m_items[i].properties[j] == ItemProperties.IncludeStatus)
                                script.m_items[i].status[j].index =
                            EditorGUILayout.Popup(script.m_items[i].status[j].index,
                            script.m_items[i].statusNames.ToArray(), GUILayout.Width(150));

                            GUILayout.EndHorizontal();
                        }
                        GUILayout.Space(10f);
                        GUILayout.BeginHorizontal();
                        if (GUILayout.Button("Add More effectiveness"))
                        {
                            if (script.m_items[i].properties.Count < 3)
                            {
                                script.AddNewStats(i);
                                script.m_items[i].properties.Add(new ItemProperties());
                                script.AddNewStatus(i);
                            }
                        }
                        if (GUILayout.Button("Delete Recent Effectiveness"))
                        {
                            if (script.m_items[i].properties.Count > 1)
                            {
                                script.m_items[i].properties.RemoveAt(script.m_items[i].properties.Count - 1);
                                script.m_items[i].allStatsEffected.RemoveAt(script.m_items[i].allStatsEffected.Count - 1);
                                script.m_items[i].statusNames.RemoveAt(script.m_items[i].statusNames.Count - 1);
                                script.m_items[i].status.RemoveAt(script.m_items[i].status.Count - 1);
                            }
                        }
                        GUILayout.EndHorizontal();
                        GUILayout.Space(10f);
                        GUILayout.Space(10f);
                        if (script.m_items[i].itemType != ItemType.Potion)
                        {
                            GUILayout.BeginHorizontal();
                            GUILayout.Label("Typing");
                            GUILayout.EndHorizontal();

                            for (int j = 0; j < script.m_items[i].variation.Count; j++)
                            {
                                script.m_items[i].variation[j].typeIndex =
                                    EditorGUILayout.Popup(script.m_items[i].variation[j].typeIndex,
                                script.m_items[i].variation[j].m_types.ToArray(), GUILayout.Width(150));
                            }
                            GUILayout.Space(10f);
                            GUILayout.BeginHorizontal();
                            if (script.m_items[i].itemType == ItemType.Armour)
                            {
                                if (GUILayout.Button("Add More Resistences"))
                                {
                                    script.AddNewTyping(i);
                                }
                                if (GUILayout.Button("Delete Recent Resistence"))
                                {
                                    if (script.m_items[i].variation.Count > 0)
                                        script.m_items[i].variation.RemoveAt(script.m_items[i].variation.Count - 1);
                                }
                            }
                            else if (script.m_items[i].itemType == ItemType.Weapon)
                            {
                                if (GUILayout.Button("Add More Weapon Types"))
                                {
                                    script.AddNewTyping(i);
                                }
                                if (GUILayout.Button("Delete Recent Weapon Types"))
                                {
                                    if (script.m_items[i].variation.Count > 0)
                                        script.m_items[i].variation.RemoveAt(script.m_items[i].variation.Count - 1);
                                }
                            }
                            GUILayout.EndHorizontal();
                        }
                    }
                }
                if (script.m_items[i].itemIsSprite == true)
                {
                    script.m_items[i].sprite = (Sprite)EditorGUILayout.ObjectField(script.m_items[i].sprite, typeof(Sprite), false);
                }
                if (script.m_items[i].itemIsSprite == false)
                {
                    script.m_items[i].obj = (GameObject)EditorGUILayout.ObjectField(script.m_items[i].obj, typeof(GameObject), false);
                }
                GUILayout.BeginHorizontal();
                if (GUILayout.Button("2D Sprite"))
                {
                    script.m_items[i].itemIsSprite = true;
                }
                if (GUILayout.Button("3D Sprite"))
                {
                    script.m_items[i].itemIsSprite = false;
                }
                GUILayout.EndHorizontal();
            }
        }
        GUILayout.Space(10f);
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Add More Items"))
        {
            script.AddNewItem();
        }
        if (GUILayout.Button("Delete More Items"))
        {
            if (script.m_items.Count > 1)
                script.m_items.RemoveAt(script.m_items.Count - 1);
        }
        GUILayout.EndHorizontal();
        EditorUtility.SetDirty(target);
        serializedObject.ApplyModifiedProperties();
    }
}
