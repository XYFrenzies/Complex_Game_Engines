using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PrimStatisic
{
    public string name;
    public bool isItAPercent;
    public double stats;//Can be base stat or additive stat
    public int min = int.MinValue;
    public int max = int.MaxValue;
    public bool diminishingReturns = true;
    [HideInInspector] public bool showItem = false;

    public PrimStatisic() 
    {
        name = "Strength";
        stats = 1;
        isItAPercent = false;
        showItem = false;
    }
    public PrimStatisic(string a_name, bool a_isItAPercent, double a_stats) 
    {
        name = a_name;
        isItAPercent = a_isItAPercent;
        stats = a_stats;
    }
}
[Serializable]
public class SecStatistic
{
    public string name;
    public bool isItAPercent;
    public double stats;//This is influenced stat
    public int min = int.MinValue;
    public int max = int.MaxValue;
    public bool diminishingReturns = true;
    [HideInInspector] public bool showItem = false;
    public SecStatistic()
    {
        name = "Damage";
        stats = 1;
        isItAPercent = false;
        showItem = false;
    }
    public SecStatistic(string a_name, bool a_isItAPercent, double a_stats)
    {
        name = a_name;
        isItAPercent = a_isItAPercent;
        stats = a_stats;
    }
}
[ExecuteInEditMode]
public class Stats : MonoBehaviour
{
    //public static Stats statsForObjects;
    public static Stats _statsForObjects = null;
    [Tooltip("Default")]
    public List<PrimStatisic> m_primaryStatistic;
    [Tooltip("Default")]
    public List<SecStatistic> m_secondaryStatistic;
    //Defaults
    //private void OnValidate()
    //{
    //    statsForObjects = this;
    //}
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
        //statsForObjects = this;
        if (!Application.isPlaying && m_primaryStatistic == null
            && m_secondaryStatistic == null)
        {
            m_primaryStatistic = new List<PrimStatisic>();
            m_secondaryStatistic = new List<SecStatistic>();
            m_primaryStatistic.Add(new PrimStatisic());
            m_secondaryStatistic.Add(new SecStatistic());
        }
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
    public void AddToPrimaryArray() 
    {
        m_primaryStatistic.Add(new PrimStatisic());
    }
    public void AddToSecondaryArray()
    {
        m_secondaryStatistic.Add(new SecStatistic());
    }
}
