using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public Vector2 respawnPos { get; set; }

    public int HPmax;
    private int HP;

    [HideInInspector] public bool isFreezed;
    [HideInInspector] public bool isDeaded;

    private void Awake()
    {
        respawnPos = transform.position;
        isFreezed = true;
        isDeaded = false;
        HP = HPmax;
    }

    public void respawn()
    {
        HP = HPmax;
        isDeaded = false;
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
        isFreezed = true;

        isDeaded = true;
        gameObject.SetActive(false);
    }
}