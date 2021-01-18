using DyrdaIo.FirstPersonController;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class Canvas_Script : MonoBehaviour, IInventorySignals
{
    private FirstPersonController.CurrentlyActive lastActive;
    public IObservable<Unit> Up => _up;
    private Subject<Unit> _up;
    public IObservable<Unit> Down=> _down;
    private Subject<Unit> _down;

    public IObservable<Unit> ToggleMenu => _toggleMenu;
    private Subject<Unit> _toggleMenu;

    public IObservable<Unit> CloseTextBox=> _closetextbox;
    private Subject<Unit> _closetextbox;

    public GameObject player;
    private FirstPersonController playerController;
    [Header("References")]
    [SerializeField]
    public InventoryInputControl inventoryInputControl;
    public Inventory_UI ui;
    public GameObject inventoryObject;

    private Text xText;
    private GameObject infoTexts;
    private Text pickUpItemText;
    private Transform textBox;
    private bool shouldListen;
   void Awake()
    {
        playerController = player.GetComponent<FirstPersonController>();
        ui = inventoryObject.GetComponent<Inventory_UI>();
        infoTexts = transform.Find("InfoTexts").gameObject;
        xText =infoTexts.transform.Find("PressXText").GetComponent<Text>();
        pickUpItemText = infoTexts.transform.Find("PickUpItemText").GetComponent<Text>();
        
        textBox = transform.Find("TextBox");
        //  Debug.Log("ui Null: " + ui == null);
        playerController.currentlyActive = FirstPersonController.CurrentlyActive.Player;
    }
     void Start()
    {
    //    Debug.Log("test");
        _up = new Subject<Unit>().AddTo(this);
        _down = new Subject<Unit>().AddTo(this);
        _toggleMenu = new Subject<Unit>().AddTo(this);
        _closetextbox = new Subject<Unit>().AddTo(this);
        HandlePositiveInput();
        HandleUpDown();
        HandleMenuToggle();

    }
    private void HandlePositiveInput()
    {
        inventoryInputControl.CloseTextBox.Subscribe(i => { 
        switch (playerController.currentlyActive)
        {
                case FirstPersonController.CurrentlyActive.Inventory: UseSelectItem();
                    break;
                case FirstPersonController.CurrentlyActive.Player: if(pickUpItemText.IsActive()) _closetextbox.OnNext(Unit.Default);
                    break;
                case FirstPersonController.CurrentlyActive.Textbox: CloseTextBox_Func();
                    break;
                default:break;
        }
            
        });
    }
    public void PickUpItemSubscription(NoteInSpace note)
    {
       CloseTextBox.Subscribe(i=>note.PickUpItem());
    }
   
    private void UseSelectItem()
    {
       
        if(playerController.currentlyActive==FirstPersonController.CurrentlyActive.Inventory)
        {
           
                // Debug.Log("use item");
                if (ui.GetSelectedItem() != null)
                    ui.GetSelectedItem().UseItem();
                else
                    OpenTextBox("No item selected.");
   
        }
        
       
    }

    private void HandleUpDown()
    {
        inventoryInputControl.Up.Subscribe(i => { GoUp(); });
        inventoryInputControl.Down.Subscribe(i => { GoDown(); });
      
    }
    private void GoUp()
    {
        
        if (playerController.GetCurrentlyActive() == FirstPersonController.CurrentlyActive.Inventory)
        {
            ui.GoUp();
        }
    }
    private void GoDown()
    {
        if (playerController.GetCurrentlyActive() == FirstPersonController.CurrentlyActive.Inventory)
        {
            ui.GoDown();
        }
    }

    private void HandleMenuToggle()
    {

        playerController.firstPersonControllerInput.ToggleMenu.Subscribe(i =>
        {

            if (playerController.currentlyActive != FirstPersonController.CurrentlyActive.Player
                && playerController.currentlyActive != FirstPersonController.CurrentlyActive.Inventory)
                return;

            if (playerController.currentlyActive == FirstPersonController.CurrentlyActive.Player)
            {
                playerController.currentlyActive = FirstPersonController.CurrentlyActive.Inventory;
                shouldListen = true;
            } 
            else if(playerController.currentlyActive== FirstPersonController.CurrentlyActive.Inventory)
                playerController.currentlyActive = FirstPersonController.CurrentlyActive.Player;
            OpenCloseInventory();
        });
       

       
    }
    public void OpenCloseInventory()
    {
       
        if (playerController.GetCurrentlyActive() == FirstPersonController.CurrentlyActive.Inventory) 
        {
            ui.SetActive(true);
            ui.RefreshInventory(0);
            xText.text = "Press (X) to close inventory";
        }
        else
        {
            ui.SetActive(false);
            xText.text = "Press (X) to open inventory";
        }
    }
    public void ReceiveItem(Item item)
    {
        ui.inventory.AddItem(item);
        OpenTextBox(item.name + " received.");
        OpenCloseInventory();
        DeactiveTexts();
       
    }

    public void OpenTextBox(string text)
    {
        lastActive = playerController.currentlyActive;
        playerController.currentlyActive = FirstPersonController.CurrentlyActive.Textbox;
        textBox.gameObject.SetActive(true);
        Text t = textBox.Find("Text").GetComponent<Text>();
        t.text = text;
        DeactiveTexts();
       
        
    }
    public void DeactiveTexts()
    {
        infoTexts.SetActive(false);
    }
    public void DeactivateInputFText()
    {
        pickUpItemText.gameObject.SetActive(false);
    }
    public void ActivateInputFText()
    {
        pickUpItemText.gameObject.SetActive(true);
    }
    public void ActivateTexts()
    {
        infoTexts.SetActive(true);
    }
    public void CloseTextBox_Func()
    {
        //Debug.Log("Close Textbox:" + (playerController.currentlyActive == FirstPersonController.CurrentlyActive.Textbox));
        textBox.gameObject.SetActive(false);
        if (playerController.currentlyActive == FirstPersonController.CurrentlyActive.Textbox)
            playerController.currentlyActive = lastActive;
        ActivateTexts();
        OpenCloseInventory();
      
    }

}
