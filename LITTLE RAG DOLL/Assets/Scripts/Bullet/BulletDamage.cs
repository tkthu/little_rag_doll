using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDamage : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(gameObject.layer == LayerMask.NameToLayer("PlayerBullets"))// PlayerBullets khong collide voi Player duoc. Vi Collider Matrix
        {
            Heath heath = collision.GetComponent<Heath>();
            if (heath != null)
                heath.takeDamage(1);
            if(collision.gameObject.layer == LayerMask.NameToLayer("EneBullets") && (collision.transform.parent.parent == null || collision.transform.parent.parent.tag != "Helmet"))
                collision.gameObject.SetActive(false);
        }else if (gameObject.layer == LayerMask.NameToLayer("EneBullets"))
        {
            if(collision.tag != "Arm")
            {
                Heath heath = collision.GetComponent<PlayerHealth>();
                if (heath != null)
                    heath.takeDamage(1);
            }
            
        }
    }
}
