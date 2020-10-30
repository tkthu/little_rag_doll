using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Attachment : MonoBehaviour
{
    private Transform root;
    private Transform end;
    public GameObject attachment;
    private float slowDown = 0.1f;

    private float len;
    private float angle;
    private float aVel;
    private float aAcc;


    // Start is called before the first frame update
    void Start()
    {
        root = transform.Find("ChainLink_root");
        end = transform.Find("ChainLink_end");
        attachment = Instantiate(attachment, end.localPosition, Quaternion.identity);
        len = Vector2.Distance(end.localPosition, root.localPosition);
        float val = Mathf.Clamp(end.localPosition.x / len, -1, 1);
        angle = Mathf.Asin(val); ;

        aVel = 0;
        aAcc = 0;

        //drawChain();
    }
    /*
    private void drawChain()
    {
        
        Transform nextChain = end;
        while(Vector2.Distance(nextChain.position, root.position) > 2)
        {
            Vector2 pos2Create = end.

            GameObject objectToPool = Resources.Load<GameObject>("Prefabs/Platfroms/ChainLink");
            GameObject tmp = Instantiate(objectToPool);
            for (int i = 0; i < amountToPool; i++)
            {
                tmp = Instantiate(objectToPool);
                tmp.SetActive(false);
                tmp.AddComponent<PoolingItem>().setOriginalParent(groupObject.transform);
                pooledObjects.Add(tmp);
            }
            attachment = Instantiate(attachment, end.localPosition, Quaternion.identity);
        }
    }
    */
    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 newPos = new Vector2(len * Mathf.Sin(angle), - len * Mathf.Cos(angle));
        end.localPosition = newPos;

        aAcc = - Time.fixedDeltaTime * Mathf.Sin(angle) / len * slowDown;

        aVel += aAcc;
        angle += aVel;

        attachment.transform.localPosition = end.transform.localPosition;
    }

}
