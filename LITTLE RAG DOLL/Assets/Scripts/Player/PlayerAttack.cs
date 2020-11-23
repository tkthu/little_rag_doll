using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int damage = 1;
    private void OnTriggerEnter2D(Collider2D collision)
    {       
        GameObject go = collision.gameObject;
        if (go.GetComponent<EnemyHealth>() != null)
            go.GetComponent<EnemyHealth>().takeDamage(damage);
        else if (go.GetComponent<FlowerHealth>() != null)
            go.GetComponent<FlowerHealth>().takeDamage(damage);
        else if (collision.tag == "breakable")
        {
            collision.GetComponent<GroundDurability>().takeDamage(damage);
        }
        else if (go.layer == LayerMask.NameToLayer("EneBullets") && (collision.transform.parent.parent == null || collision.transform.parent.parent.tag != "Helmet"))
            go.SetActive(false);

    }
}
