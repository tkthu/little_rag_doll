using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightBulletGun : MonoBehaviour
{
    private float fireRate;
    private float timeRate;
    private GameObject player;

    public GameObject bulletStraight;
    public BubbleBlowerTrigger BubbleTrigger;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.GM.startGame();
        fireRate = 1f;
        timeRate = Time.time;
        player = GameObject.Find("Player");
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        //Tạo đạn ra cho enemy
        if(BubbleTrigger.statusAnimation == true)
        {
            if (Time.time > timeRate)
            {
                bulletStraight = GameManager.GM.getStraightBullets();
                if (bulletStraight != null)
                {
                    bulletStraight.transform.position = transform.position;
                    bulletStraight.transform.rotation = transform.rotation;
                    bulletStraight.SetActive(true);

                    timeRate = Time.time + fireRate;
                    Vector2 direction = player.transform.position - bulletStraight.transform.position;
                    bulletStraight.GetComponent<StraightBulletMovement>().SetDirection(direction);
                }
            }
        }
    }
}
