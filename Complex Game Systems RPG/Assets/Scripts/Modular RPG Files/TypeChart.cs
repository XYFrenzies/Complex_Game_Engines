using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
//This will have the types of effectiveness and how they will effect each other.
[ExecuteInEditMode]
public class TypeChart : MonoBehaviour
{
    public List<string> m_nameOfType;
    public static TypeChart chart;
    void Update()
    {
        chart = this;
        if (!Application.isPlaying && m_nameOfType == null)
        {
           m_nameOfType = new List<string>();
           m_nameOfType.Add("None");
        }
        ////////////////////////////////////
        if (m_nameOfType.Count < 1)
        {
            m_nameOfType = new List<string>();
            m_nameOfType.Add("None");
        }
    }
    public List<string> ValueOfArray() 
    {
        return m_nameOfType;
    }
    public void AddType() 
    {
        m_nameOfType.Add("None");
    }
}
