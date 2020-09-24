using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatMovement : MonoBehaviour
{
    public float speed = 0.65f;
    public float stoppingDistance = 0.3f;

    private Transform target;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        if(GameManager.GM != null)
            player = GameManager.GM.player;
        else
            player = GameObject.FindGameObjectWithTag("Player");
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
}
