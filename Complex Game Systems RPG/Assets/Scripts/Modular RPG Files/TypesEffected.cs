using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]

[RequireComponent(typeof(TypeChart))]
[ExecuteInEditMode]
public class TypesEffected : MonoBehaviour
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
            if (typeEffective[i].nameAttack.Count != TypeChart.chart.m_types.Count)
            {
                Recreate(i);
                return;
            }

            for (int j = 0; j < typeEffective[i].nameAttack.Count; j++)
            {
                if (typeEffective[i].nameAttack[j] != TypeChart.chart.m_types[j])
                    Recreate(i);
            }
        }
    }
    public void Recreate(int parameter)
    {
        typeEffective[parameter].nameAttack.Clear();
        typeEffective[parameter].nameDefense.Clear();
        typeEffective[parameter].AddNew();
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
