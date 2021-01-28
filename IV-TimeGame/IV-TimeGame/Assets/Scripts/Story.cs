using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Story : MonoBehaviour
{
    private int eventCounter = 0;
    private bool shouldListen = true;
    private Canvas_Script script;
    [TextArea(10 , 5)]
    public string[] storyTexts;
    // Start is called before the first frame update
    void Start()
    {
        script = GameObject.Find("InventoryCanvas").GetComponent<Canvas_Script>();
        HandleStoryProgression();
        StartCoroutine(InitialWait());
        
    }
    IEnumerator InitialWait()
    {
        yield return new WaitForSeconds(2);
        ProgressStory();
    }

    IEnumerator InitialInventory()
    {
        
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(0.5f);

        Item watch = new Item("Time travelling watch", Item.ItemType.Watch);
        watch.description = "A futuristic watch, handed to you by the contest's committee. Press (T) while walking to travel through time.";
        script.ReceiveItem(watch);
       
    }
    private void HandleStoryProgression()
    {
        script.SubscribeStory(this);
    }
    public void ProgressStory()
    {
        //Debug.Log("this");
        if(shouldListen)
        {
            StartCoroutine(NextStoryTextBox());
        }
    }
    IEnumerator NextStoryTextBox()
    {
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(0.5f);
        if (eventCounter >= storyTexts.Length)
        {
            shouldListen = false;
            StartCoroutine(InitialInventory());
        }
        else
        script.OpenTextBox(storyTexts[eventCounter]);
       
      
        eventCounter++;
    }

}
