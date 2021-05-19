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
    public List<StatusEffects> status;
    public int statsIndex = 0;
    public int itemIndex = 0;
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
    public List<TypeChart> m_typeEffectiveness;
    public bool isTypeChartNull;
    public bool isLearntPerLevel;
    public bool isLearntExternally;
    public int levelLearnt;
    public bool isStatNull;
    public bool isStatusNull = false;
    public List<string> statusNames;
    public Moves()
    {
        status = new List<StatusEffects>();
        isStatNull = false;
        statsAttack = new List<string>();
        statusNames = new List<string>();
        isLearntPerLevel = true;
        isLearntExternally = false;
        levelLearnt = 1;
        m_typeEffectiveness = new List<TypeChart>();
        m_typeEffectiveness.Add(new TypeChart());
        name = "None";
        description = "This is a moveset";
        type = new List<Type>();
        type.Add(Type.None);
        power = 1;
        isItAPercentage = false;
        Accuracy = 100;
        showItem = false;
        if (TypeChart.chart == null)
            isTypeChartNull = true;
        else
            for (int j = 0; j < m_typeEffectiveness.Count; j++)
            {
                m_typeEffectiveness[j].m_types = new List<string>();
                foreach (var item in TypeChart.chart.m_types)
                {
                    m_typeEffectiveness[j].m_types.Add(item);
                }
            }
        if (StatsTypeAgainst.instance == null)
            isStatNull = true;
        else
            statsAttack = StatsTypeAgainst.instance.stats[0].GetAttack();
        if (StatusEffects.status == null)
        {
            isStatusNull = true;
            return;
        }
        else
        {
            for (int j = 0; j < status.Count; j++)
            {
                status[j].m_statusEffects = new List<Status>();
                foreach (var item in StatusEffects.status.m_statusEffects)
                {
                    status[j].m_statusEffects.Add(item);
                    statusNames.Add(item.name);
                }
            }
        }
    }
    
}
[RequireComponent(typeof(StatusEffects))]
[RequireComponent(typeof(TypeChart))]
[ExecuteInEditMode]
public class MoveSets : MonoBehaviour
{
    public List<Moves> m_moveSets;
    public int moveIndex;
    public static MoveSets moves;
    private bool scriptHasBeenChangedType = false;
    private bool scriptHasBeenChangedStatus = false;
    private void OnValidate()
    {
        if (moves == null)
            moves = this;
        scriptHasBeenChangedType = true;
        scriptHasBeenChangedStatus = true;
    }
    void LateUpdate()
    {
        if (!Application.isPlaying && m_moveSets == null)
        {
            moveIndex = 0;
            m_moveSets = new List<Moves>();
            m_moveSets.Add(new Moves());
        }
        for (int i = 0; i < m_moveSets.Count; i++)
        {
            if (StatusEffects.status == null || m_moveSets[i].isStatusNull)
            {
                AddToStatus(i);
                m_moveSets[i].isStatusNull = true;
            }
            if (m_moveSets[i].status.Count <= 0)
                AddNewStatus(i);
            if (m_moveSets[i].status == null)
                m_moveSets[i].status = new List<StatusEffects>();

            if (StatsTypeAgainst.instance == null || m_moveSets[i].isStatNull)
            {
                StatsTypeAgainst.instance = GetComponent<StatsTypeAgainst>();
                m_moveSets[i].statsAttack = StatsTypeAgainst.instance.stats[0].GetAttack();
            }
            if (TypeChart.chart == null || m_moveSets[i].isTypeChartNull)
            {
                TypeChart.chart = GetComponent<TypeChart>();
                AddToTypes(i);
                return;
            }
            if (scriptHasBeenChangedType)
            {
                AddToTypes(i);
                scriptHasBeenChangedType = false;
                return;
            }
            if (scriptHasBeenChangedStatus)
            {
                AddToStatus(i);
                scriptHasBeenChangedStatus = false;
                return;
            }
            for (int j = 0; j < m_moveSets[i].m_typeEffectiveness.Count; j++)
            {
                if (m_moveSets[i].m_typeEffectiveness[j] == null)
                {
                    AddToTypes(i);
                }

                if (m_moveSets[i].m_typeEffectiveness[j].m_types.Count != TypeChart.chart.m_types.Count)
                {
                    NewList(i, j);
                }
                if (!m_moveSets[i].m_typeEffectiveness[j].m_types.SequenceEqual(TypeChart.chart.m_types))
                {
                    NewList(i, j);
                }
            }

        }
        if (m_moveSets.Count < 1)
        {
            m_moveSets = new List<Moves>();
            m_moveSets.Add(new Moves());
        }
    }
    private void AddToTypes(int i)
    {
        TypeChart.chart = GetComponent<TypeChart>();
        for (int j = 0; j < m_moveSets[i].m_typeEffectiveness.Count; j++)
        {
            if (m_moveSets[i].m_typeEffectiveness[j] == null)
                m_moveSets[i].m_typeEffectiveness[j] = new TypeChart();
            m_moveSets[i].m_typeEffectiveness[j].m_types = new List<string>();
            foreach (var item in TypeChart.chart.m_types)
            {
                m_moveSets[i].m_typeEffectiveness[j].m_types.Add(item);
            }
        }
    }
    private void NewList(int itemPara, int variationPara)
    {
        m_moveSets[itemPara].m_typeEffectiveness[variationPara].m_types = new List<string>();
        foreach (var item in TypeChart.chart.m_types)
        {
            m_moveSets[itemPara].m_typeEffectiveness[variationPara].m_types.Add(item);
        }
    }
    public void AddNewTyping(int i)
    {
        m_moveSets[i].m_typeEffectiveness.Add(new TypeChart());
        foreach (var item in TypeChart.chart.m_types)
        {
            for (int j = 0; j < m_moveSets[i].m_typeEffectiveness.Count; j++)
            {
                if (m_moveSets[i].m_typeEffectiveness[j].m_types == null)
                    m_moveSets[i].m_typeEffectiveness[j].m_types = new List<string>();
                if (m_moveSets[i].m_typeEffectiveness[j].m_types.Count < TypeChart.chart.m_types.Count)
                    m_moveSets[i].m_typeEffectiveness[j].m_types.Add(item);
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
        m_moveSets[i].status.Add(new StatusEffects());
        foreach (var item in StatusEffects.status.m_statusEffects)
        {
            for (int j = 0; j < m_moveSets[i].status.Count; j++)
            {
                m_moveSets[i].status[j].m_statusEffects = new List<Status>();
                m_moveSets[i].status[j].m_statusEffects.Add(item);
                m_moveSets[i].statusNames.Add(item.name);
            }
        }
    }
    public void AddToStatus(int i)
    {
        StatusEffects.status = GetComponent<StatusEffects>();
        for (int j = 0; j < m_moveSets[i].status.Count; j++)
        {
            if (m_moveSets[i].status[j] == null)
                m_moveSets[i].status[j] = new StatusEffects();
            m_moveSets[i].status[j].m_statusEffects = new List<Status>();
            foreach (var item in StatusEffects.status.m_statusEffects)
            {
                m_moveSets[i].status[j].m_statusEffects.Add(item);
                m_moveSets[i].statusNames.Add(item.name);
            }

        }
    }
}

