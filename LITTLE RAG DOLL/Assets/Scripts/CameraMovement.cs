using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraMovement : MonoBehaviour
{
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPos = player.GetComponent<Transform>().position;
        GetComponent<Transform>().position = new Vector3(playerPos.x,playerPos.y,-10);///
    }
}
