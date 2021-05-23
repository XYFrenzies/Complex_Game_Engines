using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
//This is a stats effectiveness between each other in order to give the 
//designer an idea of whats effecting what.
//Monobehaviour class of the stats against each other.
[Serializable]
[ExecuteInEditMode]
[RequireComponent(typeof(Stats))]
public class StatsTypeAgainst : MonoBehaviour
{
    public List<int> indexAttack;
    public List<int> indexDefense;
    public List<int> indexOtherFor;
    public List<int> indexOtherAgainst;
    public List<bool> showItem;
    public List<List<string>> m_attack;
    public List<List<string>> m_defense;
    public List<List<string>> m_other;
    [HideInInspector] public bool hasSavedValues = false;
    public List<bool> isOtherActive;
    public static StatsTypeAgainst _instance;
    public static StatsTypeAgainst instance
    {
        get 
        {
            if (_instance == null)
                _instance = FindObjectOfType<StatsTypeAgainst>();
            return _instance;
        }
    }

    void Update()
    {
        if (Application.isPlaying)
        {
            _instance = this;
            if (m_attack == null || m_defense == null || m_other == null)
            {
                LoadValues();
            }
        }
        if (!Application.isPlaying)
        {
            _instance = this;
            //If the stats type against each is null then it will create a new one.
            //Goes through a loop of all the stats and checks if they exist or not, if they do, then ignore otherwise make new one
            if (hasSavedValues)
            {
                LoadValues();
            }
            else if (m_attack == null || m_defense == null || m_other == null)
            {
                showItem = new List<bool>();
                isOtherActive = new List<bool>();
                m_attack = new List<List<string>>();
                m_defense = new List<List<string>>();
                m_other = new List<List<string>>();
                indexAttack = new List<int>();
                indexDefense = new List<int>();
                indexOtherFor = new List<int>();
                indexOtherAgainst = new List<int>();
                m_attack.Add(new List<string>());
                m_defense.Add(new List<string>());
                m_other.Add(new List<string>());
                AddNewStats(m_attack[0], TypeOfStat.Attack);
                AddNewStats(m_defense[0], TypeOfStat.Defense);
                AddNewStats(m_other[0], TypeOfStat.Other);
                indexAttack.Add(0);
                indexDefense.Add(0);
                indexOtherFor.Add(0);
                indexOtherAgainst.Add(0);
                isOtherActive.Add(false);
                showItem.Add(false);

            }
        }
    }
    public void SaveValues()
    {
        //Saving only one of the arrays as they are all the same but 
        //will be converted to the same number after it is loaded
        PlayerPrefsX.SetStringArray("NameAttackType", m_attack[0].ToArray());
        PlayerPrefsX.SetStringArray("NameDefenseType", m_defense[0].ToArray());
        PlayerPrefsX.SetStringArray("NameOtherType", m_other[0].ToArray());
        //______________________________________________________________________________
        PlayerPrefsX.SetIntArray("StatsIndexAttack2", indexAttack.ToArray());
        PlayerPrefsX.SetIntArray("StatsIndexDefense2", indexDefense.ToArray());
        PlayerPrefsX.SetIntArray("StatsIndexOther2", indexOtherFor.ToArray());
        PlayerPrefsX.SetIntArray("StatsIndexOtherAgainst2", indexOtherAgainst.ToArray());
        PlayerPrefsX.SetBoolArray("IsOtherActive", isOtherActive.ToArray());
        PlayerPrefsX.SetBoolArray("showItem", showItem.ToArray());
        hasSavedValues = true;
        PlayerPrefsX.SetBool("hasSavedStat", hasSavedValues);
        PlayerPrefs.Save();
    }
    public void LoadValues()
    {
        isOtherActive = PlayerPrefsX.GetBoolArray("IsOtherActive").ToList();
        hasSavedValues = PlayerPrefsX.GetBool("hasSavedStat");
        indexAttack = PlayerPrefsX.GetIntArray("StatsIndexAttack2").ToList();
        indexDefense = PlayerPrefsX.GetIntArray("StatsIndexDefense2").ToList();
        indexOtherFor = PlayerPrefsX.GetIntArray("StatsIndexOther2").ToList();
        indexOtherAgainst = PlayerPrefsX.GetIntArray("StatsIndexOtherAgainst2").ToList();
        showItem = PlayerPrefsX.GetBoolArray("showItem").ToList();
        if (m_attack == null)
        {
            m_attack = new List<List<string>>();
            m_attack.Add(new List<string>());
        }
        if (m_defense == null)
        {
            m_defense = new List<List<string>>();
            m_defense.Add(new List<string>());
        }
        if (m_other == null)
        {
            m_other = new List<List<string>>();
            m_other.Add(new List<string>());
        }
        m_attack[0] = PlayerPrefsX.GetStringArray("NameAttackType").ToList();
        m_defense[0] = PlayerPrefsX.GetStringArray("NameDefenseType").ToList();
        m_other[0] = PlayerPrefsX.GetStringArray("NameOtherType").ToList();

        if (indexAttack.Count != m_attack.Count)
        {
            for (int i = 1; i < indexAttack.Count; i++)
            {
                m_attack.Add(PlayerPrefsX.GetStringArray("NameAttackType").ToList());

            }
        }
        if (indexDefense.Count != m_defense.Count)
        {
            for (int i = 0; i < indexDefense.Count; i++)
            {
                m_defense.Add(PlayerPrefsX.GetStringArray("NameDefenseType").ToList());
            }
        }
        if (indexOtherFor.Count != m_other.Count)
        {
            for (int i = 0; i < indexOtherFor.Count; i++)
            {
                m_other.Add(PlayerPrefsX.GetStringArray("NameOtherType").ToList());
            }
        }
    }
    //This adds a new stat to the list of statsagainst
    public void AddNewStats(List<string> type, TypeOfStat typeofStat)
    {
        foreach (var item in Stats.statsForObjects.m_primaryStatistic)
        {
            if (item.typeOfStat == typeofStat)
                type.Add(item.name);

        }
        foreach (var item in Stats.statsForObjects.m_secondaryStatistic)
        {
            if (item.typeOfStat == typeofStat)
                type.Add(item.name);
        }
    }
    //Adds the stats if a new stat against is to be added to the project.
    public void AddStats()
    {
        isOtherActive.Add(false);
        m_attack.Add(new List<string>());
        m_defense.Add(new List<string>());
        m_other.Add(new List<string>());
        indexAttack.Add(0);
        indexDefense.Add(0);
        indexOtherFor.Add(0);
        indexOtherAgainst.Add(0);
        showItem.Add(false);
        for (int j = 0; j < m_attack.Count; j++)
        {
            //Goes through a loop of all the primary stats in the stats script
            foreach (var item in Stats.statsForObjects.m_primaryStatistic)
            {
                if (item.typeOfStat == TypeOfStat.Attack)
                    m_attack[j].Add(item.name);
                else if (item.typeOfStat == TypeOfStat.Defense)
                    m_defense[j].Add(item.name);
                else if (item.typeOfStat == TypeOfStat.Other)
                    m_other[j].Add(item.name);
            }
            //Goes through a loop of all the secondary stats in the stats script
            foreach (var item in Stats.statsForObjects.m_secondaryStatistic)
            {
                if (item.typeOfStat == TypeOfStat.Attack)
                    m_attack[j].Add(item.name);
                else if (item.typeOfStat == TypeOfStat.Defense)
                    m_defense[j].Add(item.name);
                else if (item.typeOfStat == TypeOfStat.Other)
                    m_other[j].Add(item.name);
            }
        }
    }
    public void RemoveRecent() 
    {
        m_attack.RemoveAt(m_attack.Count - 1);
        m_defense.RemoveAt(m_defense.Count - 1);
        m_other.RemoveAt(m_other.Count - 1);
        indexAttack.RemoveAt(indexAttack.Count - 1);
        indexDefense.RemoveAt(indexDefense.Count - 1);
        indexOtherFor.RemoveAt(indexOtherFor.Count - 1);
        indexOtherAgainst.RemoveAt(indexOtherAgainst.Count - 1);
        showItem.RemoveAt(showItem.Count - 1);
    }
}
