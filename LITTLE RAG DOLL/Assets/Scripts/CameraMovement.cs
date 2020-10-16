using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float camSmooth = 5;

    private GameObject player;
    private Camera cam;
    private Transform camTrans;
    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.GM != null)
            player = GameManager.GM.player;
        else
            player = GameObject.FindGameObjectWithTag("Player");

        cam = GetComponent<Camera>();
        camTrans = GetComponent<Transform>();
    }

    void Update()
    {
        List<List<GameObject>> listOfPool = GameManager.GM.poolingManager.getlistOfEnemiesPool();
        foreach (List<GameObject> pool in listOfPool)
        {
            foreach (GameObject go in pool)
            {
                Vector3 eneScrPoint = cam.WorldToViewportPoint(go.transform.position);
                EnemyHealth eneHealth = go.GetComponent<EnemyHealth>();
                
                bool eneInCameraBound = eneScrPoint.x > -1 && eneScrPoint.x < 2 && eneScrPoint.y > -1 && eneScrPoint.y < 2;
                if (eneInCameraBound)
                {
                    bool eneInCamera = eneScrPoint.x > -0.125 && eneScrPoint.x < 1.125 && eneScrPoint.y > -0.125 && eneScrPoint.y < 1.125;
                    if (eneInCamera && !eneHealth.isDeaded)
                        eneHealth.isFreezed = false;

                    if (!eneInCamera)
                    {
                        if(eneHealth.isDeaded)
                            eneHealth.respawn();
                        else if(go.CompareTag("BubbleBlower"))
                            eneHealth.isFreezed = true;
                    }
                        

                    
                }
                
            }
                

        }
          
    }
    private void LateUpdate()
    {
        Vector3 playerPos = player.GetComponent<Transform>().position;
        Vector3 targetPos = new Vector3(playerPos.x, playerPos.y, -10);
        camTrans.position = Vector3.Lerp(camTrans.position,targetPos, camSmooth*Time.deltaTime);
            
    }
}
