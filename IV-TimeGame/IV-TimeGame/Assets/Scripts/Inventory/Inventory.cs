using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory 
{
    private List<Item> itemList;
    public Inventory()
    {
        itemList = new List<Item>();
    }
    public void AddItem(Item item)
    {
        this.itemList.Add(item);
    }
    public void RemoveItem(Item item)
    {
        this.itemList.Remove(item);
    }
    public List<Item> GetItemList()
    {
        return itemList;
    }
}
