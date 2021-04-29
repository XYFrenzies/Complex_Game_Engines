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
    public string name;
    public double valueToChange; //Can be a int or a percent
    public List<HowItEffects> effectiveness;
    public bool isAPercentage;
    [HideInInspector] public bool showItem;
    public Status()
    {
        name = "Toxic";
        valueToChange = 1;
        effectiveness = new List<HowItEffects>();
        effectiveness.Add(HowItEffects.Damage);
        isAPercentage = false;
        showItem = false;
    }
}
[ExecuteInEditMode]
public class StatusEffects : MonoBehaviour
{
    [Tooltip("Can be on its own.")]
    //Make functions that interact with the status.
    public List<Status> m_statusEffects;
    private void Update()
    {
        if (!Application.isPlaying && m_statusEffects == null)
        {
            m_statusEffects = new List<Status>();
            m_statusEffects.Add(new Status());
        }
        if (m_statusEffects.Count < 1)
        {
            m_statusEffects = new List<Status>();
            m_statusEffects.Add(new Status());
        }
    }

}
