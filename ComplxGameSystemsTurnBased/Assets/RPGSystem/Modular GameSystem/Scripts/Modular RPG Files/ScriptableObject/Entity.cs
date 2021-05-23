using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

[RequireComponent(typeof(Items))]
[RequireComponent(typeof(MoveSets))]
[RequireComponent(typeof(TypeChart))]
[RequireComponent(typeof(Stats))]
[Serializable]
[CreateAssetMenu(fileName = "Entity", menuName = "Entities")]
public class Entity : ScriptableObject
{
    //DropDownOptions
    [SerializeField] [HideInInspector] public bool entityMainShow;
    [SerializeField] [HideInInspector] public bool entityTypesShow;
    [SerializeField] [HideInInspector] public bool entityMoveSetShow;
    [SerializeField] [HideInInspector] public bool entityItemShow;
    [SerializeField] [HideInInspector] public bool entityPrimStatShow;
    [SerializeField] [HideInInspector] public bool entitySecStatShow;
    [SerializeField] [HideInInspector] public bool entityEffectStatShow;
    [SerializeField] [HideInInspector] public bool entitySpriteOrObj;
    [SerializeField] public bool is2DSprite;
    [SerializeField] public GameObject entityOBJ;
    [SerializeField] public Sprite sprite2D;
    //MainStats
    [SerializeField] public string m_name;
    [SerializeField] public string m_descriptionEntity;
    [SerializeField] public double m_health;
    [SerializeField] public int level;
    [SerializeField] public int maxLevel;
    [SerializeField] public float currentEXP;
    [SerializeField] public float baseEXPYield;
    [SerializeField] public float maxEXP;
    //TypeEffectiveness
    [SerializeField] public List<List<string>> m_typeEffectiveness;
    [SerializeField] public List<int> typeIndex;
    // public List<TypeChart> m_typeEffectiveness;
    //Stats
    [SerializeField] public List<PrimStatisic> m_primStat;
    [SerializeField] public List<SecStatistic> m_secStat;
    //public List<StatsEffected> m_statsEffecting;
    //Items and the amount of them
    [SerializeField] public List<Items> m_itemsOnPlayer;
    [SerializeField] public List<int> numOfItemsOnEntity;
    [SerializeField] public List<string> m_nameOfItems;
    //MoveSets of the entity
    [SerializeField] public List<int> levelForeachMoveset;
    [SerializeField] public List<int> currentMoveSetIndex;
    [SerializeField] public List<int> learnableMoveSetIndex;
    [SerializeField] public List<List<string>> m_nameOfMovesCurrent;
    [SerializeField] public List<string> currentMoves;
    [SerializeField] public List<List<string>> m_nameOfMovesExternal;
    [SerializeField] public List<bool> m_learntPerLevel;
    [SerializeField] public List<bool> m_learntExternally;
    //Status of the entity
    [SerializeField] public List<Status> onPlayer;
    [SerializeField] public List<int> itemIndex;
    [SerializeField] public bool hasBeenCreated = false;
    [SerializeField] public List<ItemID> itemsEquipped;
    [SerializeField] public bool isHealBlocked;
    [SerializeField] public bool isImmuneToDamage;
    [SerializeField] public bool isImmuneToStatus;
    [SerializeField] bool alreadyLoaded = false;
    [SerializeField] public float IDSave;
    [SerializeField] public List<Moves> moveSets;
    private void OnValidate()
    {
        //#if UNITY_EDITOR
        Load();
        //#endif
    }
    //When a new version of an scriptableobject is created, the onenable will occur.
    public void OnEnable()
    {
#if UNITY_EDITOR
        if (UnityEditor.EditorApplication.isPlayingOrWillChangePlaymode && !alreadyLoaded)
        {
            Load();
            alreadyLoaded = true;
        }
        if (!UnityEditor.EditorApplication.isPlayingOrWillChangePlaymode && !hasBeenCreated)
        {
            moveSets = new List<Moves>();
            currentMoves = new List<string>();
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
            typeIndex = new List<int>();
            currentMoveSetIndex = new List<int>();
            learnableMoveSetIndex = new List<int>();
            m_typeEffectiveness = new List<List<string>>();
            m_learntPerLevel = new List<bool>();
            m_learntExternally = new List<bool>();
            m_nameOfMovesCurrent = new List<List<string>>();
            m_nameOfMovesExternal = new List<List<string>>();
            m_primStat = new List<PrimStatisic>();
            m_secStat = new List<SecStatistic>();
            //m_statsEffecting = new List<StatsEffected>();
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
            //AddNewStatChange();
            SetMoveSets();
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
#endif
    }



    public void SetMoveSets()
    {
        foreach (var item in MoveSets.moves.m_moveSets)
        {
            moveSets.Add(item);
        }
    }
    public void Save()
    {
        IDSave = UnityEngine.Random.Range(1, 100);
        PlayerPrefs.SetFloat("IDSave" + IDSave, IDSave);
        PlayerPrefs.SetString("Name" + m_name, m_name);
        PlayerPrefs.SetString("Description" + m_descriptionEntity, m_descriptionEntity);
        PlayerPrefs.SetFloat("Health" + m_health.ToString(), (float)m_health);
        PlayerPrefs.SetFloat("currentEXP" + currentEXP.ToString(), currentEXP);
        PlayerPrefs.SetFloat("baseEXPYield" + baseEXPYield.ToString(), baseEXPYield);
        PlayerPrefs.SetFloat("maxEXP" + maxEXP.ToString(), maxEXP);
        PlayerPrefs.SetInt("Level" + level.ToString(), level);
        PlayerPrefs.SetInt("maxlevel" + maxLevel.ToString(), maxLevel);
        int statusIndex = 1;
        PlayerPrefs.SetInt("The amount of types.", m_typeEffectiveness.Count);
        //For saving types
        PlayerPrefsX.SetStringArray("Types", m_typeEffectiveness[0].ToArray());
        PlayerPrefsX.SetIntArray("typeIndex" + IDSave, typeIndex.ToArray());


        PlayerPrefsX.SetStringArray("CurrentMoveset", m_nameOfMovesCurrent[0].ToArray());
        PlayerPrefsX.SetIntArray("CurrentMovesetIndex" + IDSave, currentMoveSetIndex.ToArray());
        PlayerPrefsX.SetIntArray("LearnableMovesetIndex" + IDSave, learnableMoveSetIndex.ToArray());
        PlayerPrefsX.SetIntArray("numberToLearnMoveset" + IDSave, levelForeachMoveset.ToArray());
        PlayerPrefsX.SetStringArray("nameOFLearntMoves", m_nameOfMovesExternal[0].ToArray());

        PlayerPrefsX.SetIntArray("itemIndex" + IDSave, itemIndex.ToArray());
        PlayerPrefsX.SetIntArray("numberOfItems" + IDSave, numOfItemsOnEntity.ToArray());
        PlayerPrefsX.SetStringArray("nameofItems", m_nameOfItems.ToArray());
        PlayerPrefs.SetInt("statusAmount", onPlayer.Count);
        foreach (var status in onPlayer)
        {
            PlayerPrefs.SetString("StatusName" + statusIndex, status.name);
            statusIndex++;
        }


        //PlayerPrefs.SetInt("AmountOFStatChanges", m_statsEffecting.Count);
        //for (int i = 0; i < m_statsEffecting.Count; i++)
        //{
        //    PlayerPrefsX.SetStringArray("effectingStats", m_statsEffecting[i].m_effectingStats.ToArray());
        //    PlayerPrefsX.SetStringArray("effectedStats", m_statsEffecting[i].m_statsEffected.ToArray());
        //    PlayerPrefs.SetString("enumstatChanges", m_statsEffecting[i].m_statChanges.ToString());
        //}
        PlayerPrefs.Save();

    }

    public void Load()
    {
        IDSave = PlayerPrefs.GetFloat("IDSave" + IDSave);
        m_name = PlayerPrefs.GetString("Name" + m_name);
        m_descriptionEntity = PlayerPrefs.GetString("Description" + m_descriptionEntity);
        m_health = PlayerPrefs.GetFloat("Health" + m_health.ToString());
        currentEXP = PlayerPrefs.GetFloat("currentEXP" + currentEXP.ToString());
        baseEXPYield = PlayerPrefs.GetFloat("baseEXPYield" + baseEXPYield.ToString());
        maxEXP = PlayerPrefs.GetFloat("maxEXP" + maxEXP.ToString());
        level = PlayerPrefs.GetInt("Level" + level.ToString());
        maxLevel = PlayerPrefs.GetInt("maxlevel" + maxLevel.ToString());
        int statusIndex = 1;
        //Types
        if (m_typeEffectiveness == null)
        {
            m_typeEffectiveness = new List<List<string>>();
            m_typeEffectiveness.Add(new List<string>());
        }
        typeIndex = PlayerPrefsX.GetIntArray("typeIndex" + IDSave).ToList();
        m_typeEffectiveness[0] = PlayerPrefsX.GetStringArray("Types").ToList();
        while (m_typeEffectiveness.Count != typeIndex.Count)
        {
            if (m_typeEffectiveness.Count < typeIndex.Count)
                m_typeEffectiveness.Add(PlayerPrefsX.GetStringArray("Types").ToList());
            else if (m_typeEffectiveness.Count > typeIndex.Count)
                m_typeEffectiveness.RemoveAt(m_typeEffectiveness.Count - 1);
        }
        ////Moves
        if (m_nameOfMovesCurrent == null || m_nameOfMovesExternal == null)
        {
            m_nameOfMovesCurrent = new List<List<string>>();
            m_nameOfMovesExternal = new List<List<string>>();
            m_nameOfMovesCurrent.Add(new List<string>());
            m_nameOfMovesExternal.Add(new List<string>());
        }
        currentMoveSetIndex = PlayerPrefsX.GetIntArray("CurrentMovesetIndex" + IDSave).ToList();
        learnableMoveSetIndex = PlayerPrefsX.GetIntArray("LearnableMovesetIndex" + IDSave).ToList();
        levelForeachMoveset = PlayerPrefsX.GetIntArray("numberToLearnMoveset" + IDSave).ToList();
        m_nameOfMovesCurrent[0] = PlayerPrefsX.GetStringArray("nameOFCurrentMoves").ToList();
        m_nameOfMovesExternal[0] = PlayerPrefsX.GetStringArray("nameOFLearntMoves").ToList();
        while (m_nameOfMovesCurrent.Count != currentMoveSetIndex.Count)
        {
            if (m_nameOfMovesCurrent.Count < currentMoveSetIndex.Count)
                m_nameOfMovesCurrent.Add(PlayerPrefsX.GetStringArray("nameOFCurrentMoves").ToList());
            else if (m_nameOfMovesCurrent.Count > currentMoveSetIndex.Count)
                m_nameOfMovesCurrent.RemoveAt(m_nameOfMovesCurrent.Count - 1);
            if (m_nameOfMovesExternal.Count < learnableMoveSetIndex.Count)
                m_nameOfMovesExternal.Add(PlayerPrefsX.GetStringArray("nameOFLearntMoves").ToList());
            else if (m_nameOfMovesExternal.Count > learnableMoveSetIndex.Count)
                m_nameOfMovesExternal.RemoveAt(m_nameOfMovesExternal.Count - 1);
        }
        itemIndex = PlayerPrefsX.GetIntArray("itemIndex" + IDSave).ToList();
        numOfItemsOnEntity = PlayerPrefsX.GetIntArray("numberOfItems" + IDSave).ToList();
        m_nameOfItems = PlayerPrefsX.GetStringArray("nameofItems").ToList();
        int amount = PlayerPrefs.GetInt("statusAmount");
        if (onPlayer == null)
            onPlayer = new List<Status>();

        for (int i = 0; i < amount; i++)
        {
            string statusEffect = PlayerPrefs.GetString("StatusName");
            foreach (var status in StatusEffects.status.m_statusEffects)
            {
                if (statusEffect == status.name)
                    onPlayer.Add(status);
            }
            statusIndex++;
        }
        //if (m_statsEffecting == null)
        //{
        //    m_statsEffecting = new List<StatsEffected>();
        //}
        //int amountofChanges = PlayerPrefs.GetInt("AmountOFStatChanges");
        //for (int i = 0; i < amountofChanges; i++)
        //{
        //    m_statsEffecting[i] = StatsEffected.m_effectStats;
        //    m_statsEffecting[i].m_effectingStats = PlayerPrefsX.GetStringArray("effectingStats").ToList();
        //    m_statsEffecting[i].m_statsEffected = PlayerPrefsX.GetStringArray("effectedStats").ToList();
        //    m_statsEffecting[i].m_statChanges = (StatChange)System.Enum.Parse(typeof(StatChange),
        //        PlayerPrefs.GetString("enumstatChanges"));
        //}
    }


    #region StatFunctions
    //Adds a new primary stat to the entity
    public void AddPrimStat()
    {
        foreach (var item in Stats.statsForObjects.m_primaryStatistic)
        {
            m_primStat.Add(item);
        }
    }
    //Adds a new secondary stat to the entity
    public void AddSecStat()
    {
        foreach (var item in Stats.statsForObjects.m_secondaryStatistic)
        {
            m_secStat.Add(item);
        }
    }
    //Adds a new stat change to the entity
    //It creates a new instance of the entity, makes the statchanges to it and then adds the stats to the lists.
    //public void AddNewStatChange()
    //{
    //    m_statsEffecting.Add(new StatsEffected());
    //    for (int i = 0; i < m_statsEffecting.Count; i++)
    //    {
    //        m_statsEffecting[i].m_effectingStats = new List<string>();
    //        m_statsEffecting[i].m_statsEffected = new List<string>();
    //        m_statsEffecting[i].m_statChanges = new StatChange();
    //        foreach (var stat in Stats.statsForObjects.m_primaryStatistic)
    //        {
    //            m_statsEffecting[i].m_effectingStats.Add(stat.name);
    //        }
    //        foreach (var stat in Stats.statsForObjects.m_secondaryStatistic)
    //        {
    //            m_statsEffecting[i].m_statsEffected.Add(stat.name);
    //            m_statsEffecting[i].m_effectingStats.Add(stat.name);
    //        }
    //        m_statsEffecting[i].m_statChanges = StatChange.Additive;
    //    }
    //}
    #endregion
    #region ItemFunctions
    //Adds a new item to the entity
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
    //Adds a new intance of the item to the entity
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
    //Adds the all the types to the entity.
    public void AddAllTypes(int i)
    {
        if (TypeChart.chart == null)
            return;
        foreach (var item in TypeChart.chart.m_types)
        {
            m_typeEffectiveness[i].Add(item);
        }
    }
    //To be used in the inspector
    public void AddNewType()
    {
        m_typeEffectiveness.Add(new List<string>());
        typeIndex.Add(0);
        for (int i = 0; i < m_typeEffectiveness.Count; i++)
        {
            AddAllTypes(i);
        }
    }
    #endregion
    #region MovesetFunctions
    //This creates a new moveset for the entity
    public void AddMoveset()
    {
        if (MoveSets.moves == null)
            return;
        for (int i = 0; i < m_nameOfMovesCurrent.Count; i++)
        {
            foreach (var item in MoveSets.moves.m_moveSets)
            {
                m_nameOfMovesCurrent[i].Add(item.name);
                m_nameOfMovesExternal[i].Add(item.name);
                m_learntPerLevel.Add(item.isLearntPerLevel);
                m_learntExternally.Add(item.isLearntExternally);
                levelForeachMoveset.Add(item.levelLearnt);
            }
        }

    }
    //This creates a current moveset for the entity.
    public void AddMoreCurrentMove()
    {
        m_nameOfMovesCurrent.Add(new List<string>());
        currentMoveSetIndex.Add(0);
        if (MoveSets.moves == null)
            return;
        for (int i = 0; i < m_nameOfMovesCurrent.Count; i++)
        {
            foreach (var item in MoveSets.moves.m_moveSets)
            {
                m_nameOfMovesCurrent[i].Add(item.name);
            }
        }
    }
    //This creates a learnable moveset for the entity.
    public void AddNewLearnableMove()
    {

        m_nameOfMovesExternal.Add(new List<string>());
        learnableMoveSetIndex.Add(0);
        if (MoveSets.moves == null)
            return;
        for (int i = 0; i < m_nameOfMovesExternal.Count; i++)
        {
            foreach (var item in MoveSets.moves.m_moveSets)
            {
                m_nameOfMovesExternal[i].Add(item.name);
                m_learntPerLevel.Add(item.isLearntPerLevel);
                m_learntExternally.Add(item.isLearntExternally);
            }
        }
    }
    #endregion
    #region publicFunction
    #region Image
    //Replaces the sprite on the entity with a new one from the deisger.
    public void NewImage(Sprite sprite)
    {
        sprite2D = sprite;
    }
    //Gets the info of the sprite.
    public Sprite GetSpriteInfo()
    {
        if (is2DSprite == true)
        {
            return sprite2D;
        }
        return null;
    }
    //Gets the gameobjects identification.
    public GameObject GetGameObj()
    {
        if (!is2DSprite)
        {
            return entityOBJ;
        }
        return null;
    }
    //Creates a new image for the gameobject.
    public void NewImage(GameObject obj)
    {
        entityOBJ = obj;
    }
    #endregion
    #region Movesets
    //Finds a currentmoveset and if its in the moveset, it is set to true.
    public bool FindCurrentMoveset(string moveName)
    {
        for (int i = 0; i < m_nameOfMovesCurrent.Count; i++)
        {
            if (m_nameOfMovesCurrent[i][currentMoveSetIndex[i]] == moveName)
            {
                return true;
            }
        }
        return false;
    }
    //Gets a currentmoveset and gives the information of the moveset.
    public Moves GetCurrentMoveset(string moveName)
    {
        bool HasBeenFound = false;
        for (int i = 0; i < currentMoves.Count; i++)
        {
            if (moveName == currentMoves[i])
            {
                HasBeenFound = true;
            }
        }
        if (HasBeenFound == true)
        {
            foreach (var move in moveSets)
            {
                if (move.name == moveName)
                {
                    return move;
                }

            }
        }
        return null;
    }
    //Gets all the current movesets on the entity.
    public List<Moves> GetCurrentMovesets()
    {
        List<Moves> moves = new List<Moves>();
        foreach (var item in MoveSets.moves.m_moveSets)
        {
            for (int i = 0; i < currentMoveSetIndex.Count; i++)
            {
                if (item.name == m_nameOfMovesCurrent[i][currentMoveSetIndex[i]])
                    moves.Add(item);
            }

        }
        return moves;
    }
    //Gets all the learnablemovesets on the entity
    public List<Moves> GetLearnableMovesets()
    {
        List<Moves> moves = new List<Moves>();
        foreach (var item in MoveSets.moves.m_moveSets)
        {
            for (int i = 0; i < m_nameOfMovesExternal.Count; i++)
            {
                if (item.name == m_nameOfMovesExternal[i][learnableMoveSetIndex[i]])
                    moves.Add(item);
            }
        }
        return moves;
    }
    //Make functions to quickely access the entities methods.
    #endregion
    #region Items
    //Adds an item to the list of current items on the entity.
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
    //Adds an item to the list of current items on the entity. Incremented by one
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
    //Decreases an item amount to the list of current items on the entity. Decreases by one
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
    //Decreases an item amount to the list of current items on the entity. Decreases by amount
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
    //Adds a new item that is not on the entity.
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
    //Adds a new item that is not on the entity with a amount.
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
    //Gets all items that are on the player.
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
    //Gets the description of the specific item mentioned.
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
    //Gets the item value from the string and returns the value
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
    //Gets the durability and returns the items durability through the items id.
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
    //Loops through all the items on the player that has the name of the string and returns the durability
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
    //Gains experience depending on the level of the enemy, the baseyield of the enemy and the amount of allies in battle.
    public float GainExpMath(int a_levelOfEnemy, float a_baseEXPYieldEnemy, int a_amountOfUnitsInBattleAlly)
    {
        float m_mathOfBattle;
        m_mathOfBattle = ((a_baseEXPYieldEnemy * a_levelOfEnemy) / (5 * a_amountOfUnitsInBattleAlly))
            * ((2 * a_levelOfEnemy + 10) / a_levelOfEnemy + level + 10) + 1;
        return m_mathOfBattle;
    }
    //Gains experience depending on the level of the enemy, the baseyield of the enemy (this is only for one entity).
    public float GainExpMath(int a_levelOfEnemy, float a_baseEXPYieldEnemy)
    {
        float m_mathOfBattle;
        m_mathOfBattle = ((a_baseEXPYieldEnemy * a_levelOfEnemy) / (5))
            * ((2 * a_levelOfEnemy + 10) / a_levelOfEnemy + level + 10) + 1;
        return m_mathOfBattle;
    }
    //Gains experience depending on the level of the enemy, the baseyield of the enemy and the amount of allies in battle and the specific entities level.
    public float GainExpMath(int a_levelOfEnemy, float a_baseEXPYieldEnemy,
        int a_amountOfUnitsInBattleAlly, int mainEntityLevel)
    {
        float m_mathOfBattle;
        m_mathOfBattle = ((a_baseEXPYieldEnemy * a_levelOfEnemy) / (5 * a_amountOfUnitsInBattleAlly))
            * ((2 * a_levelOfEnemy + 10) / a_levelOfEnemy + mainEntityLevel + 10) + 1;
        currentEXP = m_mathOfBattle;
        return m_mathOfBattle;
    }
    //This returns the level, it will increase if the currentexp is higher than the levels power 3.
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
    //Levels up the entity by an amount.
    public void LevelUpEntity(int amount)
    {
        level += amount;
    }
    //This is the minimum exp of the entity for their level
    public double GetMinExp()
    {
        double expForLevel = 0;
        if (level != 1)
            expForLevel = Math.Pow(level - 1, 3);
        return expForLevel;
    }
    //This is the maximum exp for the entity for the next level
    public double GetMaxExpToGet()
    {
        double expForLevel = Math.Pow(level, 3);
        return expForLevel;
    }
    //Returns this entities level, it will increase if the currentexp is higher than the levels power 3.
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
    //Gets a base health of the entity
    public double GetHealth()
    {
        return m_health;
    }
    //Gets the increased health of the entity
    public void IncreaseHealth(double amount)
    {
        m_health += amount;
    }
    //Gets the description of the entity
    public string GetDescription()
    {
        return m_descriptionEntity;
    }
    //Gets the status descprition of the entity
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
    //Gets all the descriptions of thestatus conditions on the entity
    public List<string> GetCurrentStatusDescription()
    {
        foreach (var status in onPlayer)
        {
            onPlayer.Add(status);
        }
        return null;
    }
    //Returns level
    public int GetLevel()
    {
        return level;
    }
    //Returns max level
    public int GetMaxLevel()
    {
        return maxLevel;
    }
    //Returns the baseEXPYield of the entity
    public double GetBaseEXPYield()
    {
        return baseEXPYield;
    }
    //Returns the currentEXP for the entity
    public double CurrentEXP()
    {
        return currentEXP;
    }
    //Returns the max exp for the entity.
    public double MaxEXP()
    {
        return maxEXP;
    }
    #endregion
    #region Stats
    //Gets all the primary stats in the entity
    public List<PrimStatisic> GetPrimStats()
    {
        return m_primStat;
    }
    //Gets all the secondary stats on the entity
    public List<SecStatistic> GetSecStats()
    {
        return m_secStat;
    }
    //Gets a specific primary stat on the entity
    public PrimStatisic GetPrimStats(string primstat)
    {
        foreach (var stat in m_primStat)
        {
            if (stat.name == primstat)
                return stat;
        }
        return null;
    }
    //Gets a specific secondary stat on the entity.
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
    //Adds a new status onto the entity.
    public void AddStatus(Status status)
    {
        if (onPlayer == null)
            onPlayer = new List<Status>();
        onPlayer.Add(status);
    }
    //Deletes a specific status on the entity
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
    //Gets all thet status on the entity
    public List<Status> GetAllStatus()
    {
        return onPlayer;
    }
    //Gets the status on the entity.
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
