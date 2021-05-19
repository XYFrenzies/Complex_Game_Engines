using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StatsForAndAgainst
{
    public int indexAttack;
    public int indexDefense;
    public int indexOtherFor;
    public int indexOtherAgainst;
    public bool showItem = false;
    public List<string> m_attack;
    public List<string> m_defense;
    public List<string> m_other;
    public bool isStatNull;
    public bool isOtherActive;
    public StatsForAndAgainst()
    {
        isOtherActive = false;
        indexAttack = 0;
        indexDefense = 0;
        indexOtherFor = 0;
        indexOtherAgainst = 0;
        isStatNull = true;
        m_attack = new List<string>();
        m_defense = new List<string>();
        m_other = new List<string>();
        if (Stats.statsForObjects == null)
            isStatNull = true;
        else
        {
            AddNewStats(m_attack, TypeOfStat.Attack);
            AddNewStats(m_defense, TypeOfStat.Defense);
            AddNewStats(m_other, TypeOfStat.Other);
        }
    }
    public void AddNewStats(List<string> type, TypeOfStat typeofStat)
    {
        foreach (var item in Stats.statsForObjects.m_primaryStatistic)
        {
            if (item.typeOfStat == typeofStat)
                type.Add(item.name);
        }
        foreach (var item in Stats.statsForObjects.m_secondaryStatistic)
        {
            if (item.typeOfStat == typeofStat)
                type.Add(item.name);
        }
    }
    public List<string> GetAttack() 
    {
        return m_attack;
    }
}

[ExecuteInEditMode]
[RequireComponent(typeof(Stats))]
public class StatsTypeAgainst : MonoBehaviour
{
    public List<StatsForAndAgainst> stats;
    private Stats stat;
    public static StatsTypeAgainst instance;
    void Update()
    {
        instance = this;
        if (stat == null)
            stat = GetComponent<Stats>();
        if (!Application.isPlaying)
        {
            if (stats == null)
            {
                stats = new List<StatsForAndAgainst>();

                    stats.Add(new StatsForAndAgainst());
            }
            for (int i = 0; i < stats.Count; i++)
            {
                if (stats[i].isStatNull == true)
                {
                    AddNewStats(stats[i].m_attack, TypeOfStat.Attack);
                    AddNewStats(stats[i].m_defense, TypeOfStat.Defense);
                    AddNewStats(stats[i].m_other, TypeOfStat.Other);
                }
            }
        }
    }
    public void AddNewStats(List<string> type, TypeOfStat typeofStat)
    {
        foreach (var item in Stats.statsForObjects.m_primaryStatistic)
        {
            if (item.typeOfStat == typeofStat)
                type.Add(item.name);

        }
        foreach (var item in Stats.statsForObjects.m_secondaryStatistic)
        {
            if (item.typeOfStat == typeofStat)
                type.Add(item.name);
        }
    }
    public void AddStats()
    {
        stats.Add(new StatsForAndAgainst());
        for (int j = 0; j < stats.Count; j++)
        {
            foreach (var item in Stats.statsForObjects.m_primaryStatistic)
            {
                if (item.typeOfStat == TypeOfStat.Attack)
                    stats[j].m_attack.Add(item.name);
                else if(item.typeOfStat == TypeOfStat.Defense)
                    stats[j].m_defense.Add(item.name);
                else if(item.typeOfStat == TypeOfStat.Other)
                    stats[j].m_other.Add(item.name);
            }
            foreach (var item in Stats.statsForObjects.m_secondaryStatistic)
            {
                if (item.typeOfStat == TypeOfStat.Attack)
                    stats[j].m_attack.Add(item.name);
                else if (item.typeOfStat == TypeOfStat.Defense)
                    stats[j].m_defense.Add(item.name);
                else if (item.typeOfStat == TypeOfStat.Other)
                    stats[j].m_other.Add(item.name);
            }
        }
    }

}
