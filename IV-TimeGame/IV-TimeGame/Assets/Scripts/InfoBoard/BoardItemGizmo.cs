using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardItemGizmo : MonoBehaviour
{

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0.8f,0.2f,0.9f);
        Gizmos.DrawWireSphere(this.transform.position, 0.1f);
    }

}
