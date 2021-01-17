using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAssets : MonoBehaviour
{
    public static ItemAssets Instance { get; private set; }
    public void Awake()
    {
        Instance = this;
    }
    
    

    public Sprite NoteSprite;
    public Sprite KeySprite;
    public Sprite NoteIcon;
    public Sprite KeyIcon;
    public Sprite OtherIcon;
    public Sprite WatchSprite;
}
