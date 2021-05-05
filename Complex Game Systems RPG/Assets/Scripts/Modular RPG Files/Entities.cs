using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Entity
{
    public int indexEntity;
    public string m_name;
    public float m_health;
    public int level;
    public int maxLevel;
    public float maxEXP;
    public Stats m_stats;
    public List<TypeVariation> m_typeEffectiveness;
    public List<PrimStatisic> m_primaryStats;
    public List<SecStatistic> m_secondaryStats;
    [HideInInspector] public bool showItem;
    public Entity()
    {
        level = 1;
        maxLevel = 2;
        maxEXP = 1;
        m_typeEffectiveness = new List<TypeVariation>();
        m_primaryStats = new List<PrimStatisic>();
        m_secondaryStats = new List<SecStatistic>();
        m_name = "Name";
        m_health = 1;
        showItem = false;
        indexEntity = 0;
    }
}

[RequireComponent(typeof(TypeChart))]
[RequireComponent(typeof(Stats))]
[ExecuteInEditMode]
public class Entities : Stats
{
    [Tooltip("NEEDS A OBJECT FOR THIS TO WORK")]
    public List<Entity> entities;
    private Stats stat;
    private void LateUpdate()
    {
        if (stat == null)
            stat = GetComponent<Stats>();
        if (!Application.isPlaying && entities == null)
        {
            entities = new List<Entity>();
            entities.Add(new Entity());

            for (int i = 0; i < entities.Count; i++)
            {
                //entities[i].m_stats = new Stats();
                entities[i].m_stats = stat;
                entities[i].m_typeEffectiveness.Add(new TypeVariation());
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
                    AddNewStatisticsPrimary(i);
                    AddNewStatisticsSecondary(i);
                }
            }
            for (int i = 0; i < entities.Count; i++)
            {
                if (entities[i].m_primaryStats == null || stat.m_primaryStatistic.Count != entities[i].m_primaryStats.Count)
                    AddNewStatisticsPrimary(i);

                if (entities[i].m_secondaryStats == null || stat.m_secondaryStatistic.Count != entities[i].m_secondaryStats.Count)
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
        if (entities.Count < 1)
        {
            entities = new List<Entity>();
            entities.Add(new Entity());

            for (int i = 0; i < entities.Count; i++)
            {
                AddNewStatisticsPrimary(i);
                AddNewStatisticsSecondary(i);
                entities[i].m_typeEffectiveness = new List<TypeVariation>();
                entities[i].m_typeEffectiveness.Add(new TypeVariation());
            }
        }
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
    public void AddEntity()
    {
        entities.Add(new Entity());
        for (int i = 0; i < entities.Count; i++)
        {
            entities[i].m_stats = GetComponent<Stats>();
            entities[i].m_typeEffectiveness.Add(new TypeVariation());
            AddNewStatisticsPrimary(i);
            AddNewStatisticsSecondary(i);
        }
    }
    public void NewType(int parameter)
    {
        entities[parameter].m_typeEffectiveness.Add(new TypeVariation());
    }
}
