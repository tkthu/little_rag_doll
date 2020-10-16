using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScalePlate : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {

        GetComponentInParent<ScalePlatform>().addGameObject(collision.gameObject);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        GetComponentInParent<ScalePlatform>().removeGameObject(collision.gameObject);     
    }
}
