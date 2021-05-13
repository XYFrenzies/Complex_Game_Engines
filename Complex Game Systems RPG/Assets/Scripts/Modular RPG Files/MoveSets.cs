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
    Defend,
    Status
}
[Serializable]
public class Moves
{
    public int itemIndex = 0;
    public string name;
    public string description;
    public double power;
    public bool isItAPercentage;
    [Range(1, 100)]
    public int Accuracy;
    public List<Type> type;
    public bool showItem;
    public List<TypeChart> m_typeEffectiveness;
    public bool isTypeChartNull;
    public bool isLearntPerLevel;
    public bool isLearntExternally;
    public int levelLearnt;
    public Moves()
    {
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
    }
}
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
        if (!Application.isPlaying && m_moveSets == null)
        {
            moveIndex = 0;
            m_moveSets = new List<Moves>();
            m_moveSets.Add(new Moves());
        }
        for (int i = 0; i < m_moveSets.Count; i++)
        {
            if (TypeChart.chart == null || m_moveSets[i].isTypeChartNull)
            {
                TypeChart.chart = GetComponent<TypeChart>();
                AddToTypes(i);
                return;
            }
            for (int j = 0; j < m_moveSets[i].m_typeEffectiveness.Count; j++)
            {
                if (m_moveSets[i].m_typeEffectiveness[j] == null)
                {
                    AddToTypes(i);
                    return;
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
}

