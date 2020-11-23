using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDurability : Heath
{
    private int state;
    private int stateMax;
    private Sprite[] newSp;
    // Start is called before the first frame update
    void Start()
    {
        state = stateMax = 2;
        newSp = Resources.LoadAll<Sprite>("Sprites/Platforms/Autumnhills_8_Breakable");
        GetComponent<SpriteRenderer>().sprite = newSp[0];
    }

    override
    public void takeDamage(int damage)
    {
        state = Mathf.Clamp(state - damage, -1, stateMax);
        if (state == 1)
            GetComponent<SpriteRenderer>().sprite = newSp[2];
        else if (state == 0)
            GetComponent<SpriteRenderer>().sprite = newSp[6];
        else if (state == -1)
            Destroy(gameObject);
    }
}
