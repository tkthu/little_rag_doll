using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingFlatform : MonoBehaviour
{
    private GameObject player;
    public GameObject[] pos;

    public float speed;
    public int currentIndex;
    //public GameObject movingPlatform;
    Vector3 nextPos;

    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.GM != null)
            player = GameManager.GM.player;
        else
            player = GameObject.FindGameObjectWithTag("Player");

        
        nextPos = pos[currentIndex].transform.position;
        transform.position = nextPos;

    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position == pos[currentIndex].transform.position)
        {
            currentIndex = currentIndex + 1;
            
        }
        if(currentIndex >= pos.Length)
        {
            currentIndex = 0;
            
        }
        nextPos = pos[currentIndex].transform.position;
        transform.position = Vector3.MoveTowards(transform.position, nextPos, speed * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject == player)
        {
            //Debug.Log("Up");
            player.transform.parent = transform;
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject == player)
        {
            //Debug.Log("Down");
            player.transform.parent = null;
        }
    }

}
