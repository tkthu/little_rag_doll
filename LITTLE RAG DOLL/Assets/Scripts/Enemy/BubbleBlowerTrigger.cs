using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BubbleBlowerTrigger : MonoBehaviour
{
    //Xu li chuyen doi animation
    private Animator anim;
    private GameObject player;

    private Transform GunStraightBullet;

    private float fireRate;
    private float timeRate;
    public GameObject bulletStraight;

    private void Start()
    {
        anim = GetComponent<Animator>();
        if (GameManager.GM != null)
            player = GameManager.GM.player;
        else
            player = GameObject.FindGameObjectWithTag("Player");

        GunStraightBullet = transform.Find("GunStraightBullet").transform;
        fireRate = 1f;
        timeRate = Time.time;
    }

    private void Update()
    {
        Vector2 direction = player.transform.position - transform.position;

        if (Time.time > timeRate)
        {
            bulletStraight = GameManager.GM.getStraightBullets();
            if (bulletStraight != null)
            {
                anim.SetTrigger("Shoot");
                bulletStraight.transform.position = GunStraightBullet.position;
                bulletStraight.transform.rotation = GunStraightBullet.rotation;
                bulletStraight.SetActive(true);

                timeRate = Time.time + fireRate;
                Vector2 dir = player.transform.position - bulletStraight.transform.position;
                bulletStraight.GetComponent<StraightBulletMovement>().SetDirection(dir);
            }
        }
    }
}
