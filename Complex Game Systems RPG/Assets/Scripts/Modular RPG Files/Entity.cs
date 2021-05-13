using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[RequireComponent(typeof(Items))]
[RequireComponent(typeof(MoveSets))]
[RequireComponent(typeof(TypeChart))]
[RequireComponent(typeof(Stats))]
[Serializable]
[CreateAssetMenu(fileName = "Entity", menuName = "Entities")]
public class Entity : ScriptableObject
{
    //DropDownOptions
    [HideInInspector] public bool entityMainShow;
    [HideInInspector] public bool entityTypesShow;
    [HideInInspector] public bool entityMoveSetShow;
    [HideInInspector] public bool entityItemShow;
    [HideInInspector] public bool entityPrimStatShow;
    [HideInInspector] public bool entitySecStatShow;
    [HideInInspector] public bool entityEffectStatShow;
    //MainStats
    public string m_name;
    public string m_descriptionEntity;
    public float m_health;
    public int level;
    public int maxLevel;
    public float baseEXPYield;
    public float maxEXP;
    //TypeEffectiveness
    public List<TypeChart> m_typeEffectiveness;
    //Stats
    public List<PrimStatisic> m_primStat;
    public List<SecStatistic> m_secStat;
    public List<StatsEffected> m_statsEffecting;
    //Items and the amount of them
    public List<Items> m_itemsOnPlayer;
    public List<int> numOfItemsOnEntity;
    public List<string> m_nameOfItems;
    //MoveSets of the entity
    public List<int> levelForeachMoveset;
    public List<MoveSets> m_currentMoveSets;
    public List<MoveSets> m_learnableMoves;
    public List<string> m_nameOfMovesCurrent;
    public List<string> m_nameOfMovesExternal;
    public List<bool> m_learntPerLevel;
    public List<bool> m_learntExternally;
    public void OnEnable()
    {
        level = 1;
        maxLevel = 2;
        maxEXP = 1;
        m_typeEffectiveness = new List<TypeChart>();
        m_currentMoveSets = new List<MoveSets>();
        m_learnableMoves = new List<MoveSets>();
        m_learntPerLevel = new List<bool>();
        m_learntExternally = new List<bool>();
        m_nameOfMovesCurrent = new List<string>();
        m_nameOfMovesExternal = new List<string>();
        m_primStat = new List<PrimStatisic>();
        m_secStat = new List<SecStatistic>();
        m_statsEffecting = new List<StatsEffected>();
        m_itemsOnPlayer = new List<Items>();
        m_nameOfItems = new List<string>();
        numOfItemsOnEntity = new List<int>();
        levelForeachMoveset = new List<int>();
        m_name = "Name";
        m_descriptionEntity = "This is the entity.";
        m_health = 1;
        entityMainShow = false;
        AddNewType();
        AddNewItem();
        AddMoreCurrentMove();
        AddNewLearnableMove();
        AddNewStatChange();

        AddPrimStat();
        AddSecStat();
        AddMoveset();
    }
    #region StatFunctions
    public void AddPrimStat()
    {
        foreach (var item in Stats.statsForObjects.m_primaryStatistic)
        {
            m_primStat.Add(item);
        }
    }
    public void AddSecStat()
    {
        foreach (var item in Stats.statsForObjects.m_secondaryStatistic)
        {
            m_secStat.Add(item);
        }
    }
    public void AddNewStatChange()
    {
        m_statsEffecting.Add(new StatsEffected());
        for (int i = 0; i < m_statsEffecting.Count; i++)
        {
            m_statsEffecting[i].m_effectingStats = new List<string>();
            m_statsEffecting[i].m_statsEffected = new List<string>();
            m_statsEffecting[i].m_statChanges = new StatChange();
            foreach (var stat in Stats.statsForObjects.m_primaryStatistic)
            {
                m_statsEffecting[i].m_effectingStats.Add(stat.name);
            }
            foreach (var stat in Stats.statsForObjects.m_secondaryStatistic)
            {
                m_statsEffecting[i].m_statsEffected.Add(stat.name);
                m_statsEffecting[i].m_effectingStats.Add(stat.name);
            }
            m_statsEffecting[i].m_statChanges = StatChange.Additive;
        }
    }
    #endregion
    #region ItemFunctions
    public void AddItem(int i)
    {
        foreach (var item in Items.item.m_items)
        {
            m_itemsOnPlayer[i].m_items.Add(item);
            m_nameOfItems.Add(item.name);
            numOfItemsOnEntity.Add(item.m_amountOfItemsForPlayer);
        }
    }
    public void AddNewItem()
    {
        m_itemsOnPlayer.Add(new Items());
        for (int i = 0; i < m_itemsOnPlayer.Count; i++)
        {
            m_itemsOnPlayer[i].m_items = new List<ItemID>();
            if (Items.item == null) 
            {
                return;
            }
            AddItem(i);
        }
    }
    #endregion
    #region TypeFunctions
    public void AddAllTypes(int i)
    {
        if (TypeChart.chart == null)
            return;
        foreach (var item in TypeChart.chart.m_types)
        {
            m_typeEffectiveness[i].m_types.Add(item);
        }
    }
    //To be used in the inspector
    public void AddNewType()
    {
        m_typeEffectiveness.Add(new TypeChart());
        for (int i = 0; i < m_typeEffectiveness.Count; i++)
        {
            m_typeEffectiveness[i].m_types = new List<string>();
            AddAllTypes(i);
        }
    }
    #endregion
    #region MovesetFunctions
    public void AddMoveset()
    {
        if (MoveSets.moves == null)
            return;
        foreach (var item in MoveSets.moves.m_moveSets)
        {
            m_nameOfMovesCurrent.Add(item.name);
            m_nameOfMovesExternal.Add(item.name);
            m_learntPerLevel.Add(item.isLearntPerLevel);
            m_learntExternally.Add(item.isLearntExternally);
            levelForeachMoveset.Add(item.levelLearnt);
        }
    }
    public void AddMoreCurrentMove()
    {
        m_currentMoveSets.Add(new MoveSets());
        if (MoveSets.moves == null)
            return;
        for (int i = 0; i < m_currentMoveSets.Count; i++)
        {
            m_currentMoveSets[i].m_moveSets = new List<Moves>();
            foreach (var item in MoveSets.moves.m_moveSets)
            {
                m_currentMoveSets[i].m_moveSets.Add(item);
            }
        }
    }
    public void AddNewLearnableMove()
    {

        m_learnableMoves.Add(new MoveSets());
        if (MoveSets.moves == null)
            return;
        for (int i = 0; i < m_learnableMoves.Count; i++)
        {
            m_learnableMoves[i].m_moveSets = new List<Moves>();
            foreach (var item in MoveSets.moves.m_moveSets)
            {
                m_learnableMoves[i].m_moveSets.Add(item);
                m_learntPerLevel.Add(item.isLearntPerLevel);
                m_learntExternally.Add(item.isLearntExternally);
            }
        }
    }
    #endregion
}
