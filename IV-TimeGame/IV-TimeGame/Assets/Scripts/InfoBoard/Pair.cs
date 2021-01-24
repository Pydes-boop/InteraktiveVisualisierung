using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Pair
{
    
    public GameObject one, two;

    public void setOne(GameObject one) 
    {
        this.one = one;
    }

    public void setTwo(GameObject two) 
    {
        this.two = two;
    }

    public GameObject getOne() 
    {
        return this.one;
    }

    public GameObject getTwo()
    {
        return this.two;
    }
}
