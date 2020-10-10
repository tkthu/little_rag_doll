using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleBlowerTrigger : MonoBehaviour
{
    //Xu li chuyen doi animation
    private Animator anim;
    public bool statusAnimation;
    private GameObject player;

    private void Start()
    {
        statusAnimation = false;
        anim = GetComponent<Animator>();
        player = GameObject.Find("Player");
    }

    private void Update()
    {
        Vector2 direction = player.transform.position - transform.position;
        if (direction.x < 0 || direction.y < 0)
        {
            anim.SetBool("PlayerRange", true);
            statusAnimation = true;
        } else
        {
            anim.SetBool("PlayerRange", false);
        }
    }
}
