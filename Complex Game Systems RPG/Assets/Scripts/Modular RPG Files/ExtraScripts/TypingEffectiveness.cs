using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public enum Effectiveness
{
    None,
    superEffective,
    normalEffective,
    notVeryEffective,
    Immune
}
public class TypingEffectiveness
{
    [HideInInspector] public List<string> nameAttack;
    [HideInInspector] public List<string> nameDefense;
    [HideInInspector] public Effectiveness effect;
    [HideInInspector] public int indexValueAtt = 0;
    [HideInInspector] public int indexValueDef = 0;
    public TypingEffectiveness()
    {
        nameAttack = new List<string>();
        nameDefense = new List<string>();
        effect = Effectiveness.None;
        foreach (var item in TypeChart.chart.m_types)
        {
            nameAttack.Add(item);
            nameDefense.Add(item);
        }
    }
}
