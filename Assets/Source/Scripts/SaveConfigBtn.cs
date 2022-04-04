using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GameConfig))]
public class customButton : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GameConfig config = (GameConfig) target;
        
        if (GUILayout.Button("Save config"))
        {
            SaveSystem.Save(JsonUtility.ToJson(config.Configuration));
        }
    }

}