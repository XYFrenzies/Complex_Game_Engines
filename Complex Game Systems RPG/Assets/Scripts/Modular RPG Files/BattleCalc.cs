using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class BattleCalc : MonoBehaviour
{
    public List<Entity> m_allEntities;
    private Entity m_mainEntity;
    private double damageToDeal = 0;
    private void Awake()
    {
        m_allEntities = new List<Entity>();
        foreach (var item in FindObjectsOfType<Entity>())
        {
            m_allEntities.Add(item);
        }
        m_mainEntity = GetComponent<Entity>();
    }
    private void Update()
    {

    }
    public double Attack(Entity[] numOfEntitiesAttacked)
    {


        //double player1Damage = a_atkEntity;

        //foreach (var entity in numOfEntitiesAttacked)
        //{

        //}
        Debug.Log("Player did " + damageToDeal + "to all players");
        return damageToDeal;
    }

    public void BaseDamage()
    {
        //Strength (or other attack value) If Stab * 
    }
    //This is a calculation for the stats that are effecting each other. 
    //The calculation is determined by the entity that is placed into the scene on start.
    #region ChangeInStats
    public void StatsEffected()
    {
        List<StatsEffected> stats = m_mainEntity.m_statsEffecting;
        List<PrimStatisic> primStats = m_mainEntity.m_primStat;
        List<SecStatistic> secStatistics = m_mainEntity.m_secStat;
        PrimStatisic primaryEffectingStat = null;
        SecStatistic secondaryEffectingStat = null;
        SecStatistic secondaryEffectedStat = null;
        for (int i = 0; i < stats.Count; i++)
        {
            if (stats[i].m_effectingStats[stats[i].indexValueEffecting].Contains(primStats[i].name))
            {
                primaryEffectingStat = primStats[i];
            }
            if (stats[i].m_effectingStats[stats[i].indexValueEffecting].Contains(secStatistics[i].name))
            {
                secondaryEffectingStat = secStatistics[i];
            }
            if (stats[i].m_statsEffected[stats[i].indexValueEffected].Contains(secStatistics[i].name))
            {
                secondaryEffectedStat = secStatistics[i];
            }
            if (primaryEffectingStat == null)
            {
                CalculateStat(primaryEffectingStat, secondaryEffectedStat, stats[i].m_statChanges, stats[i]);
            }
            else if (secondaryEffectingStat == null)
            {
                CalculateStat(secondaryEffectingStat, secondaryEffectedStat, stats[i].m_statChanges, stats[i]);
            }
            primaryEffectingStat = null;
            secondaryEffectingStat = null;
            secondaryEffectedStat = null;
        }

    }

    //Overloaded functions for primary and secondary change in stats.
    private void CalculateStat(PrimStatisic primary, SecStatistic second, StatChange change, StatsEffected stat)
    {
        if (change == StatChange.Additive)
            second.stats = primary.stats + second.stats;
        else if (change == StatChange.Subtraction)
            second.stats = primary.stats - second.stats;
        else if (change == StatChange.Multiplicative)
            second.stats = primary.stats * second.stats;
        else if (change == StatChange.Division)
            second.stats = primary.stats / second.stats;
    }
    private void CalculateStat(SecStatistic secondEffecting, SecStatistic secondEffected, StatChange change, StatsEffected stat)
    {
        if (change == StatChange.Additive)
            secondEffected.stats = secondEffecting.stats + secondEffected.stats;
        else if (change == StatChange.Subtraction)
            secondEffected.stats = secondEffecting.stats - secondEffected.stats;
        else if (change == StatChange.Multiplicative)
            secondEffected.stats = secondEffecting.stats * secondEffected.stats;
        else if (change == StatChange.Division)
            secondEffected.stats = secondEffecting.stats / secondEffected.stats;
    }
    #endregion
    #region EffectivenessChart
    public void EffectivenessMath(TypeChart m_moveType, )
    {
        List<TypingEffectiveness> typeE = TypesEffected.effected.typeEffective;
        List<TypeChart> m_types = m_mainEntity.m_typeEffectiveness;

        for (int i = 0; i < typeE.Count; i++)
        {
            //if (typeE[i].nameAttack[typeE[i].indexValueAtt].Contains(m_types[i].name))
            //{
            //    primaryEffectingStat = primStats[i];
            //}
            //    if (stats[i].m_effectingStats[stats[i].indexValueEffecting].Contains(secStatistics[i].name))
            //    {
            //        secondaryEffectingStat = secStatistics[i];
            //    }
            //    if (stats[i].m_statsEffected[stats[i].indexValueEffected].Contains(secStatistics[i].name))
            //    {
            //        secondaryEffectedStat = secStatistics[i];
            //    }
            //    if (primaryEffectingStat == null)
            //    {
            //        CalculateStat(primaryEffectingStat, secondaryEffectedStat, stats[i].m_statChanges, stats[i]);
            //    }
            //    else if (secondaryEffectingStat == null)
            //    {
            //        CalculateStat(secondaryEffectingStat, secondaryEffectedStat, stats[i].m_statChanges, stats[i]);
            //    }

        }


    }
    #endregion

}
//Main User Class
public class CombatReadyEntity : Entity
{

}
//Main Enemy Class
public class EnemyAIEntity : Entity
{

}
public class StaticEntity : Entity
{

}

public class PassiveEntity : Entity
{

}


