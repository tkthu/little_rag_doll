using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeetController : MonoBehaviour
{
    public GameObject snail;

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground") || other.gameObject.layer == LayerMask.NameToLayer("Them"))
        {
            snail.transform.Rotate(new Vector3(0.0f, 180, 0.0f));
        }
    }

    
}
