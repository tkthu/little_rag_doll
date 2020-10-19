using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ScalePlatform : MonoBehaviour
{

    private float[] plateY = new float[2];

    private Transform leftPlate;
    private Transform rightPlate;

    private GameObject player;

    private List<GameObject> listGameObjectLeft = new List<GameObject>();
    private List<GameObject> listGameObjectRight = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.GM != null)
            player = GameManager.GM.player;
        else
            player = GameObject.FindGameObjectWithTag("Player");

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
        if(listGameObjectLeft.Count > listGameObjectRight.Count && rightPlate.localPosition.y < -1) //trai nang hon phai
        {
            leftPlate.transform.localPosition = new Vector2(leftPlate.transform.localPosition.x, leftPlate.transform.localPosition.y - 1 * Time.deltaTime);
            rightPlate.transform.localPosition = new Vector2(rightPlate.transform.localPosition.x, rightPlate.transform.localPosition.y + 1 * Time.deltaTime);
        }else if (listGameObjectLeft.Count < listGameObjectRight.Count && leftPlate.localPosition.y < -1) //phai nang hon trai
        {
            rightPlate.transform.localPosition = new Vector2(rightPlate.transform.localPosition.x, rightPlate.transform.localPosition.y - 1 * Time.deltaTime);
            leftPlate.transform.localPosition = new Vector2(leftPlate.transform.localPosition.x, leftPlate.transform.localPosition.y + 1 * Time.deltaTime);
        }

    }

    private bool checkContainGameObject(int instanceID, List<GameObject> listGameObject)
    {
        foreach(GameObject go in listGameObject)
        {
            if (go.GetInstanceID() == instanceID)
                return true;
        }
        return false;
    }

    public void addGameObject(GameObject go, string plateTag)
    {
        Debug.Log(listGameObjectRight.Contains(go) + " "+ go);
        if (plateTag == "RightPlate" && !listGameObjectRight.Contains(go))
            listGameObjectRight.Add(go);
        else if (plateTag == "LeftPlate" && !listGameObjectLeft.Contains(go))
            listGameObjectLeft.Add(go);
        Debug.Log(listGameObjectLeft.Count +" "+ listGameObjectRight.Count);

    }

    public void removeGameObject(GameObject go, string plateTag)
    {
        if (plateTag == "RightPlate" && listGameObjectRight.Contains(go))
            listGameObjectRight.Remove(go);
        else if (plateTag == "LeftPlate" && listGameObjectLeft.Contains(go))
            listGameObjectLeft.Remove(go);
        Debug.Log(listGameObjectLeft.Count + " " + listGameObjectRight.Count);
    }
}
