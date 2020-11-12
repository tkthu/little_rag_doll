using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrab : MonoBehaviour
{
    private LineRenderer line; // Reference to LineRenderer
    private Vector3 startPos;    // Start position of line
    private Vector3 endPos;    // End position of line
    private BoxCollider col;
    private void Start()
    {
        line = GetComponent<LineRenderer>();
        col = new GameObject("Collider").AddComponent<BoxCollider>();
        col.transform.parent = line.transform; // Collider is added as child object of line
    }
    private void Update()
    {
        line.SetPosition(0, GameManager.GM.player.transform.position);
        line.SetPosition(1, transform.position);

        startPos = GameManager.GM.player.transform.position;
        endPos = transform.position;

        addColliderToLine();
    }
    private void addColliderToLine()
    {        

        float lineLength = Vector3.Distance(startPos, endPos); // length of line
        col.size = new Vector3(lineLength, 0.1f, 1f); // size of collider is set where X is length of line, Y is width of line, Z will be set as per requirement
        Vector3 midPoint = (startPos + endPos) / 2;
        col.transform.position = midPoint; // setting position of collider object
    }

    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("EneBullets"))
        {
            Debug.Log("hit");
            transform.parent.GetComponent<PlayerMovement>().isRetracting = true;
            if (collision.transform.parent != null && collision.transform.parent.parent.CompareTag("Helmet"))
            {
                GameObject bounceBullet = GameManager.GM.poolingManager.getBounceBullets();
                if(bounceBullet != null)
                {
                    bounceBullet.SetActive(true);
                    bounceBullet.transform.position = transform.position;
                    bounceBullet.transform.SetParent(transform);
                }
            }
            else
            {
                collision.transform.SetParent(transform);
            }
            
        }
        
    }
}
