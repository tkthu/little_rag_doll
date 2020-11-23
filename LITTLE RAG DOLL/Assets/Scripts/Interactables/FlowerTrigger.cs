using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerTrigger : MonoBehaviour
{
    private Transform GunBounceBullet;
    //Time rate
    public float fireRate = 5f;
    private float timeRate;
    private GameObject bulletBounce;
    //Xu li dan bay 4 huong
    private bool left;
    private bool down;
    private bool right;
    private bool up;

    private Vector3 dir;
    //Animation
    private Animator anim;
    private FlowerHealth health;

    public AudioClip flowerAttack;

    // Start is called before the first frame update
    void Start()
    {
        GunBounceBullet = transform.Find("GunBounceBullet").transform;
        anim = GetComponent<Animator>();
        timeRate = Time.time;
        left = true;
        health = GetComponent<FlowerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health != null && !health.isFreezed)
        {
            if (Time.time > timeRate)
            {
                AudioManager.instance.PlaySound(flowerAttack, transform.position);
                bulletBounce = GameManager.GM.poolingManager.getBounceBullets();
                if (bulletBounce != null)
                {
                    anim.SetTrigger("Shoot");
                    bulletBounce.transform.position = GunBounceBullet.position;
                    bulletBounce.transform.rotation = Quaternion.identity;


                    timeRate = Time.time + fireRate;
                    bulletBounce.GetComponent<BounceBulletMovement>().SetDirection(DirDirection(dir));
                    bulletBounce.GetComponent<BounceBulletMovement>().activate();

                }
            }
        }
    }

    Vector3 DirDirection(Vector3 dir)
    {
        if (left == true)
        {
            dir = Vector3.left;
            down = true;
            left = false;
        }
        else if (down == true)
        {
            dir = Vector3.down;
            right = true;
            down = false;
        }
        else if (right == true)
        {
            dir = Vector3.right;
            up = true;
            right = false;
        }
        else if (up == true)
        {
            dir = Vector3.up;
            left = true;
            up = false;
        }
        return dir;
    }
}
