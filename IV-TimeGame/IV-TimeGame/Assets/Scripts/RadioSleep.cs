using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioSleep : MonoBehaviour
{
    public AudioSource radio;
    void Start()
    {
        CallAudio();
    }

    void CallAudio() {
        radio.Play();
        //Debug.Log("Random Sound played again");
        Invoke("SoundTimer", 285);
    }

    void SoundTimer() {
        CallAudio();
    }
}
