using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class ItemEffect
{
    protected Canvas_Script script;
    protected Item item;
    [SerializeField]
    public ItemEffectProps effectProps;
    [Serializable]
    public class ItemEffectProps
    {
        [TextArea(10, 15)]
        public string bottomText;
       
        public string topleftText;
        public Sprite image;
    }
    public ItemEffect(Item item)
    {
        script = GameObject.Find("InventoryCanvas").GetComponent<Canvas_Script>();
        this.item = item;
       
    }
    public abstract void UseItem();
}
