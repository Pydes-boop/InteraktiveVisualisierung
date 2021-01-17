using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteItemEffect  :ItemEffect
{ 
  
  
    override public void UseItem()
    {
        script.OpenTextBox("This item is not useable.");
    }
}
