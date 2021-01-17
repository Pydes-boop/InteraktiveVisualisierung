using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class PostProcessingSwitch : MonoBehaviour
{
    //Script by Philipp
    //To toggle Effects for the time switch

    public Volume volume;

    public AnimationCurve curve;


    private void Awake()
    {

        TimeSwapInput switchInput = GameObject.FindObjectOfType<TimeSwapInput>();

        if (switchInput)
            switchInput.OnSmoothToggle += switchEffects;

    }

    public void switchEffects(float state) 
    {
        //getting the value of the curve at the current animation time(=state)
        volume.weight = curve.Evaluate(state);
    }
}
