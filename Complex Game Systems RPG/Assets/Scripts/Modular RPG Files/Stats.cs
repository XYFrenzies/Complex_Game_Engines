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

    public void SetPrimaryStat(PrimStatisic a_primStat, string a_string, bool a_isAPercent, double a_value) 
    {
        PrimStatisic a_temp = a_primStat;
        m_primaryStatistic.Remove(a_primStat);
        a_temp = new PrimStatisic(a_string, a_isAPercent, a_value);
        m_primaryStatistic.Add(a_temp);
    }
    public void SetSecondaryStat(SecStatistic a_sec ,string a_string, bool a_isAPercent, double a_value) 
    {
        SecStatistic a_temp = a_sec;
        m_secondaryStatistic.Remove(a_sec);
        a_temp = new SecStatistic(a_string, a_isAPercent, a_value);
        m_secondaryStatistic.Add(a_temp);
    }

}
