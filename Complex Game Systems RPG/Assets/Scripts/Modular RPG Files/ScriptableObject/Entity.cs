using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[RequireComponent(typeof(Items))]
[RequireComponent(typeof(MoveSets))]
[RequireComponent(typeof(TypeChart))]
[RequireComponent(typeof(Stats))]
[Serializable]
[CreateAssetMenu(fileName = "Entity", menuName = "Entities")]
public class Entity : ScriptableObject
{
    //DropDownOptions
    [HideInInspector] public bool entityMainShow;
    [HideInInspector] public bool entityTypesShow;
    [HideInInspector] public bool entityMoveSetShow;
    [HideInInspector] public bool entityItemShow;
    [HideInInspector] public bool entityPrimStatShow;
    [HideInInspector] public bool entitySecStatShow;
    [HideInInspector] public bool entityEffectStatShow;
    [HideInInspector] public bool entitySpriteOrObj;
    public bool is2DSprite;
    public GameObject entityOBJ;
    public Sprite sprite2D;
    //MainStats
    public string m_name;
    public string m_descriptionEntity;
    public double m_health;
    public int level;
    public int maxLevel;
    public float currentEXP;
    public float baseEXPYield;
    public float maxEXP;
    //TypeEffectiveness
    public List<TypeChart> m_typeEffectiveness;
    //Stats
    public List<PrimStatisic> m_primStat;
    public List<SecStatistic> m_secStat;
    public List<StatsEffected> m_statsEffecting;
    //Items and the amount of them
    public List<Items> m_itemsOnPlayer;
    public List<int> numOfItemsOnEntity;
    public List<string> m_nameOfItems;
    //MoveSets of the entity
    public List<int> levelForeachMoveset;
    public List<MoveSets> m_currentMoveSets;
    public List<MoveSets> m_learnableMoves;
    public List<string> m_nameOfMovesCurrent;
    public List<string> m_nameOfMovesExternal;
    public List<bool> m_learntPerLevel;
    public List<bool> m_learntExternally;
    public List<Status> onPlayer;
    public List<int> itemIndex;
    public bool hasBeenCreated = false;
    public List<ItemID> itemsEquipped;
    public bool isHealBlocked;
    public bool isImmuneToDamage;
    public bool isImmuneToStatus;
    public void OnEnable()
    {
        if (!UnityEditor.EditorApplication.isPlayingOrWillChangePlaymode && !hasBeenCreated)
        {
            isHealBlocked = false;
            isImmuneToDamage = false;
            isImmuneToStatus = false;
            itemsEquipped = new List<ItemID>();
            hasBeenCreated = true;
            itemIndex = new List<int>();
            level = 1;
            maxLevel = 2;
            maxEXP = 1;
            currentEXP = 0;
            is2DSprite = false;
            onPlayer = new List<Status>();
            m_typeEffectiveness = new List<TypeChart>();
            m_currentMoveSets = new List<MoveSets>();
            m_learnableMoves = new List<MoveSets>();
            m_learntPerLevel = new List<bool>();
            m_learntExternally = new List<bool>();
            m_nameOfMovesCurrent = new List<string>();
            m_nameOfMovesExternal = new List<string>();
            m_primStat = new List<PrimStatisic>();
            m_secStat = new List<SecStatistic>();
            m_statsEffecting = new List<StatsEffected>();
            m_itemsOnPlayer = new List<Items>();
            m_nameOfItems = new List<string>();
            numOfItemsOnEntity = new List<int>();
            levelForeachMoveset = new List<int>();
            m_name = "Name";
            m_descriptionEntity = "This is the entity.";
            m_health = 1;
            entityMainShow = false;
            AddNewType();
            AddNewItem();
            AddMoreCurrentMove();
            AddNewLearnableMove();
            AddNewStatChange();

            AddPrimStat();
            AddSecStat();
            AddMoveset();
        }
        if (currentEXP > maxEXP)
        {
            currentEXP = maxEXP;
        }
        if (level > maxLevel)
        {
            level = maxLevel;
        }

        
    }

