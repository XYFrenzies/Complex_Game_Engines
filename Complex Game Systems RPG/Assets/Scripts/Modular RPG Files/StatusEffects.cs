using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//Healing, damage, increased stat, decrease stat (integer or percentage.), cant heal, immune
[Serializable]
public enum HowItEffects
{
    Healing,
    Damage,
    increaseStat,
    decreaseStat,
    healBlock,
    immunityToDamage,
    immunityToStatus
}
[Serializable]
public class Status
{
    public int index = 0;
    public string name;
    public string description;
    public double valueToChange; //Can be a int or a percent
    public List<HowItEffects> effectiveness;
    public bool isAPercentage;
    [HideInInspector] public bool showItem;
    public List<List<string>> allStatsEffected;
    public List<int> m_statIndex;
    public Status()
    {
        m_statIndex = new List<int>();
        allStatsEffected = new List<List<string>>();
        name = "Toxic";
        description = "This is a status Condition.";
        valueToChange = 1;
        effectiveness = new List<HowItEffects>();
        effectiveness.Add(HowItEffects.Damage);
        isAPercentage = false;
        showItem = false;
    }
}
[ExecuteInEditMode]
public class StatusEffects : MonoBehaviour
{
    [Tooltip("Can be on its own.")]
    //Make functions that interact with the status.
    public List<Status> m_statusEffects;
    public static StatusEffects status;
    public int index = 0;
    private void Update()
    {
        status = this;
        if (!Application.isPlaying && m_statusEffects == null)
        {
            m_statusEffects = new List<Status>();
            m_statusEffects.Add(new Status());
            for (int i = 0; i < m_statusEffects.Count; i++)
            {
                if (m_statusEffects[i].allStatsEffected.Count <= 0)
                    AddNewStats(i);
            }
        }
        if (m_statusEffects.Count < 1)
        {
            m_statusEffects = new List<Status>();
            m_statusEffects.Add(new Status());
        }
    }
    public void AddStats(int i)
    {
        for (int j = 0; j < m_statusEffects[i].allStatsEffected.Count; j++)
        {
            foreach (var item in Stats.statsForObjects.m_primaryStatistic)
            {
                m_statusEffects[i].allStatsEffected[j].Add(item.name);
            }
            foreach (var item in Stats.statsForObjects.m_secondaryStatistic)
            {
                m_statusEffects[i].allStatsEffected[j].Add(item.name);
            }
        }
    }
    public void AddNewStats(int i)
    {
        m_statusEffects[i].allStatsEffected.Add(new List<string>());
        m_statusEffects[i].m_statIndex.Add(0);
        for (int j = 0; j < m_statusEffects[i].allStatsEffected.Count; j++)
        {
            foreach (var item in Stats.statsForObjects.m_primaryStatistic)
            {
                m_statusEffects[i].allStatsEffected[j].Add(item.name);

            }
            foreach (var item in Stats.statsForObjects.m_secondaryStatistic)
            {
                m_statusEffects[i].allStatsEffected[j].Add(item.name);
            }
        }
    }

}
