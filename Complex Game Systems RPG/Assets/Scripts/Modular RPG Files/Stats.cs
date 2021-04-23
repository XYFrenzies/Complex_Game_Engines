using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PrimStatisic
{
    public string name;
    public float stats;//Can be base stat or additive stat
    public bool isIntOrPercent;
    public PrimStatisic() 
    {
        name = "Strength";
        stats = 1;
        isIntOrPercent = false;
    }
}
[Serializable]
public class SecStatistic
{
    public string name;
    public float stats;//This is influenced stat
    public bool isIntOrPercent;
    public SecStatistic()
    {
        name = "Damage";
        stats = 1;
        isIntOrPercent = false;
    }
}
[ExecuteInEditMode]
public class Stats : MonoBehaviour
{
    public static Stats statsForObjects;
    [Tooltip("Default")]
    public PrimStatisic[] m_primaryStatistic;
    [Tooltip("Default")]
    public SecStatistic[] m_secondaryStatistic;
    //Defaults
    private void Update()
    {
        statsForObjects = this;
        if (!Application.isPlaying && m_primaryStatistic == null
            && m_secondaryStatistic == null)
        {
            m_primaryStatistic = new PrimStatisic[2];
            m_secondaryStatistic = new SecStatistic[1];

            for (int i = 0; i < m_primaryStatistic.Length; i++)
            {
                m_primaryStatistic[i] = new PrimStatisic();
            }
            for (int i = 0; i < m_secondaryStatistic.Length; i++)
            {
                m_secondaryStatistic[i] = new SecStatistic();
            }
        }
        else
        {
            if (m_primaryStatistic.Length < 1)
            {
                m_primaryStatistic = new PrimStatisic[1];
                m_primaryStatistic[0] = new PrimStatisic();
                m_primaryStatistic[0].name = "Strength";
                m_secondaryStatistic[0].name = "Damage";
            }
        }
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
