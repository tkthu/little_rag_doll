using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailMovement : MonoBehaviour
{
    public float speed = 0.4f;
    void Update()
    {
        transform.Translate(-1*speed * Time.deltaTime, 0, 0);
    }
}
