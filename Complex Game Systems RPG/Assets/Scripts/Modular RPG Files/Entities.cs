using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;



//[RequireComponent(typeof(TypeChart))]
//[RequireComponent(typeof(Stats))]
//[ExecuteInEditMode]
//public class Entities : MonoBehaviour
//{
//    [Tooltip("NEEDS A OBJECT FOR THIS TO WORK")]
//    public List<Entity> entities;
//    private Stats stat;
//    private void LateUpdate()
//    {
//        if (stat == null)
//            stat = GetComponent<Stats>();
//        if (!Application.isPlaying)
//        {
//            if (entities == null)
//            {
//                entities = new List<Entity>();
//                entities.Add(new Entity());
//            }
//            for (int i = 0; i < entities.Count; i++)
//            {
//                if (TypeChart.chart == null || entities[i].isTypeChartNull)
//                {
//                    //AddToTypes(i);
//                }
//                AddNewStatisticsPrimary(i);
//                AddNewStatisticsSecondary(i);
//            }
//        }
//        else if (entities.Count >= 1)
//        {
//            if (stat.m_primaryStatistic.Count < 1 || stat.m_secondaryStatistic.Count < 1)
//            {
//                for (int i = 0; i < statsForObjects.m_primaryStatistic.Count; i++)
//                {
//                    entities[i] = new Entity();
//                    AddNewStatisticsPrimary(i);
//                    AddNewStatisticsSecondary(i);
//                }
//            }
//            for (int i = 0; i < entities.Count; i++)
//            {
//                if (entities[i].m_primaryStats == null || stat.m_primaryStatistic.Count != entities[i].m_primaryStats.Count)
//                    AddNewStatisticsPrimary(i);

//                if (entities[i].m_secondaryStats == null || stat.m_secondaryStatistic.Count != entities[i].m_secondaryStats.Count)
//                    AddNewStatisticsSecondary(i);

//                for (int j = 0; j < stat.m_primaryStatistic.Count; j++)
//                {
//                    if (entities[i].m_primaryStats[j] != stat.m_primaryStatistic[j])
//                        AddNewStatisticsPrimary(i);
//                }
//                for (int j = 0; j < stat.m_secondaryStatistic.Count; j++)
//                {
//                    if (entities[i].m_secondaryStats[j] != stat.m_secondaryStatistic[j])
//                        AddNewStatisticsSecondary(i);
//                }
//            }
//        }
//        if (entities.Count < 1)
//        {
//            entities = new List<Entity>();
//            entities.Add(new Entity());

//            for (int i = 0; i < entities.Count; i++)
//            {
//                AddNewStatisticsPrimary(i);
//                AddNewStatisticsSecondary(i);
//                entities[i].m_typeEffectiveness = new List<TypeChart>();
//                entities[i].m_typeEffectiveness.Add(new TypeChart());
//            }
//        }
//    }
//    void AddNewStatisticsPrimary(int parameter)
//    {
//        entities[parameter].m_primaryStats = GetComponent<Stats>().m_primaryStatistic;
//        entities[parameter].m_primaryStats.CopyTo(GetComponent<Stats>().m_primaryStatistic.ToArray(), 0);
//    }
//    void AddNewStatisticsSecondary(int parameter)
//    {
//        entities[parameter].m_secondaryStats = GetComponent<Stats>().m_secondaryStatistic;
//        entities[parameter].m_secondaryStats.CopyTo(GetComponent<Stats>().m_secondaryStatistic.ToArray(), 0);
//    }
//    public void AddEntity()
//    {
//        entities.Add(new Entity());
//        for (int i = 0; i < entities.Count; i++)
//        {
//            entities[i].m_typeEffectiveness.Add(new TypeChart());
//            AddNewStatisticsPrimary(i);
//            AddNewStatisticsSecondary(i);
//        }
//    }
//    public void AddNewTyping(int i)
//    {
//        entities[i].m_typeEffectiveness.Add(new TypeChart());

//        foreach (var item in TypeChart.chart.m_types)
//        {
//            for (int j = 0; j < entities[i].m_typeEffectiveness.Count; j++)
//            {
//                if (entities[i].m_typeEffectiveness[j].m_types == null)
//                    entities[i].m_typeEffectiveness[j].m_types = new List<string>();
//                entities[i].m_typeEffectiveness[j].m_types.Add(item);
//            }
//        }
//    }
//}
