using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class TypeVariation
{
    public List<string> m_typeVariation;
    public TypeVariation()
    {
        m_typeVariation = new List<string>();
        foreach (var item in TypeChart.chart.m_types)
        {
            m_typeVariation.Add(item);
        }
    }
}

