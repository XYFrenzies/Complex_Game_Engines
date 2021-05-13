using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFunction : ScriptableObject
{
    public string name;
    public ItemID item;
    public virtual void DoSomething() 
    {
        Debug.Log("Fire Base");
    }
}
