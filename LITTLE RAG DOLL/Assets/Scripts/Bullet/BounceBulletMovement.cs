using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class BounceBulletMovement : MonoBehaviour
{
    private Vector3 _direction;

    // Update is called once per frame
    public void SetDirection(Vector3 direction)
    {
        _direction = direction;
    }

    void Update()
    {
        Vector3 position = transform.position;

        position += transform.rotation * _direction * 0.1f;

        transform.position = position;
        bumpEdgeCamera(position);
    }

    void bumpEdgeCamera(Vector3 position)
    {
        var translation = Input.acceleration.x * 50f;
        position = Camera.main.WorldToScreenPoint(this.transform.position);

        if (position.x <= 0 || position.y <= 0 ||
            position.x > Screen.width || position.y > Screen.height)
        {
            position.x = -Screen.width;
            Debug.Log(position);
        }
    }

    void bounceBullet()
    {
        

    }
}
