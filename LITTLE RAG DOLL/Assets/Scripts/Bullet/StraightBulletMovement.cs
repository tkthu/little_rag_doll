using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightBulletMovement : MonoBehaviour
{
    public float speed = 2;
    private Vector2 _direction;

    public void SetDirection(Vector2 direction)
    {
        _direction = direction.normalized;
    }

    void FixedUpdate()
    {
        Vector2 position = transform.position;
        position += _direction * speed * Time.fixedDeltaTime;

        transform.position = position;
        if(_direction.x > 0)
           transform.localScale = new Vector2(-1,1);
        else
            transform.localScale = new Vector2(1, 1);

    }

    void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }
}
