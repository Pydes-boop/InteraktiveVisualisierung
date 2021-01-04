using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSwapInput : MonoBehaviour
{
    public KeyCode timeToggleKey = KeyCode.C;

    public delegate void TimeToggle(int state);
    public event TimeToggle OnTimeToggle;

    //State 0 = past; State 1 = present
    private int state = 0;

    void Update()
    {
        if (Input.GetKeyDown(timeToggleKey))
        {
            //change state
            state = state == 0 ? 1 : 0;

            //notify subscribers
            OnTimeToggle?.Invoke(state);
        }
    }

    public int GetTimeState() 
    {
        return this.state;
    }

}
