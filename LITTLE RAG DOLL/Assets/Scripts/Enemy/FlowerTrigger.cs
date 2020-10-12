using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerTrigger : MonoBehaviour
{
    private Transform GunBounceBullet;
    //Time rate
    private float fireRate;
    private float timeRate;
    public GameObject bulletBounce;
    //Xu li dan bay 4 huong
    private bool left;
    private bool down;
    private bool right;
    private bool up;
    private float speedDirection = 0.2f;

    private Vector3 dir;

    //Animation
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        GunBounceBullet = transform.Find("GunBounceBullet").transform;
        anim = GetComponent<Animator>();
        fireRate = 1f;
        timeRate = Time.time;
        left = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > timeRate)
        {
            bulletBounce = GameManager.GM.getBounceBullets();
            if (bulletBounce != null)
            {
                anim.SetTrigger("Shoot");
                bulletBounce.transform.position = GunBounceBullet.position;
                bulletBounce.transform.rotation = GunBounceBullet.rotation;
                bulletBounce.SetActive(true);

                timeRate = Time.time + fireRate;
                bulletBounce.GetComponent<BounceBulletMovement>().SetDirection(DirDirection(dir));
            }
        }
    }

    Vector3 DirDirection(Vector3 dir)
    {
        if (left == true)
        {
            dir = Vector3.left * speedDirection;
            down = true;
            left = false;
        }
        else if (down == true)
        {
            dir = Vector3.down * speedDirection;
            right = true;
            down = false;
        }
        else if (right == true)
        {
            dir = Vector3.right * speedDirection;
            up = true;
            right = false;
        }
        else if (up == true)
        {
            dir = Vector3.up * speedDirection;
            left = true;
            up = false;
        }
        return dir;
    }
}
