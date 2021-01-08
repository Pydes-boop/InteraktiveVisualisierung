/*
 * Written by Jonas
 * 
 * This is a Editor Window ment to aid the Level Designer
 * It Allows to paint vertex colors on meshes
 * 
 * Tutorial: 
 * 1) Open Vertex Painter window
 * 2) Select object you want to paint
 * 3) Set values as you would in any other paint programm
 *      Note: The painter does not keep track of line of sight. It will paint anything within the radius of the 3d cursor
 * 4) Make sure the "Hand Tool" is selected in the standart Unity toolbar
 * 5) Click on the Mesh to paint it
 *      Note: You may get a message "Instantiating mesh due to calling MeshFilter.mesh during edit mode. [...]". No need to worry
 *      Note: It is impossible to paint vertecies that dont exist, so check your mesh density
 * 6) Dont forget to save the scene when you are done
 * 
 * TODO: Might convert from editor window to editor tool in the future, not necessary, but cool - also the right choice of api. oops. Still works though :)
 */

using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

[ExecuteInEditMode]
public class VertexPainter : EditorWindow
{
    GameObject selected;
    Color currentColor = Color.white;
    float brushSize = 0.125f;
    float strength = 1.0f;

    bool drawVertecies = false;
    Vector3 mousePos = new Vector3();

    [MenuItem("Window/Vertex Painter")]
    public static void showWindow()
    {
        GetWindow<VertexPainter>("Vertex Painter");
    }

    void OnGUI()
    {
        //Pick Object
        selected = Selection.activeGameObject;

        //Get Some edge cases out the way
        if (!selected)
            GUILayout.Label("There is no selected gameobject");
        else
        {
            MeshFilter meshFilter = selected.GetComponent<MeshFilter>();
            if (!meshFilter)
                GUILayout.Label(selected.name + " does not have a mesh");
            else
            {
                //Tool customisation

                GUILayout.Label("Painting " + selected.name, EditorStyles.boldLabel);

                currentColor = EditorGUILayout.ColorField("Current Color", currentColor);
                brushSize = EditorGUILayout.FloatField("Brush Size", brushSize);
                strength = Mathf.Clamp(EditorGUILayout.FloatField("Strength", strength), 0.0f, 1.0f);
                drawVertecies = EditorGUILayout.Toggle("Draw Vertecies", drawVertecies);

                //get all the mesh info
                Color[] colors = meshFilter.mesh.colors;
                Vector3[] positions = meshFilter.mesh.vertices;

                if (colors.Length == 0) colors = new Color[positions.Length];//damn edge cases

                //paint every vertex
                if (GUILayout.Button("Fill"))
                {
                    for (int i = 0; i < colors.Length; i++)
                        colors[i] = currentColor;
                    meshFilter.mesh.colors = colors;
                }

                if(drawVertecies)
                {
                    Vector3[] normals = meshFilter.mesh.normals;

                    Matrix4x4 localToWorld = meshFilter.transform.localToWorldMatrix;
                    Matrix4x4 localToWorldNormal = Matrix4x4.Transpose(Matrix4x4.Inverse(localToWorld));//Da hat man ja doch was gelernt letztes semester
                    
                    for (int i = 0; i < positions.Length; i++)
                    {
                        Vector3 worldPos = localToWorld.MultiplyPoint3x4(positions[i]);
                        Vector3 worldNormal = localToWorldNormal.MultiplyPoint3x4(normals[i]).normalized;

                        Debug.DrawLine(worldPos, worldPos + worldNormal * brushSize/2, (colors[i] != null ? colors[i] : Color.black), 0.11f);
                    }
                }

                //paint specific vertecies using the mouse

                //first find mouse Position

                //The code for getting a ray in the scene view: https://stackoverflow.com/questions/58975095/how-to-raycast-from-mouse-position-in-scene-view
                //NOTE: I tried a LOT of approaches, Unity really sucks at giving proper mouse position in scene view
                //Dump out if things are null or otherwise in a problematic state
                if (!(SceneView.lastActiveSceneView.camera == null || SceneView.lastActiveSceneView != mouseOverWindow && Event.current.type == EventType.Repaint))
                {
                    Vector3 mousePosition = Event.current.mousePosition;

                    mousePosition.x += position.x; //add the position of our editor gui window
                    mousePosition.y += position.y;

                    mousePosition.x -= SceneView.lastActiveSceneView.position.x; //subtract off the position of the scene view window
                    mousePosition.y -= SceneView.lastActiveSceneView.position.y;
                    mousePosition.x /= SceneView.lastActiveSceneView.position.width; //scale to size
                    mousePosition.y /= SceneView.lastActiveSceneView.position.height;
                    mousePosition.z = 1; //set Z to a sensible non-zero value so the raycast goes in the right direction
                    mousePosition.y = 1 - mousePosition.y; //invert Y because UIs are top-down and cameras are bottom-up

                    Ray ray = SceneView.lastActiveSceneView.camera.ViewportPointToRay(mousePosition);
                    //My work again

                    RaycastHit hit;

                    if (Physics.Raycast(ray, out hit))
                    {
                        mousePos = hit.point;

                        //Do we really want to paint now?
                        if 
                        (
                            hit.collider.gameObject == selected //Are we even aiming at the selected object?
                            && Mouse.current.leftButton.IsPressed()//Are we trying to paint?
                            && EditorWindow.mouseOverWindow != null
                            && EditorWindow.mouseOverWindow.ToString().Contains("SceneView")//Am I shooting "through" the UI?
                        )
                        {
                            
                            UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(UnityEditor.SceneManagement.EditorSceneManager.GetActiveScene());

                            Matrix4x4 localToWorld = meshFilter.transform.localToWorldMatrix;

                            //loop over every vertex and maybe paint it
                            for (int i = 0; i < positions.Length; i++)
                            {
                                //determine world position and distance to mouse
                                Vector3 worldPos = localToWorld.MultiplyPoint3x4(positions[i]);
                                float dis = Vector3.Distance(hit.point, worldPos);

                                if (dis <= brushSize)
                                {
                                    colors[i] = currentColor * strength + colors[i] * (1 - strength);//alpha blending
                                }
                            }

                            //apply it all
                            meshFilter.mesh.colors = colors;
                        }
                    }
                }

                drawDebugCross(mousePos, brushSize, currentColor, 0.11f);
            }
        }
    }

    //Make sure the Editor is up to date, even when not focused
    public void OnInspectorUpdate()
    {
        Repaint();
    }

    //Using custom scene icon, cause editor windows cant draw gizmps
    void drawDebugCross(Vector3 pos, float s, Color c, float t)
    {
        Debug.DrawLine(pos + Vector3.right * s, pos - Vector3.right * s, c, t);
        Debug.DrawLine(pos + Vector3.up * s, pos - Vector3.up * s, c, t);
        Debug.DrawLine(pos + Vector3.forward * s, pos - Vector3.forward * s, c, t);
    }
}
