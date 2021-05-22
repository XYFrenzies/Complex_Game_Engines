using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
//This is the type of stat that is applied to the entity.
//This can range from an attack to a defense to a completely different 
//stat that can be compared to other stats.
[Serializable]
public enum TypeOfStat
{
    None,
    Attack,
    Defense,
    Other
}
//This is a class of the primary stats
[Serializable]
public class PrimStatisic
{
    public string name;
    public bool isItAPercent;
    public double stats;//Can be base stat or additive stat
    public int min = int.MinValue;
    public int max = int.MaxValue;
    public bool diminishingReturns = true;
    public TypeOfStat typeOfStat;
    [HideInInspector] public bool showItem = false;
    //This is a default contructor for the primary stats.
    public PrimStatisic() 
    {
        typeOfStat = TypeOfStat.Attack;
        name = "Strength";
        stats = 1;
        isItAPercent = false;
        showItem = false;
    }
    //This is an overloaded constructor the for primary stats
    public PrimStatisic(string a_name, bool a_isItAPercent, double a_stats) 
    {
        name = a_name;
        isItAPercent = a_isItAPercent;
        stats = a_stats;
    }
}
//This is a class of the secondary stats
[Serializable]
public class SecStatistic
{
    public string name;
    public bool isItAPercent;
    public double stats;//This is influenced stat
    public int min = int.MinValue;
    public int max = int.MaxValue;
    public bool diminishingReturns = true;
    public TypeOfStat typeOfStat;
    [HideInInspector] public bool showItem = false;
    //This is a default contructor for the secondary stats.
    public SecStatistic()
    {
        typeOfStat = TypeOfStat.Attack;
        name = "Damage";
        stats = 1;
        isItAPercent = false;
        showItem = false;
    }
    //This is an overloaded constructor the for secondary stats
    public SecStatistic(string a_name, bool a_isItAPercent, double a_stats)
    {
        name = a_name;
        isItAPercent = a_isItAPercent;
        stats = a_stats;
    }
}
//This class creates a stat for the editor to use.
[ExecuteInEditMode]
public class Stats : MonoBehaviour
{
    public static Stats _statsForObjects = null;
    [Tooltip("Default")]
    public List<PrimStatisic> m_primaryStatistic;
    [Tooltip("Default")]
    public List<SecStatistic> m_secondaryStatistic;
    //Defaults
    //Gets and sets the static variable of statsforobjects.
    public static Stats statsForObjects
    {
        get 
        {
            if (_statsForObjects == null)
            {
                _statsForObjects = FindObjectOfType<Stats>();
            }
                return _statsForObjects;
        }
    }
    private void Update()
    {
        //Checks if any of the stats are equal to null. If so create new stats
        if (!Application.isPlaying && m_primaryStatistic == null
            && m_secondaryStatistic == null)
        {
            m_primaryStatistic = new List<PrimStatisic>();
            m_secondaryStatistic = new List<SecStatistic>();
            m_primaryStatistic.Add(new PrimStatisic());
            m_secondaryStatistic.Add(new SecStatistic());
        }
        //Make sure that theres always a 1 stat in the primary and secondary stat
        else
        {
            if (m_primaryStatistic.Count < 1)
            {
                m_primaryStatistic = new List<PrimStatisic>();
                m_primaryStatistic.Add(new PrimStatisic());
            }
            if (m_secondaryStatistic.Count < 1)
            {
                m_secondaryStatistic = new List<SecStatistic>();
                m_secondaryStatistic.Add(new SecStatistic());
                return;
            }

        }
    }
    //Adds a new primary array.
    public void AddToPrimaryArray() 
    {
        m_primaryStatistic.Add(new PrimStatisic());
    }
    //Adds a new secondary array.
    public void AddToSecondaryArray()
    {
        m_secondaryStatistic.Add(new SecStatistic());
    }
}
