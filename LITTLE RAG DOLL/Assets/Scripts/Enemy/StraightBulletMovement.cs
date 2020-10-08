using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightBulletMovement : MonoBehaviour
{
    private float speed = 1;
    // Start is called before the first frame update
    void Update()
    {
        GetComponent<Rigidbody2D>().velocity = Vector3.left * speed;
    }
}
