using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDurability : MonoBehaviour
{
    int state;
    int stateMax;
    Sprite[] newSp;
    // Start is called before the first frame update
    void Start()
    {
        state = stateMax = 2;
        newSp = Resources.LoadAll<Sprite>("Sprites/Platforms/Autumnhills_8_Breakable");
        GetComponent<SpriteRenderer>().sprite = newSp[0];
    }

    public void takeDamage(int damage)
    {
        state = Mathf.Clamp(state - damage, -1, stateMax);
        if (state == 1)
        {
            newSp = Resources.LoadAll<Sprite>("Sprites/Platforms/Autumnhills_8_Breakable");
            GetComponent<SpriteRenderer>().sprite = newSp[2];
            Debug.Log("breakable 1");
        }
        else if (state == 0)
        {
            newSp = Resources.LoadAll<Sprite>("Sprites/Platforms/Autumnhills_8_Breakable");
            GetComponent<SpriteRenderer>().sprite = newSp[6];
            Debug.Log("breakable 0");
        }
        else if (state == -1)
        {// nó ko chịu Destoy
            Destroy(gameObject);
        }
    }
}
