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
        //If the types are null, create a new instance of a type.
        if (!Application.isPlaying && m_types == null)
        {
            m_types = new List<string>();
            m_types.Add("None");
            typeIndex = 0;
        }
        //Make sure that there is at least one type in the game.
        if (m_types.Count < 1)
        {
            m_types = new List<string>();
            m_types.Add("None");
        }
    }
    //Gets all the types in the scene.
    public List<string> GetAllTypes() 
    {
        return m_types;
    }
    public void AddType() 
    {
        m_types.Add("None");
    }
}
