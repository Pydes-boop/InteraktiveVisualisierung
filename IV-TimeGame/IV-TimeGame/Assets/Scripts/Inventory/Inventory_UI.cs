using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static ItemEffect;

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
    private Transform noteView;
    private int currentlySelected;
    private int currentlyAt;
    private Transform tr;

    
    private void Awake()
    {
        tr = transform.Find("Container");
        itemSlotContainer = tr.Find("ItemSlotContainer");
        itemSlotTemplate = itemSlotContainer.Find("ItemSlotTemplate");
        itemSlotTemplateSelected = itemSlotContainer.Find("ItemSlotTemplateSelected");
        itemDescription = tr.Find("Description");
        noteView = tr.Find("NoteView");
       
        SetInventory(new Inventory()); 
       
    }
    void Start()
    {
        SetActive(true);
    }
  
    public Item GetSelectedItem()
    {
        if (inventory.GetItemList().Count > 0 && inventory.GetItemList().Count > currentlySelected)
            return inventory.GetItemList()[currentlySelected];
        return null;
    }
    
    public void GoUp()
    {
        if (!noteView.gameObject.activeSelf)
        {
            if (currentlySelected > 0)
                currentlySelected--;
            if (currentlySelected < currentlyAt)
                currentlyAt--;
            RefreshInventory(currentlyAt);
        }
      
    }
    public void GoDown()
    {
        if (!noteView.gameObject.activeSelf)
        {
            if (currentlySelected < inventory.GetItemList().Count - 1)
                currentlySelected++;
            if (currentlyAt + maxPerPage < currentlySelected + 1)
                currentlyAt++;
            // Debug.Log("currently At: " + currentlyAt + ", selected: " + currentlySelected);
            RefreshInventory(currentlyAt);
        }
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
        if (!active)
        {
            currentlySelected = 0;
            currentlyAt = 0;
        }
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
    public void OpenNote(Item item, ItemEffectProps effectProps)
    {
        noteView.gameObject.SetActive(true);
        Transform container = noteView.Find("Container").GetComponent<Transform>();
        Image image =container.Find("Image").GetComponent<Image>();
        if (effectProps.image == null)
        {
            image.gameObject.SetActive(false);
            Text bottomText = container.Find("BottomText").GetComponent<Text>();
            bottomText.text = effectProps.bottomText;
        }
        else
        {
            image.gameObject.SetActive(true);
            image.sprite = effectProps.image;
        }
        Text topText = container.Find("TopText").GetComponent<Text>();
        topText.text = item.GetDescription();
        Text topLeftText = container.Find("TopLeftText").GetComponent<Text>();
        topLeftText.text = effectProps.topleftText;
        Text header = container.Find("Header").GetComponent<Text>();
        header.text = item.GetName();

    }

    
    public void CloseNote()
    {
        noteView.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        inventory.OnItemListChanged -= OnItemListChanged;
    }
    public bool IsNoteOpen()
    {
        return noteView.gameObject.activeSelf;
    }

}
