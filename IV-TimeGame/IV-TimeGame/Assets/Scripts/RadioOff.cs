//script by Philipp
//The radio is great but the song gets creepy after a while :D

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RadioOff : MonoBehaviour
{
    private AudioSource source;
    private Transform player;
    private bool radioOn = false;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
        player = GameObject.Find("FirstPersonPlayerTime").transform;
    }

    void Update()
    {

        if (Keyboard.current.eKey.wasPressedThisFrame && (transform.position - player.position).magnitude < 10) 
        {
            radioOn = !radioOn;
            source.mute = radioOn;
        }

    }
}
