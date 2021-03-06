﻿using UnityEngine;

public class LinearMovement : MonoBehaviour
{
    public GameObject[] pos;

    public float speed = 5f;
    public int currentIndex = 0;
    Vector3 nextPos;
    
    // Start is called before the first frame update
    void Start()
    {        
        nextPos = pos[currentIndex].transform.position;
        transform.position = nextPos;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position == pos[currentIndex].transform.position)
            currentIndex = currentIndex + 1;
        if(currentIndex >= pos.Length)
            currentIndex = 0;
        nextPos = pos[currentIndex].transform.position;
        transform.position = Vector3.MoveTowards(transform.position, nextPos, speed * Time.deltaTime);
    }

}
