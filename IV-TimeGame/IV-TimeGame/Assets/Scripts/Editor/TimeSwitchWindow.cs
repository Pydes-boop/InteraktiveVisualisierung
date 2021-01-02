/*
 * Written by Jonas
 * 
 * This is a Editor Window ment to aid the Level Designer
 * It Allows to filter the Scene view by timeframe
 */ 

using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class TimeSwitchWindow : EditorWindow
{
    enum DisplayState { Past, Present, Both};
    DisplayState displayState = DisplayState.Both;

    [MenuItem("Window/Time Switch")]
    public static void showWindow()
    {
        GetWindow<TimeSwitchWindow>("Time Switch");
    }

    void OnGUI()
    {
        GUILayout.Label("Current Point In Time:", EditorStyles.boldLabel);

        DisplayState last = displayState;
        displayState = (DisplayState) EditorGUILayout.EnumPopup(displayState);

        if (last != displayState)
        {
            //TODO: narrow it down for performance, maybe use some timeobject script
            List<GameObject> all = GetAllObjectsOnlyInScene();

            foreach (GameObject g in all)
            {
                bool visible = getVisibility(g);
                setVisibility(g, visible);
            }
        }

        GUILayout.Label("Selected Object Is In:", EditorStyles.boldLabel);
        GameObject seledctedSingle = Selection.activeGameObject;
        if (seledctedSingle != null)
            GUILayout.Label("\t" + getTime(seledctedSingle));
        else
            GUILayout.Label("\t No object selected");

        GUILayout.Label("Move Selected Objects To:", EditorStyles.boldLabel);

        EditorGUILayout.BeginHorizontal();

        bool past = GUILayout.Button("Past");
        bool present = GUILayout.Button("Present");
        bool both = GUILayout.Button("Both");

        GameObject[] selected = Selection.gameObjects;

        foreach (GameObject g in selected)
        {
            if (past)
                setTime(g, DisplayState.Past);
            else if (present)
                setTime(g, DisplayState.Present);
            else if (both)
                setTime(g, DisplayState.Both);

            if (past || present || both) setVisibility(g, getVisibility(g));
        }

        EditorGUILayout.EndHorizontal();
    }

    bool getVisibility(GameObject g)
    {
        bool visible = 
            getTime(g) == displayState
            || getTime(g) == DisplayState.Both 
            || displayState == DisplayState.Both;
        return visible;
    }

    void setVisibility(GameObject go, bool visible)
    {
        go.SetActive(visible);
    }

    //TODO: Maybe add a time script, instead of only working with layers
    //TODO: Set all to active on game Start()

    DisplayState getTime(GameObject go)
    {
        if (go.layer == 9) return DisplayState.Past;
        else if (go.layer == 10) return DisplayState.Present;
        else return DisplayState.Both;
    }

    void setTime(GameObject go, DisplayState time)
    {
        switch(time)
        {
            case DisplayState.Past:
                go.layer = 9;
                break;
            case DisplayState.Present:
                go.layer = 10;
                break;
            case DisplayState.Both:
                go.layer = 0;
                break;
            default:
                Debug.LogWarning("Time Switch Editor Window could not set Time of " + go + " to " + time);
                break;
        }
    }

    //Taken straight from the Unity Documentation: https://docs.unity3d.com/ScriptReference/Resources.FindObjectsOfTypeAll.html
    List<GameObject> GetAllObjectsOnlyInScene()
    {
        List<GameObject> objectsInScene = new List<GameObject>();

        foreach (GameObject go in Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[])
        {
            if (!EditorUtility.IsPersistent(go.transform.root.gameObject) && !(go.hideFlags == HideFlags.NotEditable || go.hideFlags == HideFlags.HideAndDontSave))
                objectsInScene.Add(go);
        }

        return objectsInScene;
    }
}
