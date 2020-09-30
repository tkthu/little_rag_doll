using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeetController : MonoBehaviour
{
    public GameObject snail;

    public void OnTriggerExit2D(Collider2D other)
    {
        snail.transform.Rotate(new Vector3(0.0f, 180, 0.0f));
    }

    
}
