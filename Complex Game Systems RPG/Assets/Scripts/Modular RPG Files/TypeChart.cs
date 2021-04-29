using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
//This will have the types of effectiveness and how they will effect each other.
[Serializable]
public class TypeVariation
{
    public List<string> m_typeVariation;
    public TypeVariation()
    {
        m_typeVariation = new List<string>();
        m_typeVariation = TypeChart.chart.m_types;
        m_typeVariation.CopyTo(TypeChart.chart.m_types.ToArray(), 0);
    }
}
[ExecuteInEditMode]
public class TypeChart : MonoBehaviour
{
    public List<string> m_types;
    public static TypeChart chart;
    [HideInInspector]public int indexValue = 0;
    void Awake()
    {
        chart = this;    
    }
    void Update()
    {
        chart = this;
        if (!Application.isPlaying && m_types == null)
        {
            m_types = new List<string>();
            m_types.Add("None");
        }
        ////////////////////////////////////
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
    //public void SeperateType(Types a_type)
    //{
    //    Types a_temp = a_type;
    //    m_types.Remove(a_type);
    //    a_temp = new Types();
    //    m_types.Add(a_temp);
    //}
}
