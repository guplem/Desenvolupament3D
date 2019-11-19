using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ToolRenaming : EditorWindow
{
    private string newName;
    private int objectNumber = 0;
    
    [MenuItem("PENE/Rename")]
    public static void ShowWindow()
    {
        GetWindow<ToolRenaming>("RENAMER");
    }
    private void OnGUI()
    {
        GUILayout.Label("Rename the selected objects", EditorStyles.boldLabel);
        newName = EditorGUILayout.TextField("New name", newName);

        if (GUILayout.Button("Rename and numerate"))
        {
            objectNumber = 0;
            
            foreach (GameObject go in Selection.gameObjects)
            {
                objectNumber++;
                go.name = newName + " - " + objectNumber;
            }
        }
        
        if (GUILayout.Button("Rename"))
        {
            foreach (GameObject go in Selection.gameObjects)
                go.name = newName;
        }
    }
}