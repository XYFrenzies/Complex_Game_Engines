using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//Healing, damage, increased stat, decrease stat (integer or percentage.), cant heal, immune
[Serializable]
public enum HowItEffects 
{
    Healing,
    Damage,
    increaseStat,
    decreaseStat,
    healBlock,
    immunity
}
[Serializable]
public class Status
{
    public string name = "";
    public int valueToChange = 1; //Can be a int or a percent
    public HowItEffects[] effectiveness;
    public bool isAPercentage = false;
}
[ExecuteInEditMode]
public class StatusEffects : MonoBehaviour
{
    [Tooltip("Can be on its own.")]
    //Make functions that interact with the status.
    public Status[] m_statusEffects;
    

    private void Update() 
    {
        if (!Application.isPlaying && m_statusEffects == null)
        {
            m_statusEffects = new Status[2];
            for (int i = 0; i < m_statusEffects.Length; i++)
            {
                m_statusEffects[i] = new Status();
            }
        }
    }

}
