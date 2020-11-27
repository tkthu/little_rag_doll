using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    bool save = false;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!save)
            {
                GameManager.GM.player.GetComponent<PlayerHealth>().addBlood(100);
                GameManager.GM.saveTemp();
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
