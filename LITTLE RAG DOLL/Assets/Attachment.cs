using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attachment : MonoBehaviour
{
    public GameObject attachment;
    // Start is called before the first frame update
    void Start()
    {
        attachment = Instantiate(attachment, transform.localPosition, Quaternion.identity);
        attachment.transform.SetParent(transform.parent);
    }

    // Update is called once per frame
    void Update()
    {
        attachment.transform.localPosition = transform.localPosition;
    }
}
