using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
[Serializable]
public enum Type
{
    None,
    Attack,
    //Defend,
    Status
}
[Serializable]
public class Moves
{
    public List<List<string>> statusNames;
    public List<int> statusIndex;
    public List<List<string>> m_typeEffectiveness;
    public List<int> typeIndex;
    public int statsIndex = 0;
    public int increaseStat = 0;
    public string name;
    public string description;
    public double power;
    public bool isItAPercentage;
    [Range(1, 100)]
    public int Accuracy;
    public List<Type> type;
    public List<string> statsAttack;
    public List<string> allStats;
    public bool showItem;
    public bool isLearntPerLevel;
    public bool isLearntExternally;
    public int levelLearnt;
    public Moves()
    {
        statusNames = new List<List<string>>();
        statsAttack = new List<string>();
        m_typeEffectiveness = new List<List<string>>();
        typeIndex = new List<int>();
        statusIndex = new List<int>();
        isLearntPerLevel = true;
        isLearntExternally = false;
        levelLearnt = 1;
        name = "None";
        description = "This is a moveset";
        type = new List<Type>();
        type.Add(Type.None);
        power = 1;
        isItAPercentage = false;
        Accuracy = 100;
        showItem = false;
    }

}
[RequireComponent(typeof(StatsTypeAgainst))]
[RequireComponent(typeof(StatusEffects))]
[RequireComponent(typeof(TypeChart))]
[ExecuteInEditMode]
public class MoveSets : MonoBehaviour
{
    public List<Moves> m_moveSets;
    public int moveIndex;
    public static MoveSets moves;
    private void OnValidate()
    {
        if (moves == null)
            moves = this;
    }
    void LateUpdate()
    {
        if (!Application.isPlaying)
        {
            if (m_moveSets == null)
            {
                moveIndex = 0;
                m_moveSets = new List<Moves>();
                m_moveSets.Add(new Moves());
            }
            for (int i = 0; i < m_moveSets.Count; i++)
            {
                if (m_moveSets[i].statusNames.Count <= 0)
                    AddNewStatus(i);
                if (m_moveSets[i].m_typeEffectiveness.Count <= 0)
                    AddNewTyping(i);
                if (StatsTypeAgainst.instance == null || m_moveSets[i].statsAttack.Count <= 0)
                {
                    StatsTypeAgainst.instance = GetComponent<StatsTypeAgainst>();
                    m_moveSets[i].statsAttack = StatsTypeAgainst.instance.stats[0].GetAttack();
                }
            }
        }
        if (m_moveSets.Count < 1)
        {
            m_moveSets = new List<Moves>();
            m_moveSets.Add(new Moves());
        }
    }
    public void AddNewTyping(int i)
    {
        m_moveSets[i].m_typeEffectiveness.Add(new List<string>());
        m_moveSets[i].typeIndex.Add(0);
        for (int j = 0; j < m_moveSets[i].m_typeEffectiveness.Count; j++)
        {
            foreach (var item in TypeChart.chart.m_types)
            {
                m_moveSets[i].m_typeEffectiveness[j].Add(item);
            }
        }
    }
    public Moves GetMoves(string name)
    {
        foreach (var item in m_moveSets)
        {
            if (item.name == name)
                return item;
        }
        return null;
    }
    public void AddNewStatus(int i)
    {
        m_moveSets[i].statusNames.Add(new List<string>());
        m_moveSets[i].statusIndex.Add(0);
        for (int j = 0; j < m_moveSets[i].statusNames.Count; j++)
        {
            foreach (var item in StatusEffects.status.m_statusEffects)
            {
                m_moveSets[i].statusNames[j].Add(item.name);
            }
        }
    }
}

