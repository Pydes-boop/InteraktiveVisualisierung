using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSwapInput : MonoBehaviour
{
    public KeyCode timeToggleKey = KeyCode.C;

    //State 0 = past; State 1 = present
    private int state = 0;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(timeToggleKey))
        {
            //toggles between states
            state = state == 0 ? 1 : 0;
        }
    }

    public int getTimeState() 
    {
        return this.state;
    }

}
