using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int HPmax;
    [HideInInspector] public int HP;

    private void Awake()
    {
        resetState();
    }
    public void resetState()
    {
        HP = HPmax;
    }
    public void takeDamage(int damage)
    {
        HP = Mathf.Clamp(HP - damage,0,HPmax);
        if (HP == 0)
            die();
            
    }

    public void addBlood(int blood)
    {
        HP = Mathf.Clamp(HP + blood, 0, HPmax);
    }

    private void die()
    {
        Debug.Log("Player is dead");
        //GameManager.GM.gameover();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.GetMask("Enemies"))  // Cái này ko chạy
        {
            Debug.Log("damage");
            takeDamage(1);
        }
        // Hồi máu
        if (other.tag == "smallBlood")
        {
            Debug.Log("smallBlood");
            addBlood(1);
            other.gameObject.SetActive(false);
        }
        // Hồi full máu
        if (other.tag == "bigBlood")
        {
            Debug.Log("bigBlood");
            HP = HPmax;
            other.gameObject.SetActive(false);
        }
        // Ăn spirit
        if (other.tag == "Spirit")
        {
            GameManager.GM.addScore(1);
            other.gameObject.SetActive(false);
        }
        // Thêm mạng
        if (other.tag == "Heart")
        {
            Debug.Log("Heart");
            HPmax += 1;
            addBlood(HPmax);
            other.gameObject.SetActive(false);
        }
    }
}
