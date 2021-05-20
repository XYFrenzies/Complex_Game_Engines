using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;
//This will have the types of effectiveness and how they will effect each other.

[ExecuteInEditMode]
public class TypeChart : MonoBehaviour
{
    public List<string> m_types;
    public int typeIndex;
    public static TypeChart chart;
    private void Awake()
    {
        chart = this;
    }
    private void OnValidate()
    {
        if (chart != this)
            chart = this;
    }
    void Update()
    {
        if (!Application.isPlaying && m_types == null)
        {
            m_types = new List<string>();
            m_types.Add("None");
            typeIndex = 0;
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
