using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BoardClick : MonoBehaviour
{

    GameObject lastClicked = null;

    public float rayDistance = 100f;
    public float targetOverBordDistance = 0.05f;

    public GameObject Player;

    private Camera cam;

    public Pair[] pairs;

    public delegate void Accept(bool correctAnswers);
    public event Accept OnAccept;

    private void Awake()
    {
        cam = Player.GetComponentInChildren<Camera>();
    }

    void Update()
    {
        if (Keyboard.current.leftCtrlKey.wasPressedThisFrame) 
        {
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.Confined;
            }
            else 
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
        }

        if (Mouse.current.leftButton.wasPressedThisFrame) 
        {
            lastClicked = getClickedObject("Dragable");
            if (lastClicked != null) 
            {
                BoardItem script = lastClicked.GetComponent<BoardItem>();
                script.setDropOff(null);
            }

            GameObject accepted = getClickedObject("Accept");
            if (accepted != null) 
            {
                OnAccept?.Invoke(true);
            }

        }

        if (Mouse.current.leftButton.isPressed && lastClicked != null) 
        {
            Vector3 pointTo = getImpectPoint();
            if (pointTo.x != -1) 
            {
                lastClicked.transform.position = pointTo;
            }
        }

        if (Mouse.current.leftButton.wasReleasedThisFrame) 
        {
            if (lastClicked != null)
            {
                GameObject dropOff = getDropOff();
                BoardItem script = lastClicked.GetComponent<BoardItem>();
                script.setDropOff(dropOff);

                if (dropOff != null)
                {
                    lastClicked.transform.position = dropOff.transform.position;
                }

            }

            lastClicked = null;
        }
    }

    public GameObject getClickedObject(string Tag) 
    {
        if (cam == null) return null;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, 100))
        {
            return (hit.transform.gameObject.tag.Equals(Tag) ? hit.transform.gameObject : null);
        }
        else
        {
            return null;
        }
    }

    public Vector3 getImpectPoint()
    {
        if (cam == null) return new Vector3(-1, -1, -1);
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits;
        hits = Physics.RaycastAll(ray, 100);

        for (int i = 0; i < hits.Length; i++) 
        {
            if (hits[i].transform.gameObject.tag.Equals("InfoBord")) 
            {
                return hits[i].point + (ray.direction.normalized * -targetOverBordDistance);
            }

        }
        return new Vector3(-1,-1,-1);
    }

    public GameObject getDropOff() 
    {
        if (cam == null) return null;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits;
        hits = Physics.RaycastAll(ray, 100);

        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].transform.gameObject.tag.Equals("DropOff"))
            {
                return hits[i].transform.gameObject;
            }

        }
        return null;
    }

}