    #region StatFunctions
    public void AddPrimStat()
    {
        foreach (var item in Stats.statsForObjects.m_primaryStatistic)
        {
            m_primStat.Add(item);
        }
    }
    public void AddSecStat()
    {
        foreach (var item in Stats.statsForObjects.m_secondaryStatistic)
        {
            m_secStat.Add(item);
        }
    }
    public void AddNewStatChange()
    {
        m_statsEffecting.Add(new StatsEffected());
        for (int i = 0; i < m_statsEffecting.Count; i++)
        {
            m_statsEffecting[i].m_effectingStats = new List<string>();
            m_statsEffecting[i].m_statsEffected = new List<string>();
            m_statsEffecting[i].m_statChanges = new StatChange();
            foreach (var stat in Stats.statsForObjects.m_primaryStatistic)
            {
                m_statsEffecting[i].m_effectingStats.Add(stat.name);
            }
            foreach (var stat in Stats.statsForObjects.m_secondaryStatistic)
            {
                m_statsEffecting[i].m_statsEffected.Add(stat.name);
                m_statsEffecting[i].m_effectingStats.Add(stat.name);
            }
            m_statsEffecting[i].m_statChanges = StatChange.Additive;
        }
    }
    #endregion
    #region ItemFunctions
    public void AddItem(int i)
    {
        foreach (var item in Items.item.m_items)
        {
            m_itemsOnPlayer[i].m_items.Add(item);
            if (Items.item.m_items.Count != m_nameOfItems.Count)
                m_nameOfItems.Add(item.name);
            numOfItemsOnEntity.Add(item.m_amountOfItemsForPlayer);
        }
    }
    public void AddNewItem()
    {
        m_itemsOnPlayer.Add(new Items());
        itemIndex.Add(0);
        for (int i = 0; i < m_itemsOnPlayer.Count; i++)
        {
            m_itemsOnPlayer[i].m_items = new List<ItemID>();
            if (Items.item == null)
            {
                return;
            }
            AddItem(i);
        }
    }
    #endregion
    #region TypeFunctions
    public void AddAllTypes(int i)
    {
        if (TypeChart.chart == null)
            return;
        foreach (var item in TypeChart.chart.m_types)
        {
            m_typeEffectiveness[i].m_types.Add(item);
        }
    }
    //To be used in the inspector
    public void AddNewType()
    {
        m_typeEffectiveness.Add(new TypeChart());
        for (int i = 0; i < m_typeEffectiveness.Count; i++)
        {
            m_typeEffectiveness[i].m_types = new List<string>();
            AddAllTypes(i);
        }
    }
    #endregion
    #region MovesetFunctions
    public void AddMoveset()
    {
        if (MoveSets.moves == null)
            return;
        foreach (var item in MoveSets.moves.m_moveSets)
        {
            m_nameOfMovesCurrent.Add(item.name);
            m_nameOfMovesExternal.Add(item.name);
            m_learntPerLevel.Add(item.isLearntPerLevel);
            m_learntExternally.Add(item.isLearntExternally);
            levelForeachMoveset.Add(item.levelLearnt);
        }
    }
    public void AddMoreCurrentMove()
    {
        m_currentMoveSets.Add(new MoveSets());
        if (MoveSets.moves == null)
            return;
        for (int i = 0; i < m_currentMoveSets.Count; i++)
        {
            m_currentMoveSets[i].m_moveSets = new List<Moves>();
            foreach (var item in MoveSets.moves.m_moveSets)
            {
                m_currentMoveSets[i].m_moveSets.Add(item);
            }
        }
    }
    public void AddNewLearnableMove()
    {

        m_learnableMoves.Add(new MoveSets());
        if (MoveSets.moves == null)
            return;
        for (int i = 0; i < m_learnableMoves.Count; i++)
        {
            m_learnableMoves[i].m_moveSets = new List<Moves>();
            foreach (var item in MoveSets.moves.m_moveSets)
            {
                m_learnableMoves[i].m_moveSets.Add(item);
                m_learntPerLevel.Add(item.isLearntPerLevel);
                m_learntExternally.Add(item.isLearntExternally);
            }
        }
    }
    #endregion
    #region publicFunction
    #region Image
    public void NewImage(Sprite sprite)
    {
        sprite2D = sprite;
    }
    public Sprite GetSpriteInfo()
    {
        if (is2DSprite == true)
        {
            return sprite2D;
        }
        return null;
    }
    public GameObject GetGameObj()
    {
        if (!is2DSprite)
        {
            return entityOBJ;
        }
        return null;
    }
    public void NewImage(GameObject obj)
    {
        entityOBJ = obj;
    }
    #endregion
    #region Movesets
    public bool FindCurrentMoveset(string moveName)
    {
        for (int i = 0; i < m_nameOfMovesCurrent.Count; i++)
        {
            if (m_nameOfMovesCurrent[i] == moveName)
            {
                return true;
            }
        }
        return false;
    }
    public Moves GetCurrentMoveset(string moveName)
    {
        for (int i = 0; i < m_currentMoveSets.Count; i++)
        {
            for (int j = 0; j < m_currentMoveSets[i].m_moveSets.Count; j++)
            {
                if (m_currentMoveSets[i].m_moveSets[j].name == moveName)
                {
                    return m_currentMoveSets[i].m_moveSets[j];
                }
            }
        }
        return null;
    }
    public List<MoveSets> GetCurrentMovesets()
    {
        return m_currentMoveSets;
    }
    public List<MoveSets> GetLearnableMovesets()
    {
        return m_learnableMoves;
    }
    //Make functions to quickely access the entities methods.
    #endregion
    #region Items
    public void AddCurrentItem(ItemID item, int amount)
    {
        for (int i = 0; i < m_itemsOnPlayer.Count; i++)
        {
            if (item == m_itemsOnPlayer[i].GetItems(item.name))
            {
                numOfItemsOnEntity[i] += amount;
            }
        }
    }
    public void AddCurrentItem(ItemID item)
    {
        for (int i = 0; i < m_itemsOnPlayer.Count; i++)
        {
            if (item == m_itemsOnPlayer[i].GetItems(item.name))
            {
                numOfItemsOnEntity[i] += 1;
            }
        }
    }
    public void DecreaseCurrentItem(ItemID item)
    {
        for (int i = 0; i < m_itemsOnPlayer.Count; i++)
        {
            if (item == m_itemsOnPlayer[i].GetItems(item.name))
            {
                numOfItemsOnEntity[i] -= 1;
            }
        }
    }
    public void DecreaseCurrentItem(ItemID item, int amount)
    {
        for (int i = 0; i < m_itemsOnPlayer.Count; i++)
        {
            if (item == m_itemsOnPlayer[i].GetItems(item.name))
            {
                numOfItemsOnEntity[i] -= amount;
            }
        }
    }
    public void AddNewItemNotOnPlayer(ItemID item)
    {
        bool thereIsItem = false;
        if (Items.item == null)
            Debug.Log("An error has occured when making a new item!");
        for (int i = 0; i < Items.item.m_items.Count; i++)
        {
            if (Items.item.m_items[i] == item)
            {
                m_nameOfItems.Add(item.name);
                numOfItemsOnEntity.Add(1);
                thereIsItem = true;
            }
        }
        if (!thereIsItem)
        {
            Debug.Log("No such Item exists!");
        }
    }
    public void AddNewItemNotOnPlayer(ItemID item, int amount)
    {
        bool thereIsItem = false;
        if (Items.item == null)
            Debug.Log("An error has occured when making a new item!");
        for (int i = 0; i < Items.item.m_items.Count; i++)
        {
            if (Items.item.m_items[i] == item)
            {
                m_nameOfItems.Add(item.name);
                numOfItemsOnEntity.Add(amount);
                thereIsItem = true;
            }
        }
        if (!thereIsItem)
        {
            Debug.Log("No such Item exists!");
        }
    }
    //This goes through every item and determines if it increases or decreases the stat depending on if its equipt or used.
    public void ItemEquiptChangeStats(ItemID item)
    {
        for (int i = 0; i < m_itemsOnPlayer.Count; i++)
        {
            if (item == m_itemsOnPlayer[i].GetItems(item.name))
            {
                itemsEquipped.Add(item);
                if (item.properties.Count != 0)
                {
                    for (int j = 0; j < item.properties.Count; j++)
                    {
                        if (item.properties[j] == ItemProperties.DecreaseStats)
                        {
                            foreach (var stat in m_primStat)
                            {
                                if (item.allStatsEffected[j][item.m_statIndex[j]] == stat.name)
                                {
                                    stat.stats -= item.valueOfItem;
                                    if (stat.stats < 0)
                                    {
                                        stat.stats = 0;
                                    }
                                }
                            }
                        }
                        if (item.properties[j] == ItemProperties.IncreaseStats)
                        {
                            foreach (var stat in m_primStat)
                            {
                                if (item.allStatsEffected[j][item.m_statIndex[j]] == stat.name)
                                {
                                    if ((stat.stats * 2) > stat.max)
                                    {
                                        //Diminishing effects of the stats as it goes over halfway.
                                        stat.stats += Mathf.Log10((float)item.valueOfItem);
                                    }
                                    else
                                    {
                                        stat.stats += item.valueOfItem;
                                    }
                                }
                            }
                        }
                        if (item.properties[j] == ItemProperties.IncludeStatus)
                        {
                            for (int k = 0; k < item.statusNames.Count; k++)
                            {
                                foreach (var status in StatusEffects.status.m_statusEffects)
                                {
                                    if (item.statusNames[k][item.statusIndex[k]] == status.name)
                                    {
                                        onPlayer.Add(status);
                                    }
                                }
                            }
                        }
                    }
                }
                return;
            }
        }
    }
    //This goes through every item and removes or the additions and subtractions of the stat changes.
    public void ItemUnEquiptChangeStats(ItemID item)
    {
        for (int i = 0; i < m_itemsOnPlayer.Count; i++)
        {
            if (item == m_itemsOnPlayer[i].GetItems(item.name))
            {
                itemsEquipped.Remove(item);
                if (item.properties.Count != 0)
                {
                    for (int j = 0; j < item.properties.Count; j++)
                    {
                        if (item.properties[j] == ItemProperties.DecreaseStats)
                        {
                            foreach (var stat in m_primStat)
                            {
                                if (item.allStatsEffected[j][item.m_statIndex[j]] == stat.name)
                                {
                                    stat.stats += item.valueOfItem;
                                }
                            }
                        }
                        if (item.properties[j] == ItemProperties.IncreaseStats)
                        {
                            foreach (var stat in m_primStat)
                            {
                                if (item.allStatsEffected[j][item.m_statIndex[j]] == stat.name)
                                {
                                    if ((stat.stats * 2) > stat.max)
                                    {
                                        //Diminishing effects of the stats as it goes over halfway.
                                        stat.stats -= Mathf.Log10((float)item.valueOfItem);
                                    }
                                    else
                                    {
                                        stat.stats -= item.valueOfItem;
                                    }
                                }
                            }
                        }
                        if (item.properties[j] == ItemProperties.IncludeStatus)
                        {
                            for (int k = 0; k < item.statusNames.Count; k++)
                            {
                                foreach (var status in StatusEffects.status.m_statusEffects)
                                {
                                    if (item.statusNames[k][item.statusIndex[k]] == status.name)
                                    {
                                        onPlayer.Remove(status);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
    public List<ItemID> GetAllItemsOnPLayer()
    {
        List<ItemID> items = new List<ItemID>();

        for (int i = 0; i < m_itemsOnPlayer.Count; i++)
        {
            if (m_itemsOnPlayer[i] == null)
                m_itemsOnPlayer[i] = Items.item;
            for (int j = 0; j < m_itemsOnPlayer[i].m_items.Count; j++)
            {
                if (m_itemsOnPlayer[i].m_items[j].name == m_nameOfItems[itemIndex[i]])
                    items.Add(m_itemsOnPlayer[i].m_items[itemIndex[i]]);
            }
        }
        return items;
    }
    public string GetItemDescription(string name)
    {
        for (int i = 0; i < m_itemsOnPlayer.Count; i++)
        {
            foreach (var item in m_itemsOnPlayer[i].m_items)
            {
                if (name == item.GetName())
                {
                    return item.GetDescription();
                }
            }

        }
        return "";
    }
    public double GetItemValue(string name)
    {
        for (int i = 0; i < m_itemsOnPlayer.Count; i++)
        {
            foreach (var item in m_itemsOnPlayer[i].m_items)
            {
                if (name == item.GetName())
                {
                    return item.GetItemValue();
                }
            }

        }
        return 0;
    }
    public double GetDurability(ItemID item)
    {
        for (int i = 0; i < m_itemsOnPlayer.Count; i++)
        {
            if (item == m_itemsOnPlayer[i].GetItems(item.name))
            {
                return m_itemsOnPlayer[i].GetItems(item.name).GetDurability();
            }
        }
        return 0;
    }
    public double GetDurability(string name)
    {
        for (int i = 0; i < m_itemsOnPlayer.Count; i++)
        {
            foreach (var item in m_itemsOnPlayer[i].m_items)
            {
                if (name == item.GetName())
                {
                    return item.GetDurability();
                }
            }
        }
        return 0;
    }
    #endregion
    #region Leveling
    public float GainExpMath(int a_levelOfEnemy, float a_baseEXPYieldEnemy, int a_amountOfUnitsInBattleAlly)
    {
        float m_mathOfBattle;
        m_mathOfBattle = ((a_baseEXPYieldEnemy * a_levelOfEnemy) / (5 * a_amountOfUnitsInBattleAlly)) 
            * ((2 * a_levelOfEnemy + 10) / a_levelOfEnemy + level + 10) + 1;
        return m_mathOfBattle;
    }
    public float GainExpMath(int a_levelOfEnemy, float a_baseEXPYieldEnemy, 
        int a_amountOfUnitsInBattleAlly, int mainEntityLevel)
    {
        float m_mathOfBattle;
        m_mathOfBattle = ((a_baseEXPYieldEnemy * a_levelOfEnemy) / (5 * a_amountOfUnitsInBattleAlly))
            * ((2 * a_levelOfEnemy + 10) / a_levelOfEnemy + mainEntityLevel + 10) + 1;
        return m_mathOfBattle;
    }
    public int ReturnLevelUp(int a_level, double a_currentEXP)
    {
        double expForLevel;
        expForLevel = Math.Pow(a_level, 3);
        if (a_currentEXP >= expForLevel)
        {
            a_level += 1;
        }
        return a_level;
    }
    //Levels up the entity by 1 stage.
    public void LevelUpThisEntity() 
    {
        level += 1;
    }
    public void LevelUpEntity(int amount)
    {
        level += amount;
    }
    public double GetMinExp()
    {
        double expForLevel = 0;
        if (level != 1)
            expForLevel = Math.Pow(level - 1, 3);
        return expForLevel;
    }
    public double GetMaxExpToGet()
    {
        double expForLevel = Math.Pow(level, 3);
        return expForLevel;
    }
    public void ReturnLevelUp()
    {
        double expForLevel = Math.Pow(level, 3);
        if (expForLevel <= currentEXP)
        {
            if (level != maxLevel)
                level += 1;
        }
    }
    #endregion
    #region GetBaseStats
    public double GetHealth()
    {
        return m_health;
    }
    public void IncreaseHealth(double amount)
    {
        m_health += amount;
    }
    public string GetDescription()
    {
        return m_descriptionEntity;
    }
    public string GetCurrentMoveDescription(string moveFind)
    {
        for (int i = 0; i < m_currentMoveSets.Count; i++)
        {
            if (moveFind != null && moveFind == m_currentMoveSets[i].GetMoves(m_nameOfMovesCurrent[i]).name)
            {
                return m_currentMoveSets[i].GetMoves(m_nameOfMovesCurrent[i]).description;
            }
        }
        return "";
    }
    public string GetStatusDescription(string status)
    {
        if (StatusEffects.status != null)
        {
            for (int i = 0; i < StatusEffects.status.m_statusEffects.Count; i++)
            {
                if (status != null && status == StatusEffects.status.m_statusEffects[i].name)
                {
                    return StatusEffects.status.m_statusEffects[i].description;
                }
            }
        }
        return "";
    }
    public List<string> GetCurrentStatusDescription()
    {
        foreach (var status in onPlayer)
        {
            onPlayer.Add(status);
        }
        return null;
    }
    public int GetLevel() 
    {
        return level;
    }
    public int GetMaxLevel() 
    {
        return maxLevel;
    }
    public double GetBaseEXPYield() 
    {
        return baseEXPYield;
    }
    public double CurrentEXP() 
    {
        return currentEXP;
    }
    public double MaxEXP() 
    {
        return maxEXP;
    }
    #endregion
    #region Stats
    public List<PrimStatisic> GetPrimStats()
    {
        return m_primStat;
    }
    public List<SecStatistic> GetSecStats()
    {
        return m_secStat;
    }
    public PrimStatisic GetPrimStats(string primstat)
    {
        foreach (var stat in m_primStat)
        {
            if (stat.name == primstat)
                return stat;
        }
        return null;
    }
    public SecStatistic GetSecStats(string secstat)
    {
        foreach (var stat in m_secStat)
        {
            if (stat.name == secstat)
                return stat;
        }
        return null;
    }
    #endregion
    #region Status
    public void AddStatus(Status status)
    {
        if (onPlayer == null)
            onPlayer = new List<Status>();
        onPlayer.Add(status);
    }
    public void DeleteStatus(Status status)
    {
        foreach (var item in onPlayer)
        {
            if (status == item)
            {
                onPlayer.Remove(item);
                return;
            }
        }
    }
    public List<Status> GetAllStatus()
    {
        return onPlayer;
    }
    public Status GetStatusOnPlayer(string status)
    {
        foreach (var statusOnPlayer in onPlayer)
        {
            if (status == statusOnPlayer.name)
                return statusOnPlayer;
        }
        return null;
    }
    #endregion
    #endregion
}
