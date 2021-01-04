/*
 * Written by Jonas
 * Sets up the Camera for Time Switch Rendering, so it doesnt have to be done by hand
 * Also refreshes if resolution changes
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSwapCamera : MonoBehaviour
{
    RenderTexture pastTexture;
    RenderTexture presentTexture;

    Camera pastCamera;
    Camera presentCamera;

    public LayerMask pastMask;
    public LayerMask presentMask;

    public Material renderMixer;
    public string pastTextureRefernce = "pastTexture";
    public string presentTextureRefernce = "presentTexture";

    Resolution screenRes;

    void Awake()
    {
        screenRes = Screen.currentResolution;
        createCameras();
        updateTextures();
    }

    void Update()
    {
        if(!resMatches(screenRes, Screen.currentResolution))
        {
            updateTextures();
        }
    }

    bool resMatches(Resolution a, Resolution b)
    {
        return a.width == b.width && a.height == b.height;
    }

    void updateTextures()
    {
        //TODO: maybe add depth channel
        pastTexture = new RenderTexture(screenRes.width, screenRes.height, 16);
        presentTexture = new RenderTexture(screenRes.width, screenRes.height, 16);

        pastTexture.Create();
        presentTexture.Create();


        pastCamera.targetTexture = pastTexture;
        presentCamera.targetTexture = presentTexture;

        renderMixer.SetTexture(pastTextureRefernce, pastTexture);
        renderMixer.SetTexture(presentTextureRefernce, presentTexture);
    }

    void createCameras()
    {
        //TODO: add more settings
        pastCamera = new GameObject("pastCamera").AddComponent<Camera>();
        presentCamera = new GameObject("presentCamera").AddComponent<Camera>();

        pastCamera.transform.position = transform.position;
        presentCamera.transform.position = transform.position;

        pastCamera.transform.rotation = transform.rotation;
        presentCamera.transform.rotation = transform.rotation;


        pastCamera.transform.SetParent(transform);
        presentCamera.transform.SetParent(transform);

        pastCamera.forceIntoRenderTexture = true;
        presentCamera.forceIntoRenderTexture = true;

        pastCamera.cullingMask = pastMask;
        presentCamera.cullingMask = presentMask;
    }

    void OnDrawGizmosSelected()
    {
        Camera main = Camera.current;
        Gizmos.DrawFrustum(transform.position, main.fieldOfView, main.farClipPlane, main.nearClipPlane, main.aspect);
    }
}
