using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleBlowerTrigger : MonoBehaviour
{
    //Quan sát target, player
    private Transform target;
    private GameObject player;

    //Xử lí bắn đạn
    private float speed = 0.5f;
    private GameObject bullet;

    public float delayTime = 0.5f;

    //Xử lí chuyển đổi annimator
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.GM.startGame();
        if (GameManager.GM != null)
            player = GameManager.GM.player;
        else
            player = GameObject.FindGameObjectWithTag("Player");
        target = player.transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Xử lí bắn đạn cho enemy
        bullet = GameManager.GM.getStraightBullets();
        if (bullet != null)
        {
            bullet.transform.position = target.transform.position;
            bullet.transform.rotation = target.transform.rotation;
            bullet.SetActive(true);
        }
    }
}
