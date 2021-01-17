using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item 
{
    public enum ItemType
    {
        Note = 0,
        Key = 1,
        Watch= 2
    }

    public Item()
    {
        
    }

    public Item(string name, ItemType type)
    {
        this.name = name;
        this.type = type;
    }
    public string name;
    public ItemType type;
    [TextArea(15, 20)]
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
        if (itemEffect == null)
            itemEffect = new NoItemEffect();
        itemEffect.UseItem();

    }
}

