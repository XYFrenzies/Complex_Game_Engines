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
    [HideInInspector] public int typeIndex;
    public string m_name;
    public float m_health;
    public int level;
    public int maxLevel;
    public float baseEXPYield;
    public float maxEXP;
    public List<TypeChart> m_typeEffectiveness;
    public List<PrimStatisic> m_primStat;
    public List<SecStatistic> m_secStat;
    //Need to display the amount of items for the player
    public List<ItemID> m_itemsOnPlayer;
    public List<int> numOfItemsOnEntity;
    public List<int> levelForeachMoveset;
    public List<MoveSets> m_currentMoveSets;
    public List<MoveSets> m_learnableMoves;
    public List<string> m_nameOfMovesCurrent;
    public List<string> m_nameOfMovesExternal;
    public List<bool> m_learntPerLevel;
    public List<bool> m_learntExternally;
    public int curMovIndex;
    public int learnMovIndex;
    public Entity()
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
        m_itemsOnPlayer = new List<ItemID>();
        numOfItemsOnEntity = new List<int>();
        levelForeachMoveset = new List<int>();
        m_name = "Name";
        m_health = 1;
        AddNewType();
        AddMoreCurrentMove();
        AddNewLearnableMove();
        foreach (var item in Stats.statsForObjects.m_primaryStatistic)
        {
            m_primStat.Add(item);
        }
        foreach (var item in Stats.statsForObjects.m_secondaryStatistic)
        {
            m_secStat.Add(item);
        }
        foreach (var item in Items.item.m_items)
        {
            m_itemsOnPlayer.Add(item);
            numOfItemsOnEntity.Add(item.m_amountOfItemsForPlayer);
        }
        foreach (var item in MoveSets.moves.m_moveSets)
        {
            m_nameOfMovesCurrent.Add(item.name);
            m_nameOfMovesExternal.Add(item.name);
            m_learntPerLevel.Add(item.isLearntPerLevel);
            m_learntExternally.Add(item.isLearntExternally);
        }
    }
    //To be used in the inspector
    public void AddNewType()
    {
        m_typeEffectiveness.Add(new TypeChart());
        for (int i = 0; i < m_typeEffectiveness.Count; i++)
        {
            m_typeEffectiveness[i].m_types = new List<string>();
            foreach (var item in TypeChart.chart.m_types)
            {
                m_typeEffectiveness[i].m_types.Add(item);
            }
        }
    }
    public void AddMoreCurrentMove()
    {
        m_currentMoveSets.Add(new MoveSets());
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
}
