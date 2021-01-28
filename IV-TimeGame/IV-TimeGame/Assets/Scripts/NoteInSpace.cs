using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(SelectionEffect))]
public class NoteInSpace : MonoBehaviour
{
    
    [SerializeField]
    public Item item;
    [SerializeField]
    public ItemEffect.ItemEffectProps effectProps;
    private NoteScripting noteScript;
    private Canvas_Script script;
    private SelectionEffect selectionEffect;
    private bool isTriggered;
    // Start is called before the first frame update
    void Start()
    {
        script=GameObject.Find("InventoryCanvas").GetComponent<Canvas_Script>();
        noteScript = GameObject.Find("Items").GetComponent<NoteScripting>();
        script.PickUpItemSubscription(this);
        selectionEffect = gameObject.GetComponent<SelectionEffect>();
        selectionEffect.disableEffect();
        // Debug.Log("script started");
        item.SetItemEffect();
        item.itemEffect.effectProps = effectProps;
        isTriggered = false;
       
    }

    private void OnTriggerEnter(Collider other)
    {
       
     
        if (other.CompareTag("Player"))
        {
            script.ActivateInputFText();
            selectionEffect.enableEffect();
            isTriggered = true;

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            script.DeactivateInputFText();
            selectionEffect.disableEffect();
            isTriggered = false;
        }
    }

    public void PickUpItem()
    {
        if(isTriggered)
        {
            // Debug.Log("PickUpItem");
            script.ReceiveItem(item);
            script.DeactivateInputFText();
            noteScript.ActivateNextNode();
            isTriggered = false;
        }
       
    }
}
