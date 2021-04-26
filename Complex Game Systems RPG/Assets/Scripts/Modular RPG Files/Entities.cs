using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Entity
{
    public string m_name = "";
    public int amountOfEntities = 1;
    public GameObject m_object;
    public float m_health = 0;
    public PrimStatisic[] m_primaryStats;
    public SecStatistic[] m_secondaryStats;
}

[RequireComponent(typeof(Stats))]
[ExecuteInEditMode]
public class Entities : Stats
{
    [Tooltip("NEEDS A OBJECT FOR THIS TO WORK")]
    public Entity[] entities;
    private void LateUpdate()
    {
        if (!Application.isPlaying && entities == null)
        {
            entities = new Entity[2];
            for (int i = 0; i < entities.Length; i++)
            {
                entities[i] = new Entity();
                AddNewStatisticsPrimary(i);
                AddNewStatisticsSecondary(i);
            }
        }
        else if (entities.Length >= 1)
        {
            if (statsForObjects.m_primaryStatistic.Length < 1 || statsForObjects.m_secondaryStatistic.Length < 1)
            {
                for (int i = 0; i < statsForObjects.m_primaryStatistic.Length; i++)
                {
                    entities[i] = new Entity();
                    CreateNew(i);
                    CopyTo(i);
                }
            }
            for (int i = 0; i < entities.Length; i++)
            {
                if (statsForObjects.m_primaryStatistic.Length != entities[i].m_primaryStats.Length)
                    AddNewStatisticsPrimary(i);

                if(statsForObjects.m_secondaryStatistic.Length != entities[i].m_secondaryStats.Length)
                    AddNewStatisticsSecondary(i);
            }
        }
        else
        {
            if (entities.Length < 1)
            {
                entities = new Entity[1];
                for (int i = 0; i < entities.Length; i++)
                {
                    entities[i] = new Entity();
                    CreateNew(i);
                    CopyTo(i);
                }
            }
        }
    }

    void CopyTo(int parameter) 
    {
        entities[parameter].m_primaryStats.CopyTo(statsForObjects.m_primaryStatistic, 0);
        entities[parameter].m_secondaryStats.CopyTo(statsForObjects.m_secondaryStatistic, 0);
    }
    void CreateNew(int parameter)
    {
        entities[parameter].m_primaryStats = statsForObjects.m_primaryStatistic;
        entities[parameter].m_secondaryStats = statsForObjects.m_secondaryStatistic;
    }
    void AddNewStatisticsPrimary(int parameter) 
    {
        entities[parameter].m_primaryStats = statsForObjects.m_primaryStatistic;
        entities[parameter].m_primaryStats.CopyTo(statsForObjects.m_primaryStatistic, 0);
    }
    void AddNewStatisticsSecondary(int parameter)
    {
        entities[parameter].m_secondaryStats = statsForObjects.m_secondaryStatistic;
        entities[parameter].m_secondaryStats.CopyTo(statsForObjects.m_secondaryStatistic, 0);
    }
}
