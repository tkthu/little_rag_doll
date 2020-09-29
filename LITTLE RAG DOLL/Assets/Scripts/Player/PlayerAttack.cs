using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int damage = 1;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemys"))
        {
            Debug.Log("hit enemy (đợi EnemyHealth)");
            // đợi EnemyHealth
            /*
            GameObject Ene = collision.gameObject;
            Ene.GetComponent<EnemyHealth>().takeDamage(damage);
            */
        }
    }
}
