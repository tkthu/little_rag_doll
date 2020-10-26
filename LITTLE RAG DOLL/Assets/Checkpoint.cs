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
            GameManager.GM.player.GetComponent<PlayerHealth>().addBlood(100);
            if ((Input.GetKey(KeyCode.Keypad1) || Input.GetKey(KeyCode.Keypad2) || Input.GetKey(KeyCode.Keypad3)) && !save)
            {
                int filenumber = 1;
                if (Input.GetKey(KeyCode.Keypad1))
                    filenumber = 1;
                else if (Input.GetKey(KeyCode.Keypad2))
                    filenumber = 2;
                else if (Input.GetKey(KeyCode.Keypad3))
                    filenumber = 3;

                SaveSystem.saveData(filenumber);
                Debug.Log("Saving...");
                save = true;
            }
        }
        

        /*
		if (Input.GetButton("Interact") && !save)
        {
            SaveSystem.saveData(1);
            save = true;
            Debug.Log("Saving...");
        }
        */ 
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            save = false;
        }
    }
}
