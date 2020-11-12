using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolingItem : MonoBehaviour
{
    private Transform originalParent;
    public void setOriginalParent(Transform parent)
    {
        originalParent = parent;
        resetParent();
    }
    public void resetState()
    {
        transform.SetParent(originalParent);
        gameObject.SetActive(false);
        gameObject.transform.position = Vector2.one * 10000;
        Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
        if (rb != null)
            rb.bodyType = RigidbodyType2D.Dynamic;
        EnemyHealth eneHealth = gameObject.GetComponent<EnemyHealth>();
        if (eneHealth != null)
            eneHealth.resetStatus();
        FlowerHealth flowerHealth = gameObject.GetComponent<FlowerHealth>();
        if (flowerHealth != null)
            flowerHealth.resetStatus();
        BounceBulletMovement bounceBulletMovement = gameObject.GetComponent<BounceBulletMovement>();
        if (bounceBulletMovement != null)
            bounceBulletMovement.SetDirection(Vector2.zero);
        StraightBulletMovement straightBulletMovement = gameObject.GetComponent<StraightBulletMovement>();
        if (straightBulletMovement != null)
            straightBulletMovement.SetDirection(Vector2.zero);
    }
    public void resetParent()
    {
        transform.SetParent(originalParent);
    }
}
