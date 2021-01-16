using DyrdaIo.FirstPersonController;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class Canvas_Script : MonoBehaviour, IInventorySignals
{
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
    private Transform textBox;
   void Awake()
    {
        playerController = player.GetComponent<FirstPersonController>();
        ui = inventoryObject.GetComponent<Inventory_UI>();
        xText = transform.Find("PressXText").GetComponent<Text>();
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
        HandleUpDown();
        HandleTextBox();

        ui.SetActive(false);


    }
    private void HandleTextBox()
    {
        inventoryInputControl.CloseTextBox.Subscribe(i => { CloseTextBox_Func(); });
    }

    private void HandleUpDown()
    {
        inventoryInputControl.Up.Subscribe(i => { GoUp(); });
        inventoryInputControl.Down.Subscribe(i => { GoDown(); });
        HandleMenuToggle();
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
                playerController.currentlyActive = FirstPersonController.CurrentlyActive.Inventory;
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
        playerController.currentlyActive = FirstPersonController.CurrentlyActive.Textbox;
        OpenCloseInventory();
        OpenTextBox(item.name + " received.");
       
    }

    public void OpenTextBox(string text)
    {
        textBox.gameObject.SetActive(true);
        Text t = textBox.Find("Text").GetComponent<Text>();
        t.text = text;
        xText.gameObject.SetActive(false);
        xText.text = "";
    }
    public void CloseTextBox_Func()
    {
        textBox.gameObject.SetActive(false);
        if (playerController.currentlyActive == FirstPersonController.CurrentlyActive.Textbox)
            playerController.currentlyActive = FirstPersonController.CurrentlyActive.Player;
        xText.gameObject.SetActive(true);
        OpenCloseInventory();
      
    }

}
