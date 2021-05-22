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
//This is the creation of a new status in the game.
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
    public List<int> m_statIndexPos;
    public List<int> m_statIndexNeg;
    //This is a default constructor for the status.
    public Status()
    {
        m_statIndexPos = new List<int>();
        m_statIndexNeg = new List<int>();
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
    public List<Status> m_statusEffects;
    public static StatusEffects status;
    public int index = 0;
    bool hasBeenChanged = false;
    private void OnValidate()
    {
        hasBeenChanged = true;
    }
    private void Update()
    {
        status = this;
        //Creates a new instance of the status.
        if (!Application.isPlaying && m_statusEffects == null)
        {
            m_statusEffects = new List<Status>();
            m_statusEffects.Add(new Status());
        }
        
        for (int i = 0; i < m_statusEffects.Count; i++)
        {
            //Checks if the stats are less than 0, if so, create a new stat.
            if (m_statusEffects[i].allStatsEffected.Count <= 0)
                AddNewStats(i);
            //If a change has occured in this script of that the stats index is 
            //different in length to the nested list of strings, readjust the list.
            if (hasBeenChanged == true ||
                m_statusEffects[i].allStatsEffected.Count != m_statusEffects[i].m_statIndexPos.Count)
            {
                ReadjustList(i);
            }
        }
    }
    //Adds stats to the nested list of strings.
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
    //Adds new stats to the nested list of all stats and index for the customeditor
    public void AddNewStats(int i)
    {
        m_statusEffects[i].allStatsEffected.Add(new List<string>());
        m_statusEffects[i].m_statIndexPos.Add(0);
        m_statusEffects[i].m_statIndexNeg.Add(0);
        for (int j = 0; j < m_statusEffects[i].allStatsEffected.Count; j++)
        {
            if (m_statusEffects[i].allStatsEffected[j].Count <= 0)
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
    //This is to check if the stats for the statuseffects is equal to the index of the stats.
    //When the while loop is finished, all the values that are less than or equall to 0 is given
    //a new primary and secondary stat.
    public void ReadjustList(int i) 
    {
        while(m_statusEffects[i].allStatsEffected.Count != m_statusEffects[i].m_statIndexPos.Count)
            m_statusEffects[i].allStatsEffected.Add(new List<string>());
        for (int j = 0; j < m_statusEffects[i].allStatsEffected.Count; j++)
        {
            if (m_statusEffects[i].allStatsEffected[j].Count <= 0)
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

}
