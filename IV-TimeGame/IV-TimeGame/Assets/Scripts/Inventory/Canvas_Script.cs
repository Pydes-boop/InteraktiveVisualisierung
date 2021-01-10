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

    public GameObject player;
    private FirstPersonController playerController;
    [Header("References")]
    [SerializeField]
    public InventoryInputControl inventoryInputControl;
    public Inventory_UI ui;
    public GameObject inventoryObject;

    private Text xText;
    private void Awake()
    {
        playerController = player.GetComponent<FirstPersonController>();
       
    }
    private void Start()
    {
    //    Debug.Log("test");
        _up = new Subject<Unit>().AddTo(this);
        _down = new Subject<Unit>().AddTo(this);
        _toggleMenu = new Subject<Unit>().AddTo(this);
        HandleUpDown();
        ui = inventoryObject.GetComponent<Inventory_UI>();
        xText = transform.Find("PressXText").GetComponent<Text>();
        
        
    }

    private void HandleUpDown()
    {
        inventoryInputControl.Up.Subscribe(i => { GoUp(); });
        inventoryInputControl.Down.Subscribe(i => { GoDown(); });
        playerController.firstPersonControllerInput.ToggleMenu.Subscribe(i => { OpenCloseInventory(); });
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
}
