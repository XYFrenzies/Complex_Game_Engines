using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public enum StatChange
{
    Additive,
    Subtraction,
    Multiplicative,
    Division
}

[ExecuteInEditMode]
public class StatsEffected : MonoBehaviour
{
    [HideInInspector]public bool showStat = false;
    [HideInInspector]public static StatsEffected m_effectStats;
    [HideInInspector]public List<string> m_effectingStats;
    [HideInInspector]public List<string> m_statsEffected;
    [HideInInspector]public int indexValueEffecting;
    [HideInInspector]public int indexValueEffected;
    [HideInInspector]public StatChange m_statChanges;
    private void OnValidate()
    {
        m_effectStats = this;
    }
    // Update is called once per frame
    private void Update()
    {
        m_effectStats = this;
        if (!Application.isPlaying && m_effectingStats == null && m_statsEffected == null)
        {
            m_effectingStats = new List<string>();
            m_statsEffected = new List<string>();
            m_statChanges = new StatChange();
            m_statChanges = StatChange.Additive;
            indexValueEffecting = 0;
            indexValueEffected = 0;
        }
    }
    public void AddStatsPrim() 
    {
        foreach (var stat in Stats.statsForObjects.m_primaryStatistic)
        {
            m_effectingStats.Add(stat.name);
        }
    }
    public void AddStatsSec()
    {
        foreach (var stat in Stats.statsForObjects.m_secondaryStatistic)
        {
            m_effectingStats.Add(stat.name);
            m_statsEffected.Add(stat.name);
        }
    }
}
