using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BattleCalc : MonoBehaviour
{
    [SerializeField] public Entity m_mainEntity;
    [SerializeField] private List<Entity> m_allEntities;
    private Inventory inventory;
    public static BattleCalc battleCalc;
    [SerializeField] private UI_Inventory uiInventory;
    private bool hasMissed = false;
    private bool isImmune = false;
    private bool canNotHeal = false;
    private void Start()
    {
        battleCalc = this;
        if (m_allEntities != null)
        {
            foreach (var Entity in m_allEntities)
            {
                StatsEffected(Entity);
            }
        }
        inventory = new Inventory(UseItem);
        uiInventory.SetInventory(inventory);
        //ItemWorld.SpawnItemInWorld(new Vector3(20, 20), m_mainEntity.GetAllItemsOnPLayer()[1]);
        //ItemWorld.SpawnItemInWorld(new Vector3(-20, -20), m_mainEntity.GetAllItemsOnPLayer()[0]);
    }
    //When the player interacts with the item on the ground
    public void PickUpItemInWorld(Collider2D collider)
    {
        ItemWorld itemWorld = collider.GetComponent<ItemWorld>();
        if (itemWorld != null)
        {
            inventory.AddItem(itemWorld.GetItem());
            itemWorld.DestroySelf();
        }
    }
    public static Entity GetEntity(string m_nameEntity)
    {
        if (battleCalc.m_allEntities == null)
            return null;
        Predicate<Entity> predicate = (Entity entity) => { return entity.m_name == m_nameEntity; };
        return battleCalc.m_allEntities.Find(predicate);
    }
    //This is using an item from the inventory.
    public void UseItem(ItemID item)
    {
        if (!item.staticItem)
        { }
    }
    #region DamageCalc
    //public double Defend(Moves move, Entity defendEntity, Entity attackEntity) { }
    public bool GetAttackHasMissed()
    {
        return hasMissed;
    }
    //Returns if the durability of the item has been broken.
    public bool DamageDurability(Entity entity, ItemID item, double damage)
    {
        bool hasBeenBroken = false;
        foreach (var itemOnPlayer in entity.GetAllItemsOnPLayer())
        {
            if (item == itemOnPlayer)
            {
                hasBeenBroken = item.DamageDurability(damage);
                return hasBeenBroken;
            }
        }
        return hasBeenBroken;
    }
    public double DoubleReDamageDurability(Entity entity, ItemID item, double damage)
    {
        double itemDur = 0;
        foreach (var itemOnPlayer in entity.GetAllItemsOnPLayer())
        {
            if (item == itemOnPlayer)
            {
                itemDur = item.DoubleReDamageDurability(damage);
                return itemDur;
            }

        }
        return itemDur;
    }
    //Returns attacking output.
    public double AttackMove(Moves move, Entity attackEntity, Entity defendEntity)
    {
        hasMissed = false;
        double damageToDeal = 0;
        bool isAttacking = false;
        bool isStatus = false;
        bool isNone = false;
        if (Accuracy(move))
        {
            for (int i = 0; i < move.type.Count; i++)
            {
                if (move.type[i] == Type.None)
                    isNone = true;
                if (move.type[i] == Type.Attack)
                    isAttacking = true;
                if (move.type[i] == Type.Status)
                    isStatus = true;
            }
            if (isNone == true)
            {
                //Calculate damage.
                damageToDeal = ((2 * attackEntity.level / 5) + 2) * (move.power * EffectivenessMath(move, defendEntity) *
                    DamageIsStab(attackEntity, move)) / 5;
            }
            else if (isAttacking == true)
            {
                //Calculate damage.
                damageToDeal = ((2 * attackEntity.level / 5) + 2) * move.power * (GetStat(move.statsAttack[move.statsIndex],
                    attackEntity) / GetDefenseStat(move.statsAttack[move.statsIndex], defendEntity) * EffectivenessMath(move, defendEntity) *
                    DamageIsStab(attackEntity, move));
            }
            if (isStatus == true && !isNone)
            {
                //Adds the Status Condition
                defendEntity.AddStatus(move.status[0].m_statusEffects[move.status[0].index]);
            }
        }
        else
            hasMissed = true;
        Debug.Log("Player did " + damageToDeal + "to 1 player");
        return damageToDeal;
    }
    public void ItemAttacks(ItemID item, Entity defendEntity)
    {

    }
    public double AttackItemMove(ItemID item, Moves move, Entity attackEntity, Entity defendEntity)
    {
        hasMissed = false;
        double damageToDeal = 0;
        bool isAttacking = false;
        bool isNone = false;
        if (Accuracy(move))
        {
            for (int i = 0; i < move.type.Count; i++)
            {
                if (move.type[i] == Type.None || item.itemType == ItemType.None)
                    isNone = true;
                if (move.type[i] == Type.Attack && item.itemType == ItemType.Weapon)
                    isAttacking = true;
            }

            if (isNone == true)
            {
                //Calculate damage.
                damageToDeal = ((2 * attackEntity.level / 5) + 2) * ((item.valueOfItem * move.power / 2) * EffectivenessMath(move, defendEntity) *
                    DamageIsStab(attackEntity, move)) / 5;
            }
            else if (isAttacking == true)
            {
                //Calculate damage.
                damageToDeal = ((2 * attackEntity.level / 5) + 2) * (item.valueOfItem * move.power / 2) * (GetStat(move.statsAttack[move.statsIndex],
                    attackEntity) / GetDefenseStat(move.statsAttack[move.statsIndex], defendEntity) * EffectivenessMath(move, defendEntity) *
                    DamageIsStab(attackEntity, move));
            }
        }
        else
            hasMissed = true;
        Debug.Log("Player did " + damageToDeal + "to 1 player");
        return damageToDeal;
    }
    //Returns the doubles in damage in the order of the defending entities.
    public List<double> MultiAttack(Moves move, Entity attackEntity, List<Entity> defendEntity)
    {
        List<double> damageToDeal = new List<double>();
        bool isAttacking = false;
        bool isStatus = false;
        bool isNone = false;
        for (int i = 0; i < move.type.Count; i++)
        {
            if (move.type[i] == Type.Attack)
                isAttacking = true;
            if (move.type[i] == Type.None)
                isNone = true;
            if (move.type[i] == Type.Status)
                isStatus = true;
        }
        if (isAttacking == true)
        {
            for (int i = 0; i < defendEntity.Count; i++)
            {
                //Calculates the damage and then adds it to the damage to deal list.
                damageToDeal.Add(((2 * attackEntity.level / 5) + 2) * move.power * (GetStat(move.statsAttack[move.statsIndex],
                    attackEntity) / GetDefenseStat(move.statsAttack[move.statsIndex], defendEntity[i]) * EffectivenessMath(move, defendEntity[i]) *
                    DamageIsStab(attackEntity, move)));
            }
        }
        if (isStatus == true && !isNone)
        {
            for (int i = 0; i < defendEntity.Count; i++)
            {
                defendEntity[i].AddStatus(move.status[0].m_statusEffects[move.status[0].index]);
            }
            //Adds the Status Condition
        }
        return damageToDeal;
    }

    #region Stats
    //Gets the stat of the entity that is attacking
    public double GetStat(string statAttack, Entity m_entity)
    {
        foreach (var stat in m_entity.GetPrimStats())
        {
            if (stat.name == statAttack)
            {
                return stat.stats;
            }
        }
        foreach (var stat in m_entity.GetSecStats())
        {
            if (stat.name == statAttack)
                return stat.stats;
        }
        return 0;
    }
    //Gets the defense stat of the entity defending.
    public double GetDefenseStat(string statAttack, Entity m_entity)
    {
        string defense = "";
        foreach (var item in StatsTypeAgainst.instance.stats)
        {
            if (item.m_attack[item.indexAttack] == statAttack)
            {
                defense = item.m_defense[item.indexDefense];
            }
        }
        if (defense != "")
        {
            foreach (var stat in m_entity.GetPrimStats())
            {
                if (stat.name == defense)
                {
                    return stat.stats;
                }
            }
            foreach (var stat in m_entity.GetSecStats())
            {
                if (stat.name == defense)
                    return stat.stats;
            }
        }
        return 0;
    }
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
    #endregion
    //This is a calculation for the stats that are effecting each other. 
    //The calculation is determined by the entity that is placed into the scene on start.
    #region ChangeInStats
    #region DiminishingEffectsFunction
    public double DiminishingEffects(double value, double valueBeingEffected)
    {
        valueBeingEffected += Mathf.Log10((float)value);
        return valueBeingEffected;
    }
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
    #endregion
    public void StatsEffected(Entity m_entity)
    {
        List<StatsEffected> stats = m_entity.m_statsEffecting;
        List<PrimStatisic> primStats = m_entity.m_primStat;
        List<SecStatistic> secStatistics = m_entity.m_secStat;
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
    public float DamageIsStab(Entity entity, Moves move)
    {
        float stabIncrease = 1;
        bool hasStab = false;
        for (int i = 0; i < move.m_typeEffectiveness.Count; i++)
        {
            for (int j = 0; j < entity.m_typeEffectiveness.Count; j++)
            {
                if (entity.m_typeEffectiveness[i].m_types[entity.m_typeEffectiveness[i].typeIndex]
                    == move.m_typeEffectiveness[i].m_types[move.m_typeEffectiveness[i].typeIndex])
                {
                    if (stabIncrease == 1)
                    {
                        stabIncrease = 1.2f;
                    }
                    else
                        hasStab = true;
                }
                if (hasStab == true)
                {
                    stabIncrease += 0.2f;
                    hasStab = false;
                }
            }

        }
        return stabIncrease;
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
    public void IsHealing(Entity m_entity, double valueToChange)
    {
        m_entity.m_health += valueToChange;
    }
    public void IsDamaging(Entity m_entity, double valueToChange)
    {
        m_entity.m_health -= valueToChange;
    }
    public void IsHealBlocked(Entity m_entity, bool healingIsBlocked)
    {
        m_entity.isHealBlocked = healingIsBlocked;
    }
    public void IsImmuneToDamage(Entity m_entity, bool immuneToDamage)
    {
        m_entity.isImmuneToDamage = immuneToDamage;
    }
    public void IsImmuneToStatus(Entity m_entity, bool immuneToStatus)
    {
        m_entity.isImmuneToStatus = immuneToStatus;
    }
    public void StatusEffectInc(Entity m_entity)
    {
        //if (!isImmune)
        //{
        //    if (m_statusEffecting.effectiveness[i] == HowItEffects.decreaseStat)
        //    {
        //        valueToChange = -m_statusEffecting.valueToChange;
        //    }
        //}
        //if (!canNotHeal)
        //{
        //    if (m_statusEffecting.effectiveness[i] == HowItEffects.increaseStat)
        //    {
        //        valueToChange = m_statusEffecting.valueToChange;
        //    }
        //}
    }
    public void StatusEffectDec(Entity entity)
    {

    }
    //Does all the statusEffects on the player.
    public void DoStatus(Entity entity)
    {
        for (int i = 0; i < entity.onPlayer.Count; i++)
        {
            foreach (var effect in entity.onPlayer[i].effectiveness)
            {
                switch (effect)
                {
                    case HowItEffects.Damage:
                        IsDamaging(entity, entity.onPlayer[i].valueToChange);
                        break;
                    case HowItEffects.Healing:
                        IsHealing(entity, entity.onPlayer[i].valueToChange);
                        break;
                    case HowItEffects.healBlock:
                        IsHealBlocked(entity, true);
                        break;
                    case HowItEffects.immunityToStatus:
                        IsImmuneToStatus(entity, true);
                        break;
                    case HowItEffects.immunityToDamage:
                        IsImmuneToDamage(entity, true);
                        break;
                    case HowItEffects.decreaseStat:
                        StatusEffectInc(entity);
                        break;
                    case HowItEffects.increaseStat:
                        StatusEffectDec(entity);
                        break;
                }
            }

        }
    }
    public double ReturnStatusDamage(Status m_statusEffecting)
    {
        double dmg = 0;
        return dmg;
    }
    //public double ReturnStatusDamage(string m_status)
    //{

    //}




    #endregion
    //Items
    #region ItemsMath

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
    #endregion
}

//Main User Class


