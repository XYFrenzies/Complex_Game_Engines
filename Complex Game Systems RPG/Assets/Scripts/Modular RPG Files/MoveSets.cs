using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public enum Type
{
    None,
    Attack,
    Defend,
    Status
}
[Serializable]
public class Moves
{
    public int itemIndex = 0;
    public string name;
    public double power;
    public bool isItAPercentage;
    [Range(1, 100)]
    public int Accuracy;
    public List<Type> type;
    public bool showItem;
    public List<TypeVariation> m_typeEffectiveness;
    public Moves()
    {
        m_typeEffectiveness = new List<TypeVariation>();
        name = "None";
        type = new List<Type>();
        type.Add(Type.None);
        power = 1;
        isItAPercentage = false;
        Accuracy = 100;
        showItem = false;
        
    }
}
[RequireComponent(typeof(TypeChart))]
[ExecuteInEditMode]
public class MoveSets : MonoBehaviour
{
    public List<Moves> m_moveSets;
    void Update()
    {
        if (!Application.isPlaying && m_moveSets == null)
        {
            m_moveSets = new List<Moves>();
            m_moveSets.Add(new Moves());
            for (int i = 0; i < m_moveSets.Count; i++)
            {
                m_moveSets[i].m_typeEffectiveness.Add(new TypeVariation());
            }
        }
    }
}
