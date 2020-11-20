using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDamage : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Arm")
        {
            Heath heath = collision.GetComponent<Heath>();
            if (heath != null)
                heath.takeDamage(1);
        }
        if (gameObject.layer == LayerMask.NameToLayer("PlayerBullets") && collision.gameObject.layer == LayerMask.NameToLayer("EneBullets") && (collision.transform.parent.parent == null || collision.transform.parent.parent.tag != "Helmet"))
        {
            collision.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
