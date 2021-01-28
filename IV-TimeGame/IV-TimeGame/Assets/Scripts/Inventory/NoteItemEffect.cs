using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteItemEffect  :ItemEffect
{ 
  
    public NoteItemEffect(Item item):base(item)
    {
        
    }
  
    override public void UseItem()
    {
        script.ShowNoteInUI(item, effectProps);
    }
}
