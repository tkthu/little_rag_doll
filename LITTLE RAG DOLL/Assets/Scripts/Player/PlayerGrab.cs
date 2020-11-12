using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrab : MonoBehaviour
{
    private LineRenderer line; // Reference to LineRenderer
    private Vector3 startPos;    // Start position of line
    private Vector3 endPos;    // End position of line
    private BoxCollider2D col;
    [HideInInspector] public bool caughted = false;
    private void Start()
    {
        line = GetComponent<LineRenderer>();
        col = gameObject.AddComponent<BoxCollider2D>();
        col.isTrigger = true;
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
        col.offset = new Vector2(- lineLength/2, 0) ; // setting position of collider object
    }

    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("EneBullets") && !caughted)
        {
            caughted = true;
            Debug.Log("hit");
            transform.parent.GetComponent<PlayerMovement>().isRetracting = true;
            if (collision.transform.parent != null && collision.transform.parent.parent.CompareTag("Helmet"))
            {
                GameObject bounceBullet = GameManager.GM.poolingManager.getBounceBullets();
                if(bounceBullet != null)
                {
                    transform.position = collision.transform.position;
                    bounceBullet.SetActive(true);
                    bounceBullet.transform.position = transform.position;
                    bounceBullet.transform.SetParent(transform);
                    
                    bounceBullet.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
                    
                }
            }
            else
            {
                transform.position = collision.transform.position;
                collision.attachedRigidbody.bodyType = RigidbodyType2D.Kinematic;
                collision.transform.SetParent(transform);
                
            }
            BounceBulletMovement bounceBulletMovement = collision.gameObject.GetComponent<BounceBulletMovement>();
            if (bounceBulletMovement != null)
                bounceBulletMovement.SetDirection(Vector2.zero);
            StraightBulletMovement straightBulletMovement = collision.gameObject.GetComponent<StraightBulletMovement>();
            if (straightBulletMovement != null)
                straightBulletMovement.SetDirection(Vector2.zero);
        }
        
    }
}
