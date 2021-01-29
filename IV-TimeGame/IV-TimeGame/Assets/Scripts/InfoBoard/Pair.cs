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

    public bool correctPair(GameObject o1, GameObject o2) 
    {
        return (o1.name.Equals(one.name) || o1.name.Equals(two.name)) && (o2.name.Equals(one.name) || o2.name.Equals(two.name));
    }
}
