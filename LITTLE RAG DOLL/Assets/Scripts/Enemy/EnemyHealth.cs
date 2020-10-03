﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
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
        if(HP<=0)
            die();
        Debug.Log("HP = " + HP);
    }

    private void die()
    {
        gameObject.SetActive(false);
    }
}