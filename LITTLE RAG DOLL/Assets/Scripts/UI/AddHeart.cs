using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddHeart : MonoBehaviour
{
    public GameObject heart6, heart7, heart8, heart9, heart10;
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            Gui.health += 1;
            gameObject.SetActive(false);
        }
        Gui.health -= 1;
    }

    /*void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemies"))
        {
            Gui.health -= 1;
            
        }
    }*/

}
