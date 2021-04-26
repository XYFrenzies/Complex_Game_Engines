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
[Serializable]
public class TypeEffectiveness
{
    public TypingBasis[] typeAttacking;
    public TypingBasis[] typeDefending;
    public Effectiveness effect;
}
[RequireComponent(typeof(TypeChart))]
[ExecuteInEditMode]
public class TypesEffected : TypeChart
{
    public TypeEffectiveness[] m_typeAttackVsDefend;

    private void LateUpdate()
    {
        if (!Application.isPlaying && m_typeAttackVsDefend == null)
        {
            m_typeAttackVsDefend = new TypeEffectiveness[1];
            for (int i = 0; i < m_typeAttackVsDefend.Length; i++)
            {
                m_typeAttackVsDefend[i] = new TypeEffectiveness();
            }
            
            for (int i = 0; i < m_typeAttackVsDefend.Length; i++)
            {
                NewType(i);
            }
        }
        if (m_typeAttackVsDefend.Length < 1)
        {
            for (int i = 0; i < m_typeAttackVsDefend.Length; i++)
            {
                m_typeAttackVsDefend[i] = new TypeEffectiveness();
                NewType(i);
            }
        }
    }
    void NewType(int Parameter)
    {
        for (int i = 0; i < chart.m_typing.Length; i++)
        {
            m_typeAttackVsDefend[Parameter].typeAttacking = chart.m_typing;
            m_typeAttackVsDefend[Parameter].typeDefending = chart.m_typing;
            m_typeAttackVsDefend[Parameter].typeAttacking.CopyTo(chart.m_typing, 0);
            m_typeAttackVsDefend[Parameter].typeDefending.CopyTo(chart.m_typing, 0);
        }
    }
}
