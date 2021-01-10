using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory_UI : MonoBehaviour
{
    public float stepSize;
    public int maxPerPage;
    public Inventory inventory;
    
    // Start is called before the first frame update
    private Transform itemSlotContainer;
    private Transform itemSlotTemplate;
    private Transform itemSlotTemplateSelected;
    private Transform itemDescription;
    private int currentlySelected;
    private int currentlyAt;
    private Transform tr;
    private void Awake()
    {
        tr = transform.Find("InventoryContainer");
        itemSlotContainer = tr.Find("ItemSlotContainer");
        itemSlotTemplate = itemSlotContainer.Find("ItemSlotTemplate");
        itemSlotTemplateSelected = itemSlotContainer.Find("ItemSlotTemplateSelected");
        itemDescription = tr.Find("Description");
        SetInventory(new Inventory());
        inventory.AddItem(new Item("my little pony", Item.ItemType.Key));
        inventory.AddItem(new Item("test 123", Item.ItemType.Note));

        for(int i =0; i< 15; i ++)
        {
            inventory.AddItem(new Item("I: " + i, Item.ItemType.Key));
        }
        currentlySelected = 0;
        currentlyAt = 0;
        //RefreshInventory(0);
        tr.gameObject.SetActive(false);
        
       
    }
   
    public void GoUp()
    {
        if (currentlySelected > 0)
            currentlySelected--;
        if (currentlySelected < currentlyAt)
            currentlyAt--;
        RefreshInventory(currentlyAt);
    }
    public void GoDown()
    {
        
        if (currentlySelected < inventory.GetItemList().Count - 1)
            currentlySelected++;
        if (currentlyAt + maxPerPage < currentlySelected+1)
            currentlyAt++;
        Debug.Log("currently At: " + currentlyAt + ", selected: " + currentlySelected);
        RefreshInventory(currentlyAt);
    }
   
    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;
        inventory.OnItemListChanged += OnItemListChanged;
        currentlyAt = 0;
        RefreshInventory(0);
    }
    private void OnItemListChanged(object sender, System.EventArgs e)
    {
        currentlyAt = 0;
        RefreshInventory(0);
    }
     public void RefreshInventory(int startingFrom)
    {
       // Debug.Log("Starting From: " + startingFrom);
        DestroyOldItemSlots();
        int counter = startingFrom;
     
        while(inventory.GetItemList().Count>counter&&counter<maxPerPage+startingFrom)
        {
            RectTransform rTransform;
            Item i = inventory.GetItemList()[counter];
            if (counter == currentlySelected)
                rTransform = Instantiate(itemSlotTemplateSelected, itemSlotContainer).GetComponent<RectTransform>();
           else
             rTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
           
            rTransform.gameObject.SetActive(true);
            rTransform.anchoredPosition -= new Vector2(0, (counter-currentlyAt) * stepSize);
            Image image = rTransform.Find("IconImage").GetComponent<Image>();
            Text text = rTransform.Find("Name").GetComponent<Text>();
            text.text = i.GetName();
            image.sprite = i.GetIcon();
            counter++;
        }
        itemSlotTemplate.gameObject.SetActive(false);
        itemSlotTemplateSelected.gameObject.SetActive(false);
        WriteDescription();
    }
    private void DestroyOldItemSlots()
    {
        foreach(Transform child in itemSlotContainer)
        {
            if (child == itemSlotTemplate|| child ==itemSlotTemplateSelected)
                continue;
            Destroy(child.gameObject);
        }

    }
    public void SetActive(bool active)
    {
        tr.gameObject.SetActive(active);
    }
    private void WriteDescription()
    {
        Transform t = itemDescription.Find("DescriptionContainer");
        
        t.gameObject.SetActive(true);
        if (inventory.GetItemList().Count == 0)
            return;
        Item i = inventory.GetItemList()[currentlySelected];

        Image image = t.Find("Image").GetComponent<Image>();
        Text header =t.Find("Header").GetComponent<Text>();
        Text text = t.Find("Text").GetComponent<Text>();
        header.text = i.GetName();
        image.sprite = i.GetSprite();
        text.text = i.GetDescription();
    }
    private void OnDisable()
    {
        inventory.OnItemListChanged -= OnItemListChanged;
    }

}
