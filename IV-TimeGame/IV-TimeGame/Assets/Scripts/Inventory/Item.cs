using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item 
{
    public enum ItemType
    {
        Note = 0,
        Key = 1
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
    public string description = "Test Description";
    public Sprite GetSprite()
    {
       
        switch (type)
        {

            default:
            case ItemType.Note: return ItemAssets.Instance.NoteSprite;
            case ItemType.Key: return ItemAssets.Instance.KeySprite;
      
                
        }
    }
    public Sprite GetIcon()
    {
        
        switch (type)
        {
           
            case ItemType.Note: return ItemAssets.Instance.NoteIcon;
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
}

