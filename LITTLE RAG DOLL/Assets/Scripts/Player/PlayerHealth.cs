using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Heath
{
    public int HPmax;
    [HideInInspector] public int HP;

    public AudioClip eatSpirit;
    public AudioClip playerDeath;
    public AudioClip playerDamage;
    public AudioClip addFullBlood;

    public float immuneTime = 2f;
    private bool immune = false;
    private Animator anim;
    private Animator headAnim;

    private void Awake()
    {
        resetState();
        anim = GetComponent<PlayerMovement>().animator;
        headAnim = GetComponent<PlayerMovement>().headAnimator;
    }

    override
    public void resetState()
    {
        HP = HPmax;
        immune = false;
        transform.Find("GrabShoot").gameObject.SetActive(false);
        transform.Find("Attack").gameObject.SetActive(false);
    }

    override
    public void takeDamage(int damage)
    {
        if (!immune)
        {
            StartCoroutine(hurtBlink());
            AudioManager.instance.PlaySound(playerDamage, transform.position);
            HP = Mathf.Clamp(HP - damage, 0, HPmax);
            if (HP == 0)
                die();
        }
            
    }
    
    IEnumerator hurtBlink()
    {
        //start blink animation
        anim.SetBool("isBlinking",true);
        headAnim.SetBool("isBlinking",true);
        immune = true;

        //wait for imune to end
        yield return new WaitForSeconds(immuneTime);

        //stop blink animton and re-enable collision        
        anim.SetBool("isBlinking", false);
        headAnim.SetBool("isBlinking", false);
        immune = false;
    }

    public void addBlood(int blood)
    {        
        HP = Mathf.Clamp(HP + blood, 0, HPmax);
    }

    private void die()
    {
        Debug.Log("Player is dead");
        GameManager.GM.gameover();
        
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.activeSelf)
        {
            // Hồi máu
            if (other.tag == "smallBlood")
            {
                GameManager.GM.addToCollection(other.gameObject);
                addBlood(1);
                other.gameObject.SetActive(false);
                AudioManager.instance.PlaySound(eatSpirit, transform.position);
            }
            // Hồi full máu
            if (other.tag == "bigBlood")
            {
                GameManager.GM.addToCollection(other.gameObject);
                HP = HPmax;
                other.gameObject.SetActive(false);
                AudioManager.instance.PlaySound(eatSpirit, transform.position);
            }
            // Ăn spirit
            if (other.tag == "Spirit")
            {
                GameManager.GM.addToCollection(other.gameObject);
                other.gameObject.SetActive(false);                
                GameManager.GM.addScore(1);
                AudioManager.instance.PlaySound(eatSpirit, transform.position);
            }
            // Thêm mạng
            if (other.tag == "Heart")
            {
                GameManager.GM.addToCollection(other.gameObject);
                HPmax = Mathf.Clamp(HPmax + 1, 0, 10);
                addBlood(HPmax);
                other.gameObject.SetActive(false);
                AudioManager.instance.PlaySound(addFullBlood, transform.position);
            }
        }
        

    }
}
