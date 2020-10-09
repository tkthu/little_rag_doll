using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float camSmooth = 0.05f;

    private GameObject player;
    private GameObject holder;
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
        holder = GameObject.FindGameObjectWithTag("EnemiesHolder");
    }

    void Update()
    {
        if(holder != null)
        {
            foreach(Transform child in holder.transform)
            {
                Vector3 eneScrPoint = cam.WorldToViewportPoint(child.position);
                EnemyHealth eneHealth = child.gameObject.GetComponent<EnemyHealth>();
                
                bool eneInCameraBound = eneScrPoint.x > -1 && eneScrPoint.x < 2 && eneScrPoint.y > -1 && eneScrPoint.y < 2;
                if (eneInCameraBound)
                {
                    bool eneInCamera = eneScrPoint.x > -0.125 && eneScrPoint.x < 1.125 && eneScrPoint.y > -0.125 && eneScrPoint.y < 1.125;
                    if (eneInCamera)
                    {
                        eneHealth.isFreezed = false;                        
                    }
                    else
                    {
                        eneHealth.respawn();
                    }
                }

            }
        }
    }
    private void LateUpdate()
    {
        Vector3 playerPos = player.GetComponent<Transform>().position;
        Vector3 playerScrPoint = cam.WorldToViewportPoint(playerPos);
        bool camYNeedToMove = playerScrPoint.y < 0.25 || playerScrPoint.y > 0.75;
        Vector3 targetPos = new Vector3(playerPos.x, camTrans.position.y , -10);
        if (camYNeedToMove)
        {
            targetPos = new Vector3(playerPos.x, playerPos.y, -10);
        }
        camTrans.position = Vector3.Lerp(camTrans.position,targetPos, camSmooth);
            
    }
}
