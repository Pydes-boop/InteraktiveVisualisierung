using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class ItemEffect
{
    protected Canvas_Script script;
    public ItemEffect()
    {
        script = GameObject.Find("InventoryCanvas").GetComponent<Canvas_Script>();
       
    }
    public abstract void UseItem();
}
