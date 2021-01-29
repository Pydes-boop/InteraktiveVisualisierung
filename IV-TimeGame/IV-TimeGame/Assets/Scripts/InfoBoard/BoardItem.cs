using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardItem : MonoBehaviour
{
    private GameObject dropOff = null;

    public void setDropOff(GameObject newDropOff) 
    {
        this.dropOff = newDropOff;    
    }

    public GameObject getDropOff() 
    {
        return dropOff;
    }
}
