using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

