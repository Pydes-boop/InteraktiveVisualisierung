/*
 * Written by Jonas
 * 
 * This is a Editor Window ment to aid the Level Designer
 * It Allows to filter the Scene view by timeframe
 * 
 *     //TODO: Maybe add a time script, instead of only working with layers
 */

using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[InitializeOnLoadAttribute]
public class TimeSwitchWindow : EditorWindow
{
    enum DisplayState { Past, Present, Both};
    DisplayState displayState = DisplayState.Both;

    [MenuItem("Window/Time Switch")]
    public static void showWindow()
    {
        GetWindow<TimeSwitchWindow>("Time Switch");
    }

    static TimeSwitchWindow()
    {
        EditorApplication.playModeStateChanged += modeChange;
    }

    //Make sure the Editor is up to date, even when not focused
    public void OnInspectorUpdate()
    {
        Repaint();
    }

    void OnGUI()
    {
        //Set / Show currently Visibly Time
        GUILayout.Label("Current Point In Time:", EditorStyles.boldLabel);

        DisplayState last = displayState;
        displayState = (DisplayState) EditorGUILayout.EnumPopup(displayState);

        if (last != displayState)
        {
            setVisibilityForAll();
        }

        //Select All Objects in Time
        if(GUILayout.Button("Select All In This Time"))
        {
            List<GameObject> all = GetAllObjectsOnlyInScene();
            List<GameObject> currentTime = new List<GameObject>();

            foreach(GameObject g in all)
            {
                if (getTime(g) == displayState)
                    currentTime.Add(g);
            }

            Selection.objects = currentTime.ToArray();
        }

        //Show current Objects Time
        GameObject selectedSingle = Selection.activeGameObject;
        if (selectedSingle != null)
        {
            GUILayout.Label(selectedSingle.name + " Is In:", EditorStyles.boldLabel);
            GUILayout.Label("\t" + getTime(selectedSingle));
        }
        else
        {
            GUILayout.Label("No object selected", EditorStyles.boldLabel);
            GUILayout.Label("\t X");
        }

        //Move selected obejcts to time
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

    //Do not interfere with play mode. Refernce: https://docs.unity3d.com/ScriptReference/EditorApplication-playModeStateChanged.html?_ga=2.220954776.1461951138.1609432168-1702603845.1548164925
    public static void modeChange(PlayModeStateChange state)
    {
        if(state == PlayModeStateChange.EnteredPlayMode)
        {
            setAllVisible();
        }
        else if(state == PlayModeStateChange.EnteredEditMode)
        {
            TimeSwitchWindow switcher = Object.FindObjectOfType<TimeSwitchWindow>();
            switcher?.setVisibilityForAll();
        }
    }

    //NOTE: Might want to change this once the time travell machanic is in place
    #region SetAndGetTimeStuff

    void setVisibilityForAll()
    {
        //TODO: narrow it down for performance, maybe use some timeobject script
        List<GameObject> all = GetAllObjectsOnlyInScene();

        foreach (GameObject g in all)
        {
            bool visible = getVisibility(g);
            setVisibility(g, visible);
        }
    }

    static void setAllVisible()
    {
        foreach(GameObject g in GetAllObjectsOnlyInScene())
        {
            setVisibility(g, true);//TODO: maybe there are some objects, that should be disabled? (by the Level Designer)
        }
    }

    bool getVisibility(GameObject g)
    {
        bool visible = 
            getTime(g) == displayState
            || getTime(g) == DisplayState.Both 
            || displayState == DisplayState.Both;
        return visible;
    }

    static void setVisibility(GameObject go, bool visible)
    {
        go.SetActive(visible);
    }

    static DisplayState getTime(GameObject go)
    {
        if (go.layer == 9) return DisplayState.Past;
        else if (go.layer == 10) return DisplayState.Present;
        else return DisplayState.Both;
    }

    static void setTime(GameObject go, DisplayState time)
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

    #endregion

    //Taken straight from the Unity Documentation: https://docs.unity3d.com/ScriptReference/Resources.FindObjectsOfTypeAll.html
    static List<GameObject> GetAllObjectsOnlyInScene()
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
