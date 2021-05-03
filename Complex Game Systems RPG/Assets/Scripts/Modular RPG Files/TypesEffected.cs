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
[Serializable]
public class TypingEffectiveness
{
    [HideInInspector]public List<string> nameAttack;
    [HideInInspector]public List<string> nameDefense;
    [HideInInspector]public Effectiveness effect;
    [HideInInspector]public int indexValueAtt = 0;
    [HideInInspector]public int indexValueDef = 0;
    public TypingEffectiveness()
    {
        nameAttack = new List<string>();
        nameDefense = new List<string>();
        effect = Effectiveness.None;
        nameAttack = TypeChart.chart.m_types;
        nameDefense = TypeChart.chart.m_types;
        nameAttack.CopyTo(TypeChart.chart.m_types.ToArray(), 0);
        nameDefense.CopyTo(TypeChart.chart.m_types.ToArray(), 0);
    }
}
[RequireComponent(typeof(TypeChart))]
[ExecuteInEditMode]
public class TypesEffected : TypeChart
{
    public List<TypingEffectiveness> typeEffective;
    public static TypesEffected effected;
    private void LateUpdate()
    {
        if (effected != this)
            effected = this;
        if (!Application.isPlaying && typeEffective == null)
        {
            typeEffective = new List<TypingEffectiveness>();
            typeEffective.Add(new TypingEffectiveness());

        }
        for (int i = 0; i < typeEffective.Count; i++)
        {
            if (typeEffective[i] == null)
            {
                typeEffective = new List<TypingEffectiveness>();
                typeEffective.Add(new TypingEffectiveness());
                Recreate(i);
            }
            if (typeEffective[i].nameAttack.Count != chart.m_types.Count)
            {
                Recreate(i);
                return;
            }

            for (int j = 0; j < typeEffective[i].nameAttack.Count; j++)
            {
                if (typeEffective[i].nameAttack[j] != chart.m_types[j])
                    Recreate(i);
            }
        }
    }
    public void Recreate(int parameter)
    {
        typeEffective[parameter].nameAttack.Clear();
        typeEffective[parameter].nameDefense.Clear();
        typeEffective[parameter].nameAttack.CopyTo(chart.m_types.ToArray(), 0);
        typeEffective[parameter].nameDefense.CopyTo(chart.m_types.ToArray(), 0);
        typeEffective[parameter].nameAttack = chart.m_types;
        typeEffective[parameter].nameDefense = chart.m_types;
    }
    public void EffectivenessCalc()
    {
        //Returns the value of which the attack is damaging the defender.
    }
    public void AddNewTypeEffect()
    {
        typeEffective.Add(new TypingEffectiveness());
    }
}
