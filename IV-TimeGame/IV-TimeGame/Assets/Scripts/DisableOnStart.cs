using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableOnStart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.SetActive(false);
        StartCoroutine(Disable());
    }
    IEnumerator Disable()
    {

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(0.05f);
        this.gameObject.SetActive(false);
    }

}
