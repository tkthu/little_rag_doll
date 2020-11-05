using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelmetTrigger : MonoBehaviour
{
    //Xu li chuyen doi animation
    private Animator anim;
    //Xac dinh bullet tu player hay enemy
    private bool isShooting;

    //firerate time
    public float fireRate = 5f;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isShooting && collision.gameObject.layer == LayerMask.NameToLayer("EneBullets"))
        {
            isShooting = true;
            anim.SetBool("ShootFromEnemy", true);
            Invoke("stopAnim", fireRate);
        }
        if (!isShooting && collision.gameObject.layer == LayerMask.NameToLayer("PlayerBullets"))
        {
            isShooting = true;
            anim.SetBool("ShootFromPlayer", true);
            Invoke("stopAnim", fireRate);
        }
    }

    private void stopAnim()
    {
        isShooting = false;
        anim.SetBool("ShootFromPlayer", false);
        anim.SetBool("ShootFromEnemy", false);
    }
}
