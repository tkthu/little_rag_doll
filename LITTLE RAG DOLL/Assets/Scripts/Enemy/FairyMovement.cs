using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FairyMovement : MonoBehaviour
{
    public float speed = 0.65f;
    public float stoppingDistance = 0.3f;

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
    void Update()
    {
        if (Vector2.Distance(transform.position, target.position) > stoppingDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
    }

    void FixedUpdate()
    {
        fairyBody.AddForce(moveDirection * 50f);
    }
}
