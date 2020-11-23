using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Attachment : MonoBehaviour
{
    public GameObject attachment;
    // Start is called before the first frame update
    void Start()
    {
        attachment = Instantiate(attachment, transform.localPosition, Quaternion.identity);
        attachment.transform.SetParent(transform);
        attachment.transform.localPosition = Vector2.zero;
    }
}
