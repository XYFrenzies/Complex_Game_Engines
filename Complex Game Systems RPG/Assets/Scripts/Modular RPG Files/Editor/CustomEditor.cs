using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Entities))]
[CanEditMultipleObjects]
public class CustomEditorScript : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Entities entity = (Entities)target;
        
    }
}
