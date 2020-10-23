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
    private Dictionary<GameObject, Transform> dictParent = new Dictionary<GameObject, Transform>();
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
        addGameObject(other.gameObject, other.transform.parent);
        other.transform.parent = transform;
    }

    void OnCollisionExit2D(Collision2D other)
    {
        Transform parent = removeGameObject(other.gameObject);
        other.transform.parent = parent;
    }
    public void addGameObject(GameObject goKey, Transform transParent)
    {
        if (!dictParent.ContainsKey(goKey))
            dictParent.Add(goKey, transParent);
    }

    public Transform removeGameObject(GameObject goKey)
    {
        Transform transParent = null;
        if (dictParent.ContainsKey(goKey))
        {
            transParent = dictParent[goKey];
            dictParent.Remove(goKey);
        }
        return transParent;
    }

}
