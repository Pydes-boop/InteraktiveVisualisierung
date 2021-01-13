using System;
using UnityEngine;
public class RightClickZoomSimple : MonoBehaviour
{
    [Tooltip("ZoomIn/Out = currentFov +/- Zoom")]
    public float Zoom = 10;
    [Tooltip("Currently unused")]
    public float smooth = 1;
    [Tooltip("ZoomIn/Out on MouseClick/MouseHold")]
    public bool MouseClick = true;

    private float _currentFov;
    private bool zoomedIn;
    private Camera past;
    private Camera present;
    private Camera final;

    void Start()
    {
        zoomedIn = false;
        past = GameObject.Find("pastCamera").GetComponent<Camera>();
        present = GameObject.Find("presentCamera").GetComponent<Camera>();
        final = GameObject.Find("finalCamera").GetComponent<Camera>();
        _currentFov = past.fieldOfView;
    }


    void Update()
    {
        if (MouseClick)
        {
            if (Input.GetMouseButtonDown(1))
                ChangeFOV();
        }
        else
        {
            if (Input.GetMouseButtonDown(1))
                ChangeFOV();
            if (Input.GetMouseButtonUp(1))
                ChangeFOV();
        }

    }

    void ChangeFOV()
    {
        //TODO: Smooth out Transition with Lerp and Time.DeltaTime
        if (!zoomedIn)
        {
            _currentFov -= Zoom;
            zoomedIn = true;
        }
        else
        {
            _currentFov += Zoom;
            zoomedIn = false;

        }

        past.fieldOfView = _currentFov;
        present.fieldOfView = _currentFov;
        final.fieldOfView = _currentFov;
    }
}
