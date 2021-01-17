/*
 * Written by Jonas
 * Enables and Disables Selection/Outline Effect by swapping out material.
 * Attempts to use main texture, might not succeed if base material is not setup properly
 * NOTE: Only works with one material per object. If objects with multiple materials are needed, contact me
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class SelectionEffect : MonoBehaviour
{
    Material selectMat;
    Material baseMat;

    new Renderer renderer;

    //Only used for debugging. Do not enable this in a proper build!
    /*
    [SerializeField] bool enabled = false;

    void Update()
    {
        if (enabled) enableEffect();
        else disableEffect();
    }
    */
    void Awake()
    {
        selectMat = Resources.Load<Material>("Materials/Selected");
        renderer = GetComponent<Renderer>();

        if(selectMat == null) Debug.LogWarning(gameObject.name + " couldnt find the selction material in the Resources Folder");
        if(renderer == null) Debug.LogWarning(gameObject.name + " couldnt setup the selection effect, because it doesnt have a renderer component");

        baseMat = renderer.material;
    }

    public void enableEffect()
    {
        baseMat = renderer.material;//should be redunant, but theoretically some other script might change the material aswell
        if (selectMat != null)
        {
            renderer.material = selectMat;
            
            if(baseMat.mainTexture != null)
                renderer.material.mainTexture = baseMat.mainTexture;
        }
        else
            Debug.LogWarning("Couldnt enable Selection effect on " + gameObject.name + ", becasue it couldnt find the material in the Resources Folder");
    }

    public void disableEffect()
    {
        if (baseMat != null)
            renderer.material = baseMat;
        else
            Debug.LogWarning("Couldnt diable Selection effect on " + gameObject.name + ", becasue it couldnt find the base material");
    }
}
