using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceController : MonoBehaviour
{
    public GameObject snail;

    public void OnTriggerEnter2D(Collider2D other)
    {
        // Layer 9 is Ground
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground") || other.gameObject.layer == LayerMask.NameToLayer("Cong") || other.gameObject.layer == LayerMask.NameToLayer("Them"))
        {
            snail.transform.Rotate(new Vector3(0.0f, 180, 0.0f));
        }
    }

}
