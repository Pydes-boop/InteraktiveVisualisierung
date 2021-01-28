using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(CapsuleCollider))]
public class LadderEnd : MonoBehaviour
{
    private Canvas_Script script;

    // Start is called before the first frame update
    void Start()
    {
       script= GameObject.Find("InventoryCanvas").GetComponent<Canvas_Script>();
    }


    private void OnTriggerEnter(Collider other)
    {

     
        if (other.CompareTag("Player"))
        {
            script.ActivateClimbingText();

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            script.DeActivateClimbingText();
        }
    }
}
