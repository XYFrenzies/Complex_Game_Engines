using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BattleCalc : MonoBehaviour
{
    public List<Entity> m_allEntities;
    public static BattleCalc battleCalc;
    private Entity m_mainEntity;
    private double damageToDeal = 0;
    private void Awake()
    {
        battleCalc = this;
        m_allEntities = new List<Entity>();
        foreach (var item in FindObjectsOfType<Entity>())
        {
            m_allEntities.Add(item);
        }
        m_mainEntity = GetComponent<Entity>();
        StatsEffected();
    }

    public static Entity GetEntity(string m_nameEntity) 
    {
        if (battleCalc.m_allEntities == null)
            return null;
        Predicate<Entity> predicate = (Entity entity) => { return entity.m_name == m_nameEntity; };
        return battleCalc.m_allEntities.Find(predicate);
    }

    public void Attack(Entity[] numOfEntitiesAttacked)
    {

        damageToDeal = BaseDamage();
        //double player1Damage = a_atkEntity;

        //foreach (var entity in numOfEntitiesAttacked)
        //{

        //}
        Debug.Log("Player did " + damageToDeal + "to all players");
    }

    public double BaseDamage()
    {
        double result = 0;

        //Strength (or other attack value) If Stab * 
        return result;
    }
    #region Stats
    //Determines who goes first or when someone will attack.
    //Returns true if left side is larger but is false if right is larger.
    public bool CompareStatsLHSisLarger(PrimStatisic m_mainPrim, PrimStatisic m_enemyPrim)
    {
        if (m_mainPrim == null || m_enemyPrim == null)
            return false;
        if (m_mainPrim.stats > m_enemyPrim.stats)
        {
            return true;
        }
        return false;
        
    }
    #endregion
    

    //This is a calculation for the stats that are effecting each other. 
    //The calculation is determined by the entity that is placed into the scene on start.
    #region ChangeInStats
    public void DiminishingEffects(PrimStatisic prim, double value)
    {
        if (prim.diminishingReturns &&
            prim.stats == prim.max / 2)
            prim.stats += Mathf.Log10((float)value);
    }
    public void DiminishingEffects(SecStatistic sec, double value)
    {
        if (sec.diminishingReturns &&
            sec.stats == sec.max / 2)
            sec.stats += Mathf.Log10((float)value);

    }
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
                CalculateStat(primaryEffectingStat, secondaryEffectedStat, stats[i]);
            }
            else if (secondaryEffectingStat == null)
            {
                CalculateStat(secondaryEffectingStat, secondaryEffectedStat, stats[i]);
            }
            primaryEffectingStat = null;
            secondaryEffectingStat = null;
            secondaryEffectedStat = null;
        }

    }
    //Overloaded functions for primary and secondary change in stats.
    private void CalculateStat(PrimStatisic primary, SecStatistic second, StatsEffected m_stats)
    {
        //Uses the operators in the statseffected to return the funcs.
        second.stats = m_stats.operate(primary.stats, second.stats);
    }
    private void CalculateStat(SecStatistic secondEffecting, SecStatistic secondEffected, StatsEffected m_stats)
    {
        secondEffected.stats = m_stats.operate(secondEffecting.stats, secondEffected.stats);
    }
    #endregion
    //This evaluation is to be used when calculating the result of the attack. 
    //Depending on the effectiveness of all types.
    #region EffectivenessChart
    public float EffectivenessMath(Moves m_moveType, Entity m_enemyEntity)
    {
        float effectOfAttack = 1;
        List<float> m_allTypesCombinedAttack = new List<float>();
        string attack = "";
        string defense = "";
        int typeItrAtt = 0;
        int typeItrDef = 0;
        while (typeItrAtt < m_moveType.m_typeEffectiveness.Count)
        {
            attack = m_moveType.m_typeEffectiveness[typeItrAtt].m_types[m_moveType.m_typeEffectiveness[typeItrAtt].typeIndex];
            defense = m_enemyEntity.m_typeEffectiveness[typeItrDef].m_types[m_enemyEntity.m_typeEffectiveness[typeItrDef].typeIndex];
            m_allTypesCombinedAttack.Add(EffectivenessLibrary(attack, defense));
            typeItrDef++;
            if (typeItrDef > m_enemyEntity.m_typeEffectiveness.Count)
            {
                typeItrDef = 0;
                typeItrAtt++;
            }
        }
        for (int i = 0; i < m_allTypesCombinedAttack.Count; i++)
        {
            effectOfAttack *= m_allTypesCombinedAttack[i];
        }
        return effectOfAttack;
    }
    public float EffectivenessLibrary(string m_attack, string m_defense)
    {
        float effectiveness = 1;
        List<TypingEffectiveness> typeE = TypesEffected.effected.typeEffective;
        for (int i = 0; i < typeE.Count; i++)
        {
            if (typeE[i].nameAttack[typeE[i].indexValueAtt] == m_attack
                && typeE[i].nameDefense[typeE[i].indexValueDef] == m_defense)
            {
                if (typeE[i].effect == Effectiveness.Immune)
                {
                    effectiveness = 0;
                }
                else if (typeE[i].effect == Effectiveness.None || typeE[i].effect == Effectiveness.normalEffective)
                {
                    effectiveness = 1;
                }
                else if (typeE[i].effect == Effectiveness.superEffective)
                {
                    effectiveness = 2;
                }
                else if (typeE[i].effect == Effectiveness.notVeryEffective)
                {
                    effectiveness = 0.5f;
                }
            }
            else
                effectiveness = 1;

        }
        return effectiveness;
    }

    #endregion
    //Accuracy of the attack
    #region AccuracyOfAttack
    public bool Accuracy(Moves m_moveAccuracy)
    {
        bool canHit = true;
        int acc = m_moveAccuracy.Accuracy;

        float accRange = UnityEngine.Random.Range(0, 100);
        if (acc <= accRange)
            canHit = true;
        else
            canHit = false;

        return canHit;
    }
    #endregion
    //Status Effects
    //checks the status condition and converts it to a positive or negative depending on 
    //if the entity is not immune.
    #region StatusEffectsMath
    public double StatusEffectIncDec(Status m_statusEffecting)
    {
        double valueToChange = 0;
        bool isImmune = false;
        bool canNotHeal = false;
        if (m_statusEffecting.effectiveness.Contains(HowItEffects.immunityToStatus))
        {
            isImmune = true;
        }
        if (m_statusEffecting.effectiveness.Contains(HowItEffects.healBlock))
        {
            canNotHeal = true;
        }
        for (int i = 0; i < m_statusEffecting.effectiveness.Count; i++)
        {
            if (!isImmune)
            {
                if (m_statusEffecting.effectiveness[i] == HowItEffects.Damage ||
                    m_statusEffecting.effectiveness[i] == HowItEffects.decreaseStat)
                {
                    valueToChange = -m_statusEffecting.valueToChange;
                }
            }
            if (!canNotHeal)
            {
                if (m_statusEffecting.effectiveness[i] == HowItEffects.Healing ||
                    m_statusEffecting.effectiveness[i] == HowItEffects.increaseStat)
                {
                    valueToChange = m_statusEffecting.valueToChange;
                }
            }
        }
        return valueToChange;
    }
    #endregion
    //Items
    #region ItemsMath
    public void ItemEquiptChangeStats(ItemID item)
    {
        
    }
    public void ItemUnEquiptChangeStats(ItemID item)
    {

    }
    public void UseItem()
    {
        //foreach (var itemFunction in item.itemFunctions)
        //{
        //    itemFunction.DoSomething();
        //}

    }
    //Looks through 1 instance of an items durability
    public bool ItemDurabilityCheck(ItemID item, double calcDmgToDurability)
    {
        bool isDestroyed = false;
        if (item.isDurability == true)
        {
            item.durability -= calcDmgToDurability;
            if (item.durability <= 0)
            {
                isDestroyed = true;
            }
        }
        return isDestroyed;
    }
    //public int ItemEffectiveness() 
    //{
    //    if () { }
    //}
    #endregion
}
//Main User Class


