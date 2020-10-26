using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gui : MonoBehaviour
{
    public int health;
    public int NumOfHearts;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    // Start is called before the first frame update
    void Start()
    {
        NumOfHearts = GameManager.GM.player.GetComponent<PlayerHealth>().HPmax;
        health = GameManager.GM.player.GetComponent<PlayerHealth>().HP;

    }

    // Update is called once per frame
    void Update()
    {
        NumOfHearts = GameManager.GM.player.GetComponent<PlayerHealth>().HPmax;
        health = GameManager.GM.player.GetComponent<PlayerHealth>().HP;
        if (health > NumOfHearts)
        {
            health = NumOfHearts;
        }
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
            if (i < NumOfHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }

    
}
