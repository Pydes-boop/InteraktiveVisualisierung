using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteInSpace : MonoBehaviour
{
    
    [SerializeField]
    public Item item;

    private Canvas_Script script;
    // Start is called before the first frame update
    void Start()
    {
        script=GameObject.Find("InventoryCanvas").GetComponent<Canvas_Script>();
        script.PickUpItemSubscription(this);
    }

    private void OnTriggerEnter(Collider other)
    {
       // Debug.Log(script == null);
        if (other.CompareTag("Player"))
        {
            script.ActivateInputFText();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            script.DeactivateInputFText();
        }
    }

    public void PickUpItem()
    {
        script.ReceiveItem(item);
        script.DeactivateInputFText();
        Destroy(gameObject);
    }
}
