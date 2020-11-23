using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    bool save = false;
    public AudioClip checkPoint;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //AudioManager.instance.PlaySound(checkPoint, transform.position);
            if (!save)
            {
                GameManager.GM.player.GetComponent<PlayerHealth>().addBlood(100);
                SaveSystem.saveData(GameManager.GM);
                Debug.Log("Saving...");
                save = true;
            }
            
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            save = false;
        }
    }
}
