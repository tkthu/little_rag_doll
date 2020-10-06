using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailMovement : MonoBehaviour
{
    public float speed = 0.4f;
    private EnemyHealth eneHealth;
    void Start()
    {
        eneHealth = GetComponent<EnemyHealth>();
    }
    void Update()
    {
        if (eneHealth != null && !eneHealth.isFreezed)//neu Player khong bi dong cung
        {            
            transform.Translate(-1 * speed * Time.deltaTime, 0, 0);
        }
    }
}
