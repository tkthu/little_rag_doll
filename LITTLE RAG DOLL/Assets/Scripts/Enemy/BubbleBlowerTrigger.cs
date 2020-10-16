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

    public float fireRate = 2f;
    private float timeRate;
    private GameObject bulletStraight;

    private bool m_FacingRight;
    private EnemyHealth eneHealth;

    private void Start()
    {
        anim = GetComponent<Animator>();
        if (GameManager.GM != null)
            player = GameManager.GM.player;
        else
            player = GameObject.FindGameObjectWithTag("Player");

        GunStraightBullet = transform.Find("GunStraightBullet").transform;
        timeRate = Time.time;

        m_FacingRight = false;
        eneHealth = GetComponent<EnemyHealth>();
    }

    private void Update()
    {
        if (eneHealth != null && !eneHealth.isFreezed)//neu Enemy khong bi dong cung
        {
            if (player.transform.position.x < transform.position.x && m_FacingRight)
                flip();
            else if (player.transform.position.x > transform.position.x && !m_FacingRight)
                flip();

            if (Time.time > timeRate)
            {
                bulletStraight = GameManager.GM.poolingManager.getStraightBullets();
                if (bulletStraight != null)
                {
                    anim.SetTrigger("Shoot");
                    bulletStraight.transform.position = GunStraightBullet.position;
                    bulletStraight.transform.rotation = GunStraightBullet.rotation;
                    bulletStraight.SetActive(true);

                    timeRate = Time.time + fireRate;
                    Vector2 dir = player.transform.position - bulletStraight.transform.position;
                    bulletStraight.GetComponent<StraightBulletMovement>().SetDirection(new Vector2(dir.x, 0));//ban ngang
                }
            }
        }
    }
    private void flip()
    {
        m_FacingRight = !m_FacingRight;
        Vector2 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
