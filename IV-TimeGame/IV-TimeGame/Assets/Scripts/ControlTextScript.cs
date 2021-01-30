using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControlTextScript : MonoBehaviour
{
    Canvas_Script script;
    // Start is called before the first frame update
    bool isActive = false;
    void Start()
    {
        script = GameObject.Find("InventoryCanvas").GetComponent<Canvas_Script>();
    }
    private void OnTriggerEnter(Collider other)
    {
       // Debug.Log("hello");

        if (other.CompareTag("Player"))
        {
            script.ActivateControlText();
            isActive = true;

        }
    }
    private void Update()
    {
        if (Keyboard.current.leftCtrlKey.wasPressedThisFrame&&isActive)
        {
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.Confined;
            }
            else
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;

            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
      //  Debug.Log("bye");

        if (other.CompareTag("Player"))
        {
            script.DeactivateControlText();
            isActive = false;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

}
