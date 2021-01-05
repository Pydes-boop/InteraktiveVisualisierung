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
    private void Awake()
    {
        itemSlotContainer = transform.Find("ItemSlotContainer");
        itemSlotTemplate = itemSlotContainer.Find("ItemSlotTemplate");
        itemSlotTemplateSelected = itemSlotContainer.Find("ItemSlotTemplateSelected");
        itemDescription = transform.Find("Description");
        SetInventory(new Inventory());
        inventory.AddItem(new Item("my little pony", Item.ItemType.Key));
        inventory.AddItem(new Item("test 123", Item.ItemType.Note));
        currentlySelected = 0;
        RefreshInventory(0);
    }
    void Start()
    {
        
    }
    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;
        RefreshInventory(0);
    }
    public void RefreshInventory(int startingFrom)
    {
        int counter = startingFrom;
     
        while(inventory.GetItemList().Count>counter&&counter<maxPerPage-startingFrom)
        {
            RectTransform rTransform;
            Item i = inventory.GetItemList()[counter];
            if (counter == currentlySelected)
                rTransform = Instantiate(itemSlotTemplateSelected, itemSlotContainer).GetComponent<RectTransform>();
            else
             rTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
           
            rTransform.gameObject.SetActive(true);
            rTransform.anchoredPosition -= new Vector2(0, counter * stepSize);
            Image image = rTransform.Find("IconImage").GetComponent<Image>();
            Text text = rTransform.Find("Name").GetComponent<Text>();
            text.text = i.GetName();
            image.sprite = i.GetIcon();
            counter++;
        }
        WriteDescription();
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
   
}
