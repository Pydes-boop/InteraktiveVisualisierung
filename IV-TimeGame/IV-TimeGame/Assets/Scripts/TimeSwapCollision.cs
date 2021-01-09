using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSwapCollision : MonoBehaviour
{
    //Script by Philipp
    //This script switches the characters layer to disable collision with the unseen objects in the sceen

    public int past, present;
    public int currentState = 0;

    private void Awake()
    {
        TimeSwapInput ti = GameObject.FindObjectOfType<TimeSwapInput>();
        if (ti)
            ti.OnTimeToggle += onTimeChange;
        //TimeSwapInput.OnTimeToggle += onTimeChange;
    }

    void onTimeChange(int state)
    {
        gameObject.layer = (state == 0 ? present : past);
    }
}
