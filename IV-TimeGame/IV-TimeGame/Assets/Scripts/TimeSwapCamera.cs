/*
 * Written by Jonas
 * Sets up the Camera for Time Switch Rendering, so it doesnt have to be done by hand
 * Also refreshes if resolution changes
*/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeSwapCamera : MonoBehaviour
{
    RenderTexture pastTexture;
    RenderTexture presentTexture;

    //RenderTexture pastDepth;
    //RenderTexture presentDepth;

    Camera pastCamera;
    Camera presentCamera;

    public LayerMask pastMask;
    public LayerMask presentMask;

    public Material renderMixer;
    public string pastTextureRefernce = "pastTexture";
    public string presentTextureRefernce = "presentTexture";
    //public string pastDepthRefernce = "pastDepth";
    //public string presentDepthRefernce = "presentDepth";
    public string lerpValueRefernce = "lerpValue";

    Resolution screenRes;
    
    Camera finalCamera;

    Canvas canvas;
    RawImage UIdisplay;
    

    void Awake()
    {
        screenRes = Screen.currentResolution;
        createCameras();
        updateTextures();
    }

    void Start()
    {
        TimeSwapInput ti = GameObject.FindObjectOfType<TimeSwapInput>();
        if (ti)
            //ti.OnTimeToggle += setLerpValue;
            ti.OnSmoothToggle += setLerpValue;
        //TimeSwapInput.OnTimeToggle += setLerpValue;
    }

    void Update()
    {
        if(!resMatches(screenRes, Screen.currentResolution))//resize with game window
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

        //create the buffers
        pastTexture = new RenderTexture(screenRes.width, screenRes.height, 0);
        presentTexture = new RenderTexture(screenRes.width, screenRes.height, 0);

        //pastDepth = new RenderTexture(screenRes.width, screenRes.height,0);
        //presentDepth = new RenderTexture(screenRes.width, screenRes.height,0);

        pastTexture.Create();
        presentTexture.Create();

        //pastDepth.Create();
        //pastDepth.Create();

        //render to them
        pastCamera.targetTexture = pastTexture;
        presentCamera.targetTexture = presentTexture;

        //pastCamera.SetTargetBuffers(pastTexture.colorBuffer, pastDepth.colorBuffer);
        //presentCamera.SetTargetBuffers(presentTexture.colorBuffer, presentDepth.colorBuffer);

        //plug them into the shader
        renderMixer.SetTexture(pastTextureRefernce, pastTexture);
        renderMixer.SetTexture(presentTextureRefernce, presentTexture);

        //renderMixer.SetTexture(pastDepthRefernce, pastDepth);
        //renderMixer.SetTexture(presentDepthRefernce, presentDepth);
    }

    void createCameras()
    {
        //TODO: add more settings
        pastCamera = new GameObject("pastCamera").AddComponent<Camera>();
        presentCamera = new GameObject("presentCamera").AddComponent<Camera>();
        finalCamera = new GameObject("finalCamera").AddComponent<Camera>();

        //set position
        pastCamera.transform.position = transform.position;
        presentCamera.transform.position = transform.position;
        finalCamera.transform.position = transform.position;
        //set rotation
        pastCamera.transform.rotation = transform.rotation;
        presentCamera.transform.rotation = transform.rotation;
        finalCamera.transform.rotation = transform.rotation;

        //set parent
        pastCamera.transform.SetParent(transform);
        presentCamera.transform.SetParent(transform);
        finalCamera.transform.SetParent(transform);

        //make them render to proper taget
        pastCamera.forceIntoRenderTexture = true;
        presentCamera.forceIntoRenderTexture = true;
        presentCamera.forceIntoRenderTexture = false;

        //only render the right stuff
        pastCamera.cullingMask = pastMask;
        presentCamera.cullingMask = presentMask;

        LayerMask finalMask = LayerMask.GetMask("UI");
        finalCamera.cullingMask = finalMask;

        finalCamera.clearFlags = CameraClearFlags.SolidColor;

        //now set the ui so, the final camera can see the mixed effect
        canvas = new GameObject("canvas").AddComponent<Canvas>();
        //make it follow parent
        canvas.transform.position = transform.position;
        canvas.transform.rotation = transform.rotation;
        canvas.transform.SetParent(transform);

        //render UI to final camera
        canvas.renderMode = RenderMode.ScreenSpaceCamera;
        canvas.worldCamera = finalCamera;
        canvas.gameObject.layer = 5;//LayerMask.GetMask("UI");

        //and here is our screen
        UIdisplay = new GameObject("UIdisplay").AddComponent<RawImage>();
        //transform to parent again
        UIdisplay.transform.position = transform.position;
        UIdisplay.transform.rotation = transform.rotation;
        UIdisplay.rectTransform.SetParent(canvas.transform);

        //make full screen
        UIdisplay.rectTransform.localPosition = new Vector3(0, 0,0);
        UIdisplay.rectTransform.sizeDelta = new Vector2(0, 0);
        UIdisplay.rectTransform.anchorMin = new Vector2(0, 0);
        UIdisplay.rectTransform.anchorMax = new Vector2(1, 1);
        UIdisplay.rectTransform.pivot = new Vector2(0.5f, 0.5f);
        UIdisplay.rectTransform.localRotation = Quaternion.Euler(0, 0, 0);
        UIdisplay.rectTransform.localScale = Vector3.one;

        //display the magic
        UIdisplay.material = renderMixer;
        UIdisplay.gameObject.layer = 5;//LayerMask.GetMask("UI");
    }

    void setLerpValue(float state)//TODO: make this change slowly, not instantly
    {
        renderMixer.SetFloat(lerpValueRefernce, state);

        //Diable camera if not in use to boost performance
        presentCamera.gameObject.SetActive(!(state <= 0.001f));
        pastCamera.gameObject.SetActive(!(state >= 0.999f));
    }

    void OnDrawGizmosSelected()
    {
        Camera main = Camera.current;
        Gizmos.DrawFrustum(transform.position, main.fieldOfView, main.farClipPlane, main.nearClipPlane, main.aspect);
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawIcon(transform.position, "d_SceneViewCamera");
    }
}