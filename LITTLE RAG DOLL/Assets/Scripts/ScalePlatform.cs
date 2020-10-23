using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ScalePlatform : MonoBehaviour
{

    private float[] plateY = new float[2];

    private Transform leftPlate;
    private Transform rightPlate;


    private Dictionary<GameObject,Transform> dictParentLeft = new Dictionary<GameObject, Transform>();
    private Dictionary<GameObject, Transform> dictParentRight = new Dictionary<GameObject, Transform>();

    // Start is called before the first frame update
    void Start()
    {
        leftPlate = transform.Find("LeftPlate");
        rightPlate = transform.Find("RightPlate");

        updatePlateY();
    }
    
    private void updatePlateY()
    {
        plateY[0] = leftPlate.localPosition.y;
        plateY[1] = rightPlate.localPosition.y;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D GroundInfoLeft = Physics2D.Raycast(leftPlate.position,Vector2.down,0.2f,LayerMask.GetMask("Ground"));
        RaycastHit2D GroundInfoRight = Physics2D.Raycast(rightPlate.position,Vector2.down,0.2f, LayerMask.GetMask("Ground"));
        

        if (dictParentLeft.Count > dictParentRight.Count && rightPlate.localPosition.y < -1 && !GroundInfoLeft.collider) //trai nang hon phai
        {
            leftPlate.transform.localPosition = new Vector2(leftPlate.transform.localPosition.x, leftPlate.transform.localPosition.y - 1 * Time.deltaTime);
            rightPlate.transform.localPosition = new Vector2(rightPlate.transform.localPosition.x, rightPlate.transform.localPosition.y + 1 * Time.deltaTime);
        }else if (dictParentLeft.Count < dictParentRight.Count && leftPlate.localPosition.y < -1 && !GroundInfoRight.collider) //phai nang hon trai
        {
            rightPlate.transform.localPosition = new Vector2(rightPlate.transform.localPosition.x, rightPlate.transform.localPosition.y - 1 * Time.deltaTime);
            leftPlate.transform.localPosition = new Vector2(leftPlate.transform.localPosition.x, leftPlate.transform.localPosition.y + 1 * Time.deltaTime);
        }

    }

    public void addGameObject(GameObject goKey, Transform transParent, string plateTag)
    {
        if (plateTag == "RightPlate" && !dictParentRight.ContainsKey(goKey))
        {
            dictParentRight.Add(goKey, transParent);
        }            
        else if (plateTag == "LeftPlate" && !dictParentLeft.ContainsKey(goKey))
        {
            dictParentLeft.Add(goKey, transParent);
        }
            
    }

    public Transform removeGameObject(GameObject goKey, string plateTag)
    {
        Transform transParent = null;
        if (plateTag == "RightPlate" && dictParentRight.ContainsKey(goKey))
        {
            transParent = dictParentRight[goKey];
            dictParentRight.Remove(goKey);
        }            
        else if (plateTag == "LeftPlate" && dictParentLeft.ContainsKey(goKey))
        {
            transParent = dictParentLeft[goKey];
            dictParentLeft.Remove(goKey);
        }
        return transParent;
    }
}
