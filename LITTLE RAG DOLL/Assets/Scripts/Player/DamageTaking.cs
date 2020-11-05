using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTaking : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Enemies"))
        {
            GameManager.GM.player.GetComponent<PlayerHealth>().takeDamage(1);
        }
        // Đụng lưỡi cưa
        if (collision.tag == "Saw")
        {
            GameManager.GM.player.GetComponent<PlayerHealth>().takeDamage(1);
        }

    }
}
