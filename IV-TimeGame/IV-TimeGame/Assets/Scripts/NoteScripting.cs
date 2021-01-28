using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteScripting : MonoBehaviour
{
    private List<GameObject> notes;
 
    void Start()
    {
        notes = new List<GameObject>();
        StartCoroutine(DeactivateAll());
       
    }
    IEnumerator DeactivateAll()
    {
        yield return new WaitForSeconds(0.5f);
        foreach (Transform child in transform)
            notes.Add(child.gameObject);
        notes.Sort((i, j) => i.name.CompareTo(j.name));
        for (int i = 1; i < notes.Count; i++)
            notes[i].SetActive(false);
    }
    public void ActivateNextNode()
    {
        Debug.Log("hello");
        GameObject toDestroy = notes[0];
        notes.RemoveAt(0);
        Destroy(toDestroy);
        if (notes.Count > 0)
            notes[0].SetActive(true);
    }
}
