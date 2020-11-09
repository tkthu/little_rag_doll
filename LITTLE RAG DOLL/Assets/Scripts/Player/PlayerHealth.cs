using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int HPmax;
    [HideInInspector] public int HP;

    public AudioClip eatSpirit;
    public AudioClip playerDeath;
    public AudioClip playerDamage;
    public AudioClip addFullBlood;

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
        AudioManager.instance.PlaySound(playerDamage, transform.position);
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
        //AudioManager.instance.PlaySound(playerDeath, transform.position);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {        
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
            AudioManager.instance.PlaySound(eatSpirit, transform.position);
        }
        // Thêm mạng
        if (other.tag == "Heart")
        {
            HPmax += 1;
            addBlood(HPmax);
            other.gameObject.SetActive(false);
            AudioManager.instance.PlaySound(addFullBlood, transform.position);
        }

    }
}
