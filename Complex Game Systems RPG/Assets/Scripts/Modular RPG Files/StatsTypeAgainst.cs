using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Stats))]
public class StatsTypeAgainst : MonoBehaviour
{
    public List<List<string>> m_attack;
    public List<List<string>> m_defense;
    public List<List<string>> m_other;
    public List<int> m_index;
    private Stats stat;
    void Update()
    {
        if (stat == null)
            stat = GetComponent<Stats>();
        if (!Application.isPlaying && m_defense == null && 
            m_other == null && m_attack == null)
        {
            m_index = new List<int>();
            m_attack = new List<List<string>>();
            m_defense = new List<List<string>>();
            m_other = new List<List<string>>();
            AddNewStats(m_attack, TypeOfStat.Attack);
            AddNewStats(m_defense, TypeOfStat.Defense);
            AddNewStats(m_other, TypeOfStat.Other);
        }
    }
    public void AddNewStats(List<List<string>> type, TypeOfStat typeofStat)
    {
        type.Add(new List<string>());
        m_index.Add(0);
        for (int j = 0; j < type.Count; j++)
        {
            foreach (var item in Stats.statsForObjects.m_primaryStatistic)
            {
                if (item.typeOfStat == typeofStat)
                    type[j].Add(item.name);

            }
            foreach (var item in Stats.statsForObjects.m_secondaryStatistic)
            {
                if (item.typeOfStat == typeofStat)
                    type[j].Add(item.name);
            }
        }
    }
    public void AddStats(List<List<string>> type, TypeOfStat typeofStat)
    {
        for (int j = 0; j < type.Count; j++)
        {
            foreach (var item in Stats.statsForObjects.m_primaryStatistic)
            {
                if(item.typeOfStat == typeofStat)
                    type[j].Add(item.name);
            }
            foreach (var item in Stats.statsForObjects.m_secondaryStatistic)
            {
                if (item.typeOfStat == typeofStat)
                    type[j].Add(item.name);
            }
        }
    }

}
