using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Entity
{
    public string m_name;
    public int amountOfEntities;
    public GameObject m_object;
    public float m_health;
    public List<PrimStatisic> m_primaryStats;
    public List<SecStatistic> m_secondaryStats;
    public Entity() 
    {
        m_name = "";
        amountOfEntities = 1;
        m_object = null;
        m_health = 0;
    }
}

[RequireComponent(typeof(Stats))]
[ExecuteInEditMode]
public class Entities : Stats
{
    [Tooltip("NEEDS A OBJECT FOR THIS TO WORK")]
    public List<Entity> entities;
    private Stats stat;
    private void Awake()
    {
        stat = GetComponent<Stats>();
    }
    private void LateUpdate()
    {
        if (!Application.isPlaying && entities == null)
        {
            entities = new List<Entity>();
            entities.Add(new Entity());
            for (int i = 0; i < entities.Count; i++)
            {
                AddNewStatisticsPrimary(i);
                AddNewStatisticsSecondary(i);
            }
        }
        else if (entities.Count >= 1)
        {
            if (stat.m_primaryStatistic.Count < 1 || stat.m_secondaryStatistic.Count < 1)
            {
                for (int i = 0; i < statsForObjects.m_primaryStatistic.Count; i++)
                {
                    entities[i] = new Entity();
                    CreateNew(i);
                    CopyTo(i);
                }
            }
            for (int i = 0; i < entities.Count; i++)
            {
                if (stat.m_primaryStatistic.Count != entities[i].m_primaryStats.Count)
                    AddNewStatisticsPrimary(i);

                if(stat.m_secondaryStatistic.Count != entities[i].m_secondaryStats.Count)
                    AddNewStatisticsSecondary(i);

                for (int j = 0; j < stat.m_primaryStatistic.Count; j++)
                {
                    if (entities[i].m_primaryStats[j] != stat.m_primaryStatistic[j])
                        AddNewStatisticsPrimary(i);
                }
                for (int j = 0; j < stat.m_secondaryStatistic.Count; j++)
                {
                    if (entities[i].m_secondaryStats[j] != stat.m_secondaryStatistic[j])
                        AddNewStatisticsSecondary(i);
                }
            }
        }
        else
        {
            if (entities.Count < 1)
            {
                entities = new List<Entity>();
                entities.Add(new Entity());
                for (int i = 0; i < entities.Count; i++)
                {
                    CreateNew(i);
                    CopyTo(i);
                }
            }
        }
    }

    void CopyTo(int parameter) 
    {
        entities[parameter].m_primaryStats.CopyTo(GetComponent<Stats>().m_primaryStatistic.ToArray(), 0);
        entities[parameter].m_secondaryStats.CopyTo(GetComponent<Stats>().m_secondaryStatistic.ToArray(), 0);
    }
    void CreateNew(int parameter)
    {
        entities[parameter].m_primaryStats = GetComponent<Stats>().m_primaryStatistic;
        entities[parameter].m_secondaryStats = GetComponent<Stats>().m_secondaryStatistic;
    }
    void AddNewStatisticsPrimary(int parameter) 
    {
        entities[parameter].m_primaryStats = GetComponent<Stats>().m_primaryStatistic;
        entities[parameter].m_primaryStats.CopyTo(GetComponent<Stats>().m_primaryStatistic.ToArray(), 0);
    }
    void AddNewStatisticsSecondary(int parameter)
    {
        entities[parameter].m_secondaryStats = GetComponent<Stats>().m_secondaryStatistic;
        entities[parameter].m_secondaryStats.CopyTo(GetComponent<Stats>().m_secondaryStatistic.ToArray(), 0);
    }
}
