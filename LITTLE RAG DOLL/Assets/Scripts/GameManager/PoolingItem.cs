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
        transform.parent = originalParent;
        gameObject.SetActive(false);
        gameObject.transform.position = Vector2.one * 10000;
        EnemyHealth eneHealth = gameObject.GetComponent<EnemyHealth>();
        if (eneHealth != null)
            eneHealth.resetStatus();
        FlowerHealth flowerHealth = gameObject.GetComponent<FlowerHealth>();
        if (flowerHealth != null)
            flowerHealth.resetStatus();
    }
    public void resetParent()
    {
        transform.SetParent(originalParent);
    }
}
