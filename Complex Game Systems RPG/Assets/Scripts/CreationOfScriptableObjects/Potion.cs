using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Items", menuName = "ItemFunction/Potion")]
public class Potion : ItemFunction
{
    public override void DoSomething()
    {
        Debug.Log("Fired do something text function");
    }
}
