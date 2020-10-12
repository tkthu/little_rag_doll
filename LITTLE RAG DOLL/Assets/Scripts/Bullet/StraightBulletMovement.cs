using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightBulletMovement : MonoBehaviour
{
    private float speed = 4;
    private Vector2 _direction;

    public void SetDirection(Vector2 direction)
    {
        _direction = direction.normalized;
    }

    void FixedUpdate()
    {
        Vector2 position = transform.position;
        position += _direction * speed * 0.1f;

        transform.position = position;
        if(position.x > 0)
           transform.localScale = new Vector2(-1,1);
        else
            transform.localScale = new Vector2(1, 1);

    }

    void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }
}
