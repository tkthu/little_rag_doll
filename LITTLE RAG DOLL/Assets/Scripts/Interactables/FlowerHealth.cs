using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerHealth : MonoBehaviour
{
    public int HPmax;
    private int HP;

    [HideInInspector] public Vector2 respawnPos = Vector2.one * 10000;
    [HideInInspector] public bool isFreezed;
    [HideInInspector] public bool isDeaded;

    public float respawnTime = 5f;
    private void Awake()
    {
        resetStatus();
    }

    public void resetStatus()
    {
        isFreezed = true;
        isDeaded = false;
        HP = HPmax;
    }

    public void respawn()
    {
        HP = HPmax;
        isDeaded = false;
        gameObject.SetActive(true);
        isFreezed = false;
    }

    public void takeDamage(int damage)
    {
        Debug.Log("takeDamage");
        HP = HP - damage;
        if (HP <= 0)
            die();
    }

    private void die()
    {
        transform.position = respawnPos;
        isFreezed = true;

        isDeaded = true;
        gameObject.SetActive(false);
        Invoke("respawn", respawnTime);
    }
}
