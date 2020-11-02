using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    private PlatformEffector2D effector;
    private float waitTime;

    private List<GameObject> listGO = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        effector = GetComponent<PlatformEffector2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Press S and K to go down
        
        if(Input.GetButton("Duck") && Input.GetButton("Jump"))
        {
            effector.rotationalOffset = 180f;
           
        }
        else if(effector.rotationalOffset != 0)
        {
            effector.rotationalOffset = 0;
        }
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
            if (pItem != null)
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
