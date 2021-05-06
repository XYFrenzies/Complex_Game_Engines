using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[RequireComponent(typeof(TypeChart))]
[RequireComponent(typeof(Stats))]
[Serializable]
public class Entity : ScriptableObject
{
    public string m_name;
    public float m_health;
    public int level;
    public int maxLevel;
    public float maxEXP;
    public List<TypeChart> m_typeEffectiveness;
    public List<PrimStatisic> m_primStat;
    public List<SecStatistic> m_secStat;
    public Entity()
    {
        level = 1;
        maxLevel = 2;
        maxEXP = 1;
        m_typeEffectiveness = new List<TypeChart>();
        m_primStat = new List<PrimStatisic>();
        m_secStat = new List<SecStatistic>();
        m_name = "Name";
        m_health = 1;
        for (int j = 0; j < m_typeEffectiveness.Count; j++)
        {
            m_typeEffectiveness[j].m_types = new List<string>();
            foreach (var item in TypeChart.chart.m_types)
            {
                m_typeEffectiveness[j].m_types.Add(item);
            }
        }
        foreach (var item in Stats.statsForObjects.m_primaryStatistic)
        {
            m_primStat.Add(item);
        }
        foreach (var item in Stats.statsForObjects.m_secondaryStatistic)
        {
            m_secStat.Add(item);
        }

    }
}
