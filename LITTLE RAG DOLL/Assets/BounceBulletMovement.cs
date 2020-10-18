using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceBulletMovement : MonoBehaviour
{
    private Vector3 _direction;

    // Update is called once per frame
    public void SetDirection(Vector3 direction)
    {
        _direction = direction;
    }

    void FixedUpdate()
    {
        Vector3 position = transform.position;
        
        position += transform.rotation * _direction;

        transform.position = position;

    }
}
