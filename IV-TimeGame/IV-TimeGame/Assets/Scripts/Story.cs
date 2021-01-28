using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Introduction : MonoBehaviour
{
    private int eventCounter = 0;
    private bool shouldListen = false;
    private Canvas_Script script;
    // Start is called before the first frame update
    void Start()
    {
        script = GameObject.Find("InventoryCanvas").GetComponent<Canvas_Script>();
        StartCoroutine(InitialInventory());
        
    }

    IEnumerator InitialInventory()
    {
        
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(3);

        Item watch = new Item("Time travelling watch", Item.ItemType.Watch);
        watch.description = "A futuristic watch, handed to you by the contest's committee. Press (T) while walking to travel through time.";
        script.ReceiveItem(watch);
        shouldListen = true;
    }
    private void HandleStoryProgression()
    {

    }

}
