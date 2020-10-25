using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int HPmax;
    public int HP;

    private void Awake()
    {
        HP = HPmax;
    }

    public void takeDamage(int damage)
    {
        HP = HP - damage;
        if (HP <= 0)
            die();
        //Debug.Log("Player HP = " +HP);
    }

    public void addBlood(int blood)
    {
        HP = HP + blood;
        if (HP > HPmax)
            HP = HPmax;
        //Debug.Log("Player HP = " +HP);
    }

    private void die()
    {
        Debug.Log("Player is dead");
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
            addBlood(1);
            other.gameObject.SetActive(false);
        }
    }
}
