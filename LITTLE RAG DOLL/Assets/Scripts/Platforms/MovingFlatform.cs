using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingFlatform : MonoBehaviour
{
    public GameObject[] pos;

    public float speed;
    public int currentIndex;
    //public GameObject movingPlatform;
    Vector3 nextPos;
    private List<GameObject> listGO = new List<GameObject>();
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

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.activeSelf && other.enabled)
        {
            addGameObject(other.gameObject);
            other.transform.parent = transform;
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.activeSelf)
        {
            removeGameObject(other.gameObject);
            PoolingItem pItem = other.gameObject.GetComponent<PoolingItem>();
            if(pItem != null)
            {
                pItem.resetParent();
            }

        }
        
    }
    public void addGameObject(GameObject go)
    {
        if (!listGO.Contains(go))
            listGO.Add(go);
    }

    public void removeGameObject(GameObject go)
    {
        if (listGO.Contains(go))
            listGO.Remove(go);
    }

}
