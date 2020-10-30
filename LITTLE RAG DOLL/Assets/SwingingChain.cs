using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class SwingingChain : MonoBehaviour
{
    private Transform root;
    private Transform end;    
    private float slowDown = 0.01f;

    private float len;
    private float angle;
    private float aVel;
    private float aAcc;

    private float chainDistance = 0.5f;

    private Dictionary<Transform, float> dictOfLen = new Dictionary<Transform, float>();


    // Start is called before the first frame update
    void Start()
    {
        root = transform.Find("ChainLink_root");
        end = transform.Find("ChainLink_end");        
        len = Vector2.Distance(end.localPosition, root.localPosition);
        float val = Mathf.Clamp(end.localPosition.x / len, -1, 1);
        angle = Mathf.Asin(val); ;

        aVel = 0;
        aAcc = 0;
        dictOfLen.Add(end, len);
        drawChain();
    }
    
    private void drawChain()
    {
        float l = len;
        while(l > chainDistance)
        {
            l = l - chainDistance;
            GameObject prefab = Resources.Load<GameObject>("Prefabs/Platfroms/ChainLink");
            GameObject chain = Instantiate(prefab);
            chain.transform.SetParent(transform);
            dictOfLen.Add(chain.transform, l);
        }
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        foreach (KeyValuePair<Transform, float> entry in dictOfLen)
        {
            len = entry.Value;
            Vector2 newPos = new Vector2(len * Mathf.Sin(angle), -len * Mathf.Cos(angle));
            entry.Key.localPosition = newPos;
        }
        

        aAcc = - Time.fixedDeltaTime * Mathf.Sin(angle) / len * slowDown;

        aVel += aAcc;
        angle += aVel;

        
    }

}
