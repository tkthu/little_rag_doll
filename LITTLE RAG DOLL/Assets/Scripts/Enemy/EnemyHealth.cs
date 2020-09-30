using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int HPmax;
    public int HP;

    void takeDamage(int damage)
    {
        HP = HP - damage;
        if(HP<=0)
        {
            die();
        }
    }

    void die()
    {
        gameObject.SetActive(false);
    }
}
