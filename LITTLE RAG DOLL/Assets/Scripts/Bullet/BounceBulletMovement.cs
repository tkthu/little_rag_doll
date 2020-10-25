using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class BounceBulletMovement : MonoBehaviour
{
    private Vector3 _direction;
    private bool firstBounce = true;
    private float speed = 2f;
    private float aliveTime = 10f;
    private Transform oiginalParent;

    private void Start()
    {
        Debug.Log("Start " + transform.parent);
             
    }

    // Update is called once per frame
    public void SetDirection(Vector3 direction)
    {
        _direction = direction;
    }

    void Update()
    {
        Vector3 position = transform.position;

        position += _direction * speed * Time.deltaTime;

        transform.position = position;
        bumpEdgeCamera();
    }

    void bumpEdgeCamera()
    {
        Vector3 position = Camera.main.WorldToScreenPoint(transform.position);

        if (position.x < 0 || position.y < 0 ||
            position.x > Screen.width || position.y > Screen.height)
        {
            if (firstBounce)
            {
                var rot = Quaternion.AngleAxis(135, Vector3.forward);
                var lDirection = rot * _direction;
                var wDirection = transform.TransformDirection(lDirection);

                _direction = wDirection;
                transform.SetParent(Camera.main.transform);
                firstBounce = false;
            }
            else
            {
                if(position.x < 0 && _direction.x < 0)
                {
                    _direction.x = -_direction.x;
                }
                else if (position.x > Screen.width && _direction.x > 0)
                {
                    _direction.x = -_direction.x;
                }
                    
                if(position.y < 0 && _direction.y < 0)
                {
                    _direction.y = -_direction.y;
                }
                else if (position.y > Screen.height && _direction.y > 0)
                {
                    _direction.y = -_direction.y;
                }
                    
            }
                
        }
    }

    void outOfTime()
    {
        firstBounce = true;
        gameObject.SetActive(false);
        transform.SetParent(oiginalParent);
    }

    public void activate()
    {
        oiginalParent = transform.parent;
        Invoke("outOfTime", aliveTime);        
        gameObject.SetActive(true);
    }
}
