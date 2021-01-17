using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Introduction : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(InitialInventory());
        
    }

    IEnumerator InitialInventory()
    {
        
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(1);

        Item watch = new Item("Time travelling watch", Item.ItemType.Watch);
        watch.description = "A futuristic watch, handed to you by the contest's committee. Press (T) while walking to travel through time.";
        GameObject.Find("InventoryCanvas").GetComponent<Canvas_Script>().ReceiveItem(watch);
    }

}
