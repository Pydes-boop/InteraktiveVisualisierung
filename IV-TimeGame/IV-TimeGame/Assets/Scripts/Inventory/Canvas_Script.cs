using DyrdaIo.FirstPersonController;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(InventorySound))]
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
    public IObservable<Unit> ProgressStory => _progressStory;
    private Subject<Unit> _progressStory;

    public GameObject player;
    private FirstPersonController playerController;
    [Header("References")]
    [SerializeField]
    public InventoryInputControl inventoryInputControl;
    public Inventory_UI ui;
    public GameObject inventoryObject;

    private Text xText;
    private GameObject infoTexts;
    private Text ladderText;
    private Text fPickUpItemText;
    private Text controlText;
    private Transform textBox;
    private bool fTextWasActive=false;
    private RectTransform fTextTransform;
    public int fPositionDifference = 30;
    private bool climbingTextShouldBeActivated = false;
    private bool climbingStatus = false;
    private bool controlTextWasActive = false;
    private bool ladderTextWasActive = false;
    private float yPositionOriginalFText;
    private InventorySound sound;
   void Awake()
    {
        playerController = player.GetComponent<FirstPersonController>();
        ui = inventoryObject.GetComponent<Inventory_UI>();
        infoTexts = transform.Find("InfoTexts").gameObject;
        xText =infoTexts.transform.Find("PressXText").GetComponent<Text>();
        controlText = infoTexts.transform.Find("ControlText").GetComponent<Text>();
        fPickUpItemText = infoTexts.transform.Find("PickUpItemText").GetComponent<Text>();
        fTextTransform = infoTexts.transform.Find("PickUpItemText").gameObject.GetComponent<RectTransform>();
        ladderText = infoTexts.transform.Find("LadderText").GetComponent<Text>();
        textBox = transform.Find("TextBox");
        //  Debug.Log("ui Null: " + ui == null);
        playerController.currentlyActive = FirstPersonController.CurrentlyActive.Player;
        
        _up = new Subject<Unit>().AddTo(this);
        _down = new Subject<Unit>().AddTo(this);
        _toggleMenu = new Subject<Unit>().AddTo(this);
        _closetextbox = new Subject<Unit>().AddTo(this);
        _progressStory = new Subject<Unit>().AddTo(this);
    }

    internal void SubscribeSound(InventorySound inventorySound)
    {
        this.sound = inventorySound;
    }
    void Start()
    {

        yPositionOriginalFText = fTextTransform.anchoredPosition.y;
      //  Debug.Log("yPosition: " + yPositionOriginalFText);
      
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
                case FirstPersonController.CurrentlyActive.Player: if(fPickUpItemText.IsActive()) _closetextbox.OnNext(Unit.Default);
                    break;
                case FirstPersonController.CurrentlyActive.Textbox: CloseTextBox_Func(); _progressStory.OnNext(Unit.Default);
                    break;
                default:break;
        }
            
        });
    }
    public void PickUpItemSubscription(ItemInSpace note)
    {
       
       CloseTextBox.Subscribe(i=>note.PickUpItem());
    }
    public void SubscribeStory(Story s)
    {
        ProgressStory.Subscribe(i => s.ProgressStory());
    }
   
    private void UseSelectItem()
    {
       
        if(playerController.currentlyActive==FirstPersonController.CurrentlyActive.Inventory)
        {

            if (ui.IsNoteOpen())
            {
                ui.CloseNote();
                sound.PlayMenuSelectionChanged();
            }
            else
            {

                if (ui.GetSelectedItem() != null)
                {
                    ui.GetSelectedItem().UseItem();
                   
                }
                else
                    OpenTextBox("No item selected.");
            }
                
   
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
            sound.PlayMenuSelectionChanged();
        }
    }
    private void GoDown()
    {
        if (playerController.GetCurrentlyActive() == FirstPersonController.CurrentlyActive.Inventory)
        {
            ui.GoDown();
            sound.PlayMenuSelectionChanged();
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
                DeactivatePickUpTextTemp();
                sound.PlayMenuOpenSound();
            }
            else if (playerController.currentlyActive == FirstPersonController.CurrentlyActive.Inventory)
            {
                playerController.currentlyActive = FirstPersonController.CurrentlyActive.Player;
                ActivatePickUpTextTemp();
                sound.PlayMenuCloseSound();
            }
            OpenCloseInventory();
        });
       

       
    }
    public void OpenCloseInventory()
    {
       
        if (playerController.GetCurrentlyActive() == FirstPersonController.CurrentlyActive.Inventory) 
        {
            ui.SetActive(true);
          
            ui.RefreshInventory(0);
            ui.CloseNote();
            xText.text = "Press (X) to close inventory";
        }
        else
        {
        
            ui.SetActive(false);
            xText.text = "Press (X) to open inventory";
        }
    }
    private void DeactivatePickUpTextTemp()
    {
        fTextWasActive = fPickUpItemText.gameObject.activeSelf;
        controlTextWasActive = controlText.gameObject.activeSelf;
        ladderTextWasActive = ladderText.gameObject.activeSelf;
        fPickUpItemText.gameObject.SetActive(false);
        ladderText.gameObject.SetActive(false);
        controlText.gameObject.SetActive(false);
    }
    private void ActivatePickUpTextTemp()
    {

        if (fTextWasActive)
        {
            fTextWasActive = false;
            fPickUpItemText.gameObject.SetActive(true);

        }
        if (controlTextWasActive)
        {
            controlTextWasActive = false;
            controlText.gameObject.SetActive(true);
        }
        if (ladderTextWasActive)
        {
            ladderTextWasActive = false;
           ladderText.gameObject.SetActive(true);
        }
    }
    public void ReceiveItem(Item item)
    {
        ui.inventory.AddItem(item);
        OpenTextBox(item.name + " received.");
        sound.PlaySuccess();
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
        sound.PlayOpenTextBox();
 
    }
    public void DeactiveTexts()
    {
        infoTexts.SetActive(false);
    }
    public void DeactivateInputFText()
    {
        fTextWasActive = false;
        fPickUpItemText.gameObject.SetActive(false);
    }
    public void ActivateInputFText()
    {
        fTextWasActive = true;
        fPickUpItemText.gameObject.SetActive(true);
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
        sound.PlayCloseTextBox();
      
    }
    public void ClimbingStatusChanged(bool newStatus)
    {
        climbingStatus = newStatus;
        if (climbingStatus)
            ladderText.text = "Use (E) and (Q) to climb ladder";
        else
        {
            ladderText.text = "Press (E) to climb ladder";
            if (!climbingTextShouldBeActivated)
                DeactivateClimbing_Func();
        }
           
    }
    public void ActivateClimbingText()
    {
      //  Debug.Log("activate:"+yPositionOriginalFText);
        climbingTextShouldBeActivated = true;
        ladderText.gameObject.SetActive(true);
        fTextTransform.anchoredPosition = new Vector2(fTextTransform.anchoredPosition.x,
            yPositionOriginalFText + fPositionDifference);
     
    }
    public void DeActivateClimbingText()
    {
        climbingTextShouldBeActivated = false;
        if (!climbingStatus)
            DeactivateClimbing_Func();
    }
    private void DeactivateClimbing_Func()
    {
      //  Debug.Log("deactivate:"+yPositionOriginalFText);
        ladderText.gameObject.SetActive(false);
        fTextTransform.anchoredPosition = new Vector2(fTextTransform.anchoredPosition.x,
           yPositionOriginalFText);
    }

    internal void ShowNoteInUI(Item item, ItemEffect.ItemEffectProps effectProps)
    {
        ui.OpenNote(item, effectProps);
        sound.PlayMenuSelectionChanged();

    }

    internal void ActivateControlText()
    {
        controlText.gameObject.SetActive(true);
    }
    internal void DeactivateControlText()
    {
        controlText.gameObject.SetActive(false);
    }
}
