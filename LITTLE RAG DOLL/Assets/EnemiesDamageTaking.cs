using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesDamageTaking : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Enemies"))
        {
            GameManager.GM.player.GetComponent<PlayerHealth>().takeDamage(1);
        }

    }
}
