using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TimeSwapInput : MonoBehaviour
{
    //Script by Philipp
    //Event system for time shift

    //public KeyCode timeToggleKey = KeyCode.C;
    //public KeyCode smoothTimeToggleKey = KeyCode.T;

    public delegate void TimeToggle(int state);
    public event TimeToggle OnTimeToggle;
    public delegate void SmoothToggle(float state);
    public event SmoothToggle OnSmoothToggle;

    //State 0 = past; State 1 = present
    private int state = 0;
    private float smoothState = 0;
    public float smoothTime = 1f;
    private float smoothStep = 0f;
    private bool smooth = false;

    void Update()
    {
        //no need for two systems
        /*
        if (Keyboard.current.cKey.wasPressedThisFrame)
        {
            //change state
            state = state == 0 ? 1 : 0;
            smoothState = state;
            smooth = false;

            //notify subscribers
            OnTimeToggle?.Invoke(state);
        }
        */
        if (Keyboard.current.tKey.wasPressedThisFrame && !smooth)
        {
            //change state
            smoothStep = 0;
            state = state == 0 ? 1 : 0;
            smooth = true;

            //Also notify fast subscribers on smooth
            OnTimeToggle?.Invoke(state);
        }

        if (smooth) 
        {
            smoothState = Mathf.Lerp(state == 0 ? 1 : 0, state, Mathf.Clamp(smoothStep, 0, 1));

            //notify subscribers
            OnSmoothToggle?.Invoke(smoothState);

            if (smoothStep >= 1) smooth = false;
            smoothStep += (1.0f / smoothTime) * Time.deltaTime;
        }

    }

    public int GetTimeState() 
    {
        return this.state;
    }

}
