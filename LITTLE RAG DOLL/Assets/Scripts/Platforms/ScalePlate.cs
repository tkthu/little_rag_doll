using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScalePlate : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GetComponentInParent<ScalePlatform>().addGameObject(collision.gameObject,collision.transform.parent,gameObject.tag);
        collision.transform.parent = transform;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        Transform parent = GetComponentInParent<ScalePlatform>().removeGameObject(collision.gameObject, gameObject.tag);
        collision.transform.parent = parent;
    }
}
