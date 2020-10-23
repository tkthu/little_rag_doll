using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class BounceBulletMovement : MonoBehaviour
{
    private Vector3 _direction;

    //private Rigidbody2D rb;

    private Vector3 initialVelocity;

    private float minVelocity = 10f;

    private Vector3 lastFrameVelocity;
    private Rigidbody2D rb;

    // Update is called once per frame
    public void SetDirection(Vector3 direction)
    {
        _direction = direction;
    }

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = initialVelocity;
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    var direction = Vector3.Reflect(_direction, collision.contacts[0].normal);
    //    this.transform.rotation = Quaternion.LookRotation(new Vector3(direction.x, direction.y, 0.0f));
    //}

    void Update()
    {
        lastFrameVelocity = rb.velocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Bounce(collision.contacts[0].normal);
    }

    private void Bounce(Vector3 collisionNormal)
    {
        var speed = lastFrameVelocity.magnitude;
        var direction = Vector3.Reflect(_direction, collisionNormal);

        Debug.Log("Out Direction: " + direction);
        rb.velocity = direction * Mathf.Max(speed, minVelocity);
    }
}
