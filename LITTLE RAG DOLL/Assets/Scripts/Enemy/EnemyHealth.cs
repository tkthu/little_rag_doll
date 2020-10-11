using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private Vector2 respawnPos;

    public int HPmax;
    private int HP;

    [HideInInspector] public bool isFreezed;

    private void Awake()
    {
        respawnPos = transform.position;
        isFreezed = true;
        HP = HPmax;
    }

    public void respawn()
    {
        HP = HPmax;
        isFreezed = true;
        gameObject.SetActive(true);
    }

    public void takeDamage(int damage)
    {
        HP = HP - damage;
        if (HP <= 0)
            die();
        Debug.Log("HP = " + HP);
    }

    private void die()
    {
        transform.position = respawnPos;
        gameObject.SetActive(false);
    }
}