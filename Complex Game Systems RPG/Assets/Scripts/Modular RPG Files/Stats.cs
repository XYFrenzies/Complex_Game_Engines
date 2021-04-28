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
    [HideInInspector] public bool showItem = false;

    public PrimStatisic() 
    {
        name = "Strength";
        stats = 1;
        isItAPercent = false;
        showItem = false;
    }
}
[Serializable]
public class SecStatistic
{
    public string name;
    public bool isItAPercent;
    public double stats;//This is influenced stat
    [HideInInspector] public bool showItem = false;
    public SecStatistic()
    {
        name = "Damage";
        stats = 1;
        isItAPercent = false;
        showItem = false;
    }
}
[ExecuteInEditMode]
public class Stats : MonoBehaviour
{
    public static Stats statsForObjects;
    [Tooltip("Default")]
    public List<PrimStatisic> m_primaryStatistic;
    [Tooltip("Default")]
    public List<SecStatistic> m_secondaryStatistic;
    //Defaults
    private void Update()
    {
        statsForObjects = this;
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
    //Function to add primary stat with secondary stat
    public void AddPrimWithSecStats(float a_primStat, float a_secStat) 
    {
        //In this either make another function that adds the percentage by the integer or in this function
    }
    //Function to add secondary stat with secondary stat
    public void AddSecWithSecStats(float a_secStatA, float a_secStatB)
    {
        //In this either make another function that adds the percentage by the integer or in this function
    }
}
