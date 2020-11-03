using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelmetTrigger : MonoBehaviour
{
    //Xu li chuyen doi animation
    private Animator anim;
    //Xac dinh bullet tu player hay enemy
    private bool bulletPlayer;
    private bool bulletEnemy;

    //firerate time
    public float fireRate = 5f;

    private float timeRate;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

        timeRate = Time.time;
        bulletPlayer = false;
        bulletEnemy = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > timeRate)
        {
            if (bulletPlayer == true)
            {
                anim.SetTrigger("ShootFromPlayer");
                timeRate = Time.time + fireRate;
            }

            if (bulletEnemy == true)
            {
                anim.SetTrigger("ShootFromEnemy");
                timeRate = Time.time + fireRate;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("BounceBullet") || collision.gameObject.tag.Equals("StraightBullet"))
        {
            bulletEnemy = true;
        }
        if (collision.gameObject.tag.Equals("PlayerBounceBullet") || collision.gameObject.tag.Equals("PlayerStraightBullet")
            || collision.gameObject.tag.Equals("PlayerExplodeBullet") || collision.gameObject.tag.Equals("PlayerPistil") || collision.gameObject.tag.Equals("PlayerBattery"))
        {
            bulletPlayer = true;
        }
    }
}
