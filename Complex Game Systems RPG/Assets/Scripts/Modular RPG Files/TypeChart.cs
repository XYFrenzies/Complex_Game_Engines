using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
//This will have the types of effectiveness and how they will effect each other.
[Serializable]
public class TypingBasis
{
    public string name = "None";
}
[ExecuteInEditMode]
public class TypeChart : MonoBehaviour
{
    public static TypeChart chart;
    public TypingBasis[] m_typing;

    void Update()
    {
        chart = this;
        if (!Application.isPlaying && m_typing == null)
        {
            m_typing = new TypingBasis[1];
            for (int i = 0; i < m_typing.Length; i++)
            {
                m_typing[i] = new TypingBasis();
            }

        }
        ////////////////////////////////////
        else if (m_typing.Length < 1)
        {
            m_typing = new TypingBasis[1];
            for (int i = 0; i < m_typing.Length; i++)
            {
                m_typing[i] = new TypingBasis();
                m_typing[i].name = "None";
            }
        }
    }

    public void EffectiveStatus(TypeEffectiveness t1Attack, TypeEffectiveness t2Defend, Effectiveness effect)
    {

    }
}
