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
        
    }
    private void LateUpdate()
    {
        Vector3 playerPos = player.GetComponent<Transform>().position;
        Vector3 targetPos = new Vector3(playerPos.x, playerPos.y, -10);
        camTrans.position = Vector3.Lerp(camTrans.position,targetPos, camSmooth*Time.deltaTime);
            
    }
}
