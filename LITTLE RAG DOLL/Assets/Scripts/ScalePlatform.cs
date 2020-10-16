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
    private Transform playerFeet;

    private List<GameObject> listGameObject = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.GM != null)
            player = GameManager.GM.player;
        else
            player = GameObject.FindGameObjectWithTag("Player");

        playerFeet = player.transform.Find("GroundCheck");

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
        RaycastHit2D groundInfo = Physics2D.Raycast(playerFeet.position, Vector2.down, 0.2f, LayerMask.GetMask("Them"));
        if(groundInfo.collider && (groundInfo.collider.CompareTag("LeftPlate") || groundInfo.collider.CompareTag("RightPlate")))
        {
            if(groundInfo.collider.transform == leftPlate)
            {
                //Debug.Log("leftPlate.position.y "+ leftPlate.localPosition.y);
            }
            groundInfo.collider.attachedRigidbody.bodyType = RigidbodyType2D.Dynamic;
            groundInfo.collider.attachedRigidbody.gravityScale = 1;
                      
        }
    }

    public void addGameObject(GameObject go)
    {
        if (!listGameObject.Contains(go))
        {
            listGameObject.Add(go);
            Debug.Log(listGameObject.Count);
        }
    }

    public void removeGameObject(GameObject go)
    {
        if (listGameObject.Contains(go))
        {            
            listGameObject.Remove(go);
            Debug.Log(listGameObject.Count);
        }
    }
}
