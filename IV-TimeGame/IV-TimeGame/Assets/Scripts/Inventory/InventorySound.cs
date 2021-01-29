using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(AudioSource))]
[RequireComponent(typeof(Canvas_Script))]
public class InventorySound : MonoBehaviour
{
    public Canvas_Script script;
    public AudioSource source;
    public AudioClip openMenu;
    public AudioClip closeMenu;
    public AudioClip success;
    public AudioClip openTextBox;
    public AudioClip closeTextBox;
    public AudioClip menuSelectionChanged;
    // Start is called before the first frame update
    void Start()
    {
        script = gameObject.GetComponent<Canvas_Script>();
        source = gameObject.GetComponent<AudioSource>();
        source.playOnAwake = false;
        source.loop = false;
        script.SubscribeSound(this);
    }
    public void PlayMenuOpenSound()
    {
        source.Stop();
        source.clip = openMenu;
        source.Play();
    }
    public void PlayMenuCloseSound()
    {
        source.Stop();
        source.clip = closeMenu;
        source.Play();
    }
    public void PlaySuccess()
    {
        source.Stop();
        source.clip = success;
        source.Play();
    }
    public void PlayCloseTextBox()
    {
        source.Stop();
        source.clip = closeTextBox;
        source.Play();
    }
    public void PlayOpenTextBox()
    {
        source.Stop();
        source.clip = openTextBox;
        source.Play();
    }
    public void PlayMenuSelectionChanged()
    {
        source.Stop();
        source.clip = menuSelectionChanged;
        source.Play();
    }

}
