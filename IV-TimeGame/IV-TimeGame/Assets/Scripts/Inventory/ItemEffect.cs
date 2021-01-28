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
        [TextArea(5, 10)]
        public string bottomText;
        [TextArea(5, 10)]
        public string topleftText;
        public Image image;
    }
    public ItemEffect(Item item)
    {
        script = GameObject.Find("InventoryCanvas").GetComponent<Canvas_Script>();
        this.item = item;
       
    }
    public abstract void UseItem();
}
