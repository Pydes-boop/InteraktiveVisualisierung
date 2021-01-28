using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SelectionEffect))]
public class NoteInSpace : MonoBehaviour
{
    
    [SerializeField]
    public Item item;

    private Canvas_Script script;
    private SelectionEffect selectionEffect;
    // Start is called before the first frame update
    void Start()
    {
        script=GameObject.Find("InventoryCanvas").GetComponent<Canvas_Script>();
        script.PickUpItemSubscription(this);
        selectionEffect = gameObject.GetComponent<SelectionEffect>();
        selectionEffect.disableEffect();
        Debug.Log("script started");
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("hello");
     
        if (other.CompareTag("Player"))
        {
            script.ActivateInputFText();
            selectionEffect.enableEffect();

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            script.DeactivateInputFText();
            selectionEffect.disableEffect();
        }
    }

    public void PickUpItem()
    {
       // Debug.Log("PickUpItem");
        script.ReceiveItem(item);
        script.DeactivateInputFText();
        Destroy(gameObject);
    }
}
