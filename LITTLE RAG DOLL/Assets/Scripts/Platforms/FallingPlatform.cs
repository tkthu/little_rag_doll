using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    public float distance = 0.02f;
    private GameObject player;
    private Transform feet;

    Rigidbody2D rb;
    public float delay = 1;
    bool grounded = false;


    void Start()
    {
        if (GameManager.GM != null)
            player = GameManager.GM.player;
        else
            player = GameObject.FindGameObjectWithTag("Player");
        feet = player.transform.Find("GroundCheck");

        rb = GetComponent<Rigidbody2D>();
    }

        // Update is called once per frame
        void Update()
    {
        // Kiểm tra có trúng layer thềm ko
        Collider2D[] groundInfo2D = Physics2D.OverlapCircleAll(feet.position, distance,LayerMask.GetMask("Them"));
        if (groundInfo2D.Length > 0 && grounded == false)
        {
            for(int i=0; i<groundInfo2D.Length;i++)
            {
                // Kiểm tra nếu trong những thềm đó chứa thềm đang đứng thì chỉ rớt thềm đó thôi.
                if (groundInfo2D[i].transform == gameObject.transform)
                {
                    grounded = true;
                    Invoke("falling", delay);
                    break;
                }
        
            }
        }

    }

    private void falling()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = 1;
    }
}
