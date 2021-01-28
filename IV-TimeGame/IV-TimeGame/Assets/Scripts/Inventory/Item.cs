using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Item 
{
    public enum ItemType
    {
        Note = 0,
        Key = 1,
        Watch= 2
    }
    public enum ItemEffectType
    {
        None=0,
        Note=1
    }
    public ItemEffectType effectType=ItemEffectType.None;

    public Item(string name, ItemType type)
    {
        this.name = name;
        this.type = type;
        
    }
   
    public void SetItemEffect()
    {
        if (itemEffect == null)
        {
            if (effectType == ItemEffectType.None)
                itemEffect = new NoItemEffect(this);
            else if (effectType == ItemEffectType.Note)
                itemEffect = new NoteItemEffect(this);
        }
    }
    public string name;
    public ItemType type;
    [TextArea(5, 10)]
    public string description = "Test Description";
   
    public ItemEffect itemEffect;
   
    public Sprite GetSprite()
    {
       
    
        switch (type)
        {
            case ItemType.Watch: return ItemAssets.Instance.WatchSprite;
            case ItemType.Note: return ItemAssets.Instance.NoteSprite;
            default:
            case ItemType.Key: return ItemAssets.Instance.KeySprite;
  
                
        }
    }
    public Sprite GetIcon()
    {
        
        switch (type)
        {
           
            case ItemType.Note: return ItemAssets.Instance.NoteIcon;
            case ItemType.Watch:
            case ItemType.Key: return ItemAssets.Instance.KeyIcon;
            default: return ItemAssets.Instance.OtherIcon;
        }
    }
    public string GetName()
    {
        return name;
    }
    public string GetDescription()
    {
        return description;
    }
    public void UseItem()
    {

        SetItemEffect();
        itemEffect.UseItem();

    }
}

