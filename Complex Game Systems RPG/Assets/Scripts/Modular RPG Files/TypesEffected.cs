using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public enum Effectiveness
{
    superEffective,
    normalEffective,
    notVeryEffective,
    Immune
}
[RequireComponent(typeof(TypeChart))]
[ExecuteInEditMode]
public class TypesEffected : MonoBehaviour
{
    public static TypesEffected effected;
    public List<string> nameAttack;
    public List<string> nameDefense;
    public Effectiveness effect;
    public int indexValue = 0;
    private TypeChart type;
    private void Awake()
    {
        type = GetComponent<TypeChart>();
    }
    private void LateUpdate()
    {
        effected = this;
        if (!Application.isPlaying && (nameAttack == null || nameDefense == null))
        {
            nameAttack = new List<string>();
            nameDefense = new List<string>();
            nameAttack = type.m_nameOfType;
            nameDefense = type.m_nameOfType;
            nameAttack.CopyTo(type.m_nameOfType.ToArray(), 0);
            nameDefense.CopyTo(type.m_nameOfType.ToArray(), 0);

        }
        if (nameAttack.Count != type.m_nameOfType.Count)
            Recreate();
        for (int i = 0; i < nameAttack.Count; i++)
        {
            if (nameAttack[i] != type.m_nameOfType[i])
                Recreate();
        }
    }


    public void Recreate()
    {
        nameAttack.Clear();
        nameDefense.Clear();
        nameAttack.CopyTo(type.m_nameOfType.ToArray(), 0);
        nameDefense.CopyTo(type.m_nameOfType.ToArray(), 0);
        nameAttack = type.m_nameOfType;
        nameDefense = type.m_nameOfType;
    }

    public void EffectivenessCalc() 
    {
//Returns the value of which the attack is damaging the defender.
    }
    public List<string> GetAttack() { return nameAttack; }
    public List<string> GetDefense() { return nameDefense; }


}
