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
    public List<string> nameAttack;
    public List<string> nameDefense;
    public Effectiveness effect;
    [HideInInspector] public int indexValueAtt = 0;
    [HideInInspector] public int indexValueDef = 0;
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
public class TypesEffected : MonoBehaviour
{
    public List<TypingEffectiveness> typeEffective;
    public static TypesEffected effected;
    private void LateUpdate()
    {
        effected = this;
        if (!Application.isPlaying && typeEffective == null)
        {
            typeEffective = new List<TypingEffectiveness>();
            typeEffective.Add(new TypingEffectiveness());

        }
        for (int i = 0; i < typeEffective.Count; i++)
        {
            if (typeEffective[i].nameAttack.Count != TypeChart.chart.m_types.Count)
                Recreate();
            for (int j = 0; j < typeEffective[i].nameAttack.Count; j++)
            {
                if (typeEffective[i].nameAttack[j] != TypeChart.chart.m_types[j])
                    Recreate();
            }
        }
    }
    public void Recreate()
    {
        for (int i = 0; i < typeEffective.Count; i++)
        {
            typeEffective[i].nameAttack.Clear();
            typeEffective[i].nameDefense.Clear();
            typeEffective[i].nameAttack.CopyTo(TypeChart.chart.m_types.ToArray(), 0);
            typeEffective[i].nameDefense.CopyTo(TypeChart.chart.m_types.ToArray(), 0);
            typeEffective[i].nameAttack = TypeChart.chart.m_types;
            typeEffective[i].nameDefense = TypeChart.chart.m_types;
        }

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
