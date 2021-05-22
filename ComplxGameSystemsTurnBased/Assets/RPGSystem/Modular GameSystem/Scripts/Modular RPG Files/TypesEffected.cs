using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
[Serializable]
public enum Effectiveness
{
    None,
    superEffective,
    normalEffective,
    notVeryEffective,
    Immune
}
[RequireComponent(typeof(TypeChart))]
[ExecuteInEditMode]
public class TypesEffected : MonoBehaviour
{
    [HideInInspector] public List<List<string>> nameAttack;
    [HideInInspector] public List<List<string>> nameDefense;
    [HideInInspector] public List<Effectiveness> effect;
    [HideInInspector] public List<int> indexValueAtt;
    [HideInInspector] public List<int> indexValueDef;
    public static TypesEffected effected;
    [HideInInspector]public bool hasSavedValues = false;
    private void LateUpdate()
    {
        if (effected != this)
            effected = this;
        if (Application.isPlaying)
        {
            if (nameAttack == null || nameDefense == null)
            {
                LoadValues();
            }
        }
        if (!Application.isPlaying)
        {
            if (hasSavedValues)
            {
                LoadValues();
            }
            else if (nameAttack == null || nameDefense == null)
            {
                nameAttack = new List<List<string>>();
                nameDefense = new List<List<string>>();
                indexValueAtt = new List<int>();
                indexValueDef = new List<int>();
                effect = new List<Effectiveness>();
                for (int i = 0; i < nameAttack.Count; i++)
                {
                    foreach (var item in TypeChart.chart.m_types)
                    {
                        nameAttack[i].Add(item);
                        nameDefense[i].Add(item);
                        indexValueAtt.Add(0);
                        indexValueDef.Add(0);
                        effect.Add(Effectiveness.None);
                    }
                }
            }
            if (nameAttack.Count <= 0 || nameDefense.Count <= 0)
            {
                AddNewTyping();
            }
        }
    }

    public void SaveValues()
    {
        //Saving only one of the arrays as they are all the same but 
        //will be converted to the same number after it is loaded
        PlayerPrefsX.SetStringArray("NameAttack", nameAttack[0].ToArray());
        PlayerPrefsX.SetStringArray("NameDefense", nameDefense[0].ToArray());
        int i = 1;
        foreach (var enumValue in effect)
        {
            PlayerPrefs.SetString("EnumName" + i, enumValue.ToString());
            i++;
        }
        //______________________________________________________________________________
        PlayerPrefsX.SetIntArray("TypeIndexAttack", indexValueAtt.ToArray());
        PlayerPrefsX.SetIntArray("TypeIndexDefense", indexValueDef.ToArray());
        hasSavedValues = true;
        PlayerPrefsX.SetBool("hasSaved", hasSavedValues);
        PlayerPrefs.Save();

    }
    public void LoadValues()
    {
        hasSavedValues = PlayerPrefsX.GetBool("hasSaved");
        indexValueAtt = PlayerPrefsX.GetIntArray("TypeIndexAttack").ToList();
        indexValueDef = PlayerPrefsX.GetIntArray("TypeIndexDefense").ToList();
        if (nameAttack == null)
        {
            nameAttack = new List<List<string>>();
            nameAttack.Add(new List<string>());
        }
        if (nameDefense == null)
        {
            nameDefense = new List<List<string>>();
            nameDefense.Add(new List<string>());
        }
        nameAttack[0] = PlayerPrefsX.GetStringArray("NameAttack").ToList();
        nameDefense[0] = PlayerPrefsX.GetStringArray("NameDefense").ToList();

        if (indexValueAtt.Count != nameAttack.Count || indexValueDef.Count != nameDefense.Count)
        {
            for (int i = 1; i < indexValueAtt.Count; i++)
            {
                nameAttack.Add(PlayerPrefsX.GetStringArray("NameAttack").ToList());
                nameDefense.Add(PlayerPrefsX.GetStringArray("NameDefense").ToList());
            }
        }
        int j = 1;
        effect = new List<Effectiveness>();
        for (int i = 0; i < indexValueAtt.Count; i++)
        {
            effect.Add((Effectiveness)System.Enum.Parse(typeof(Effectiveness), PlayerPrefs.GetString("EnumName" + j)));
            j++;
        }
    }
    public void AddNewTyping()
    {
        nameAttack.Add(new List<string>());
        nameDefense.Add(new List<string>());
        indexValueAtt.Add(0);
        indexValueDef.Add(0);
        effect.Add(Effectiveness.None);
        for (int j = 0; j < nameAttack.Count; j++)
        {
            foreach (var item in TypeChart.chart.m_types)
            {
                nameAttack[j].Add(item);
                nameDefense[j].Add(item);
            }
        }
    }
    public void DeleteTyping()
    {
        nameAttack.RemoveAt(nameAttack.Count - 1);
        nameDefense.RemoveAt(nameDefense.Count - 1);
        indexValueAtt.RemoveAt(indexValueAtt.Count - 1);
        indexValueDef.RemoveAt(indexValueDef.Count - 1);
        effect.RemoveAt(effect.Count - 1);
    }
}
