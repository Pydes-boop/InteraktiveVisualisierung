using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BoardClick : MonoBehaviour
{

    GameObject lastClicked = null;

    Vector2 MousePosNow = new Vector2(0, 0);
    Vector2 MousePosPrev = new Vector2(0, 0);

    void Update()
    {
        MousePosPrev = MousePosNow;
        MousePosNow = Mouse.current.position.ReadValue();
        if (Mouse.current.leftButton.wasPressedThisFrame) 
        {
            lastClicked = getClickedObject();
        }

        if (Mouse.current.leftButton.isPressed && lastClicked != null) 
        {
            Vector2 dif = MousePosNow - MousePosPrev;
            Vector3 pointTo = getImpectPoint();
            Debug.Log(pointTo);
            if (pointTo.x != -1) 
            {
                lastClicked.transform.position = pointTo;
            }
        }

        if (Mouse.current.leftButton.wasReleasedThisFrame) 
        {
            lastClicked = null;
        }
    }

    public GameObject getClickedObject() 
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, 100))
        {
            return (hit.transform.gameObject.tag.Contains("Dragable") ? hit.transform.gameObject : null);
        }
        else
        {
            return null;
        }
    }

    public Vector3 getImpectPoint()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits;
        hits = Physics.RaycastAll(ray, 100);

        for (int i = 0; i < hits.Length; i++) 
        {

            if (hits[i].transform.gameObject.tag.Contains("InfoBord")) 
            {
                return hits[i].point;
            }

        }
        return new Vector3(-1,-1,-1);
    }

}
