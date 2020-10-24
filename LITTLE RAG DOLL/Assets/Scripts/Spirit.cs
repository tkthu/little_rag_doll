using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spirit : MonoBehaviour
{
    private int count = 0;
    public Text scoreSpirit;

    void Start()
    {
        scoreSpirit.text = "Spirit: ";
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Spirit");
            count++;
            gameObject.SetActive(false);
            ShowScore();
        }

    }

    public void ShowScore()
    {
        scoreSpirit.text = "Spirit: " + count;
    }

}
