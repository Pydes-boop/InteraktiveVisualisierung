using System;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class RightClickZoomSimple : MonoBehaviour
{
    public float FovStart = 50;
    public float FovEnd = 40;
    public float TransitionTime = 10;

    private float _currentFov;
    private float _lerpTime;
    private Camera past;
    private Camera present;
    private Camera final;

    void Start()
    {
        past = GameObject.Find("pastCamera").GetComponent<Camera>();
        present = GameObject.Find("presentCamera").GetComponent<Camera>();
        final = GameObject.Find("finalCamera").GetComponent<Camera>();
    }


    void Update()
    {
        if (Input.GetMouseButton(1))
            ChangeFOV();

    }

    void ChangeFOV()
    {
        if (Math.Abs(_currentFov - FovEnd) > float.Epsilon)
        {
            _lerpTime += Time.deltaTime;
            var t = _lerpTime / TransitionTime;

            t = Mathf.SmoothStep(0, 1, t);

            _currentFov = Mathf.Lerp(FovStart, FovEnd, t);
        }
        else if (Math.Abs(_currentFov - FovEnd) < float.Epsilon)
        {
            _lerpTime = 0;
            Debug.Log("Switch");
            var tmp = FovStart;
            FovStart = FovEnd;
            FovEnd = tmp;
        }

        past.fieldOfView = _currentFov;
        present.fieldOfView = _currentFov;
        final.fieldOfView = _currentFov;
    }
}
