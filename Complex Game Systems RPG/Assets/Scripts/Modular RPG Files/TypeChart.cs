using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
//This will have the types of effectiveness and how they will effect each other.
[Serializable]
public class TypeVariation : TypeChart
{
    public List<string> m_typeVariation;
    public TypeVariation()
    {
        m_typeVariation = new List<string>();
        m_typeVariation = chart.m_types;
        m_typeVariation.CopyTo(chart.m_types.ToArray(), 0);

    }
}
[ExecuteInEditMode]
public class TypeChart : MonoBehaviour
{
    public List<string> m_types;
    public static TypeChart chart;
    void Update()
    {
        if (chart != this)
            chart = this;
        if (!Application.isPlaying && m_types == null)
        {
            m_types = new List<string>();
            m_types.Add("None");
        }
        if (m_types.Count < 1)
        {
            m_types = new List<string>();
            m_types.Add("None");
        }
    }
    public List<string> ValueOfArray() 
    {
        return m_types;
    }
    public void AddType() 
    {
        m_types.Add("None");
    }
}
