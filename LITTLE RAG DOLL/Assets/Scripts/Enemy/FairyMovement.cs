using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FairyMovement : MonoBehaviour
{
    public float speed = 0.65f;
    private Rigidbody2D fairyBody;
    private Transform target;
    private GameObject player;
    private Vector2 moveDirection;

    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.GM != null)
            player = GameManager.GM.player;
        else
        {
            player = GameObject.FindGameObjectWithTag("Player");
            fairyBody = GetComponent<Rigidbody2D>();
        }
        target = player.transform;
    }
        // Update is called once per frame

    void FixedUpdate()
    {
        moveDirection = (target.position - transform.position).normalized * speed * Time.deltaTime;
        fairyBody.AddForce(moveDirection * 50f);
    }
}
