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
using UnityEngine.UIElements;

//[ExecuteInEditMode]
public class VertexPainter : EditorWindow
{
    GameObject selected;
    Color currentColor = Color.white;
    float brushSize = 0.125f;
    float strength = 1.0f;
    
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
            {
                GUILayout.Label("Painting " + selected.name, EditorStyles.boldLabel);

                currentColor = EditorGUILayout.ColorField("Current Color", currentColor);
                brushSize = EditorGUILayout.FloatField("Brush Size", brushSize);
                strength = EditorGUILayout.FloatField("Strength", strength);

                if (GUILayout.Button("Fill"))
                {
                    Color[] colors = new Color[meshFilter.mesh.vertexCount];
                    for (int i = 0; i < colors.Length; i++)
                        colors[i] = currentColor;
                    meshFilter.mesh.colors = colors;
                }


                if (Mouse.current.leftButton.IsPressed())
                {
                    //Debug.Log("Tried to paint");

                    if (selected)
                    {
                        //MeshFilter meshFilter = selected.GetComponent<MeshFilter>();
                        if (meshFilter)
                        {
                            
                            Vector2 mousePos = Event.current.mousePosition;
                            mousePos.y = SceneView.lastActiveSceneView.camera.pixelHeight - mousePos.y;
                            Vector3 position = SceneView.lastActiveSceneView.camera.ScreenPointToRay(mousePos).origin;
                            //Debug.DrawLine(position, position + SceneView.lastActiveSceneView.camera.transform.forward, Color.white, 0.5f);
                            Ray ray = SceneView.lastActiveSceneView.camera.ScreenPointToRay(position);
                            
                            //Raycasting
                            //Ray ray = SceneView.lastActiveSceneView.camera.ScreenPointToRay(Event.current.mousePosition);
                            //Ray ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
                            //Ray ray = HandleUtility.GUIPointToWorldRay(mousePos);
                            RaycastHit hit;

                            Debug.DrawLine(ray.origin, ray.origin + 10.0f * ray.direction, Color.green, 5.0f);

                            if (Physics.Raycast(ray, out hit))
                            {
                                Debug.Log("Raycast: " + hit.collider.gameObject);

                                Debug.DrawLine(ray.origin, ray.origin + 10.0f * hit.point, Color.white, 5.0f);

                                if (hit.collider.gameObject == selected)
                                {
                                    Color[] colors = meshFilter.mesh.colors;
                                    Vector3[] positions = meshFilter.mesh.vertices;

                                    if (colors.Length == 0) colors = new Color[positions.Length];

                                    //Debug.Log("Painted");
                                    for (int i = 0; i < positions.Length; i++)
                                    {
                                        float dis = Vector3.Distance(hit.point, positions[i]);

                                        if (dis <= brushSize)
                                        {
                                            colors[i] = currentColor * strength + colors[i] * (1 - strength);//alpha blending
                                        }
                                    }
                                    meshFilter.mesh.colors = colors;
                                }
                            }
                        }
                    }

                }
            }
        }
    }

    //Make sure the Editor is up to date, even when not focused
    public void OnInspectorUpdate()
    {
        Repaint();
    }
}
