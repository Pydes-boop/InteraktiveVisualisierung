/*
 * Written by Jonas
 * 
 * This is a Editor Window ment to aid the Level Designer
 * It Allows to paint vertex colors on meshes
 * 
 *     //TODO: Maybe add a time script, instead of only working with layers
 */

using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[ExecuteInEditMode]
public class VertexPainter : EditorWindow
{
    GameObject selected;
    Color currentColor = Color.white;
    
    [MenuItem("Window/Vertex Painter")]
    public static void showWindow()
    {
        GetWindow<VertexPainter>("Vertex Painter");
    }

    void OnGUI()
    {
        selected = Selection.activeGameObject;
        if (!selected)
            GUILayout.Label("There is no selected gameobject");
        else
        {
            MeshFilter meshFilter = selected.GetComponent<MeshFilter>();
            if (!meshFilter)
                GUILayout.Label(selected.name + " does not have a mesh");
            else
                GUILayout.Label("Painting " + selected.name, EditorStyles.boldLabel);

            currentColor = EditorGUILayout.ColorField("Current Color", currentColor);

            if(GUILayout.Button("Fill"))
            {
                Debug.Log("Tried to fill");
            }
        }
    }

    void Update()
    {
        /*
        if(Mouse.current.leftButton.IsPressed()) 
        {
            Debug.Log("Tried to paint");
            
            if (selected)
            {
                MeshFilter meshFilter = selected.GetComponent<MeshFilter>();
                if (meshFilter)
                {
                    //Raycasting
                    //Ray ray = SceneView.lastActiveSceneView.camera.ScreenPointToRay(Event.current.mousePosition);
                    //Ray ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
                    Ray ray = HandleUtility.GUIPointToWorldRay(mousePos);
                    RaycastHit hit;

                    if (Physics.Raycast(ray, out hit))
                    {
                        if (hit.collider.gameObject == selected)
                        {
                            Debug.Log("Tried to paint");
                        }
                    }
                }
            }
            
        }
        */
    }
}
