using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    bool save = false;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetButton("Interact") && !save)
        {
            SaveSystem.saveData();
            save = true;
            Debug.Log("Saving...");
        }
            
    }
}
