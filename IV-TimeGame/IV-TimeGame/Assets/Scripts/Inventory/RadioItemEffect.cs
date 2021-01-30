using UnityEngine;

internal class RadioItemEffect : ItemEffect
{
    AudioSource radio;

    public RadioItemEffect(Item item) : base(item)
    {
        radio = GameObject.Find("radio").GetComponent<AudioSource>();
        radio.loop = true;
        
    }
    public override void UseItem()
    {
        if (radio.isPlaying)
        {
            radio.Stop();
            script.OpenTextBox("Stopped radio.");
        }
        else
        {
            radio.Play();
            radio.loop = true;
            script.OpenTextBox("Started radio.");
        }
         

    }
}