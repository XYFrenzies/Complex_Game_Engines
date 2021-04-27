using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public enum Effectiveness
{
    superEffective,
    normalEffective,
    notVeryEffective,
    Immune
}
[RequireComponent(typeof(TypeChart))]
[ExecuteInEditMode]
public class TypesEffected : TypeChart
{
    public List<string> nameAttack;
    public List<string> nameDefense;
    public Effectiveness effect;
    public int indexValue = 0;
    private void LateUpdate()
    {
        if (!Application.isPlaying && (nameAttack == null || nameDefense == null))
        {
            nameAttack = new List<string>();
            nameDefense = new List<string>();
            nameAttack.CopyTo(chart.m_nameOfType.ToArray(), 0);
            nameDefense.CopyTo(chart.m_nameOfType.ToArray(), 0);
            nameAttack = chart.m_nameOfType;
            nameDefense = chart.m_nameOfType;
        }
        if (nameAttack.Count != chart.m_nameOfType.Count)
        {
            nameAttack.Clear();
            nameDefense.Clear();
            nameAttack.CopyTo(chart.m_nameOfType.ToArray(), 0);
            nameDefense.CopyTo(chart.m_nameOfType.ToArray(), 0);
            nameAttack = chart.m_nameOfType;
            nameDefense = chart.m_nameOfType;
        }
    }
    public void EffectivenessCalc() 
    {
//Returns the value of which the attack is damaging the defender.
    }
    public List<string> GetAttack() { return nameAttack; }
    public List<string> GetDefense() { return nameDefense; }
}
