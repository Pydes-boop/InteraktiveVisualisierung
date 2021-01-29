using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BoardItemInfo : MonoBehaviour
{
    public Sprite image = null;

    public string Header = "Sample";

    [Multiline (4)]
    public string Text = "Sample";
}
