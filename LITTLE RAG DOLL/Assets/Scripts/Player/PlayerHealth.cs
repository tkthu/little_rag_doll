using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int HPmax;
    private int HP;

    private void Awake()
    {
        HP = HPmax;
    }

    public void takeDamage(int damage)
    {
        HP = HP - damage;
        if (HP <= 0)
            die();
    }

    private void die()
    {
        Debug.Log("Player is dead");
    }
}
