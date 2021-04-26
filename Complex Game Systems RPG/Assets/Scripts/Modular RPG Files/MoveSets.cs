using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public enum Type
{
    Attack,
    Defend,
    Status
}
[Serializable]
public class Moves 
{
    public string name = "";
    public int stat = 1;
    public bool isItAPercentage = false;
    [Range(1, 100)]
    public int Accuracy = 100;
    public Type type;
}
[ExecuteInEditMode]
public class MoveSets : MonoBehaviour
{
    public Moves[] m_moveSets;
    void Update()
    {
        if (!Application.isPlaying && m_moveSets == null)
        {
            m_moveSets = new Moves[1];
            for (int i = 0; i < m_moveSets.Length; i++)
            {
                m_moveSets[i] = new Moves();
            }
        }
    }
}
