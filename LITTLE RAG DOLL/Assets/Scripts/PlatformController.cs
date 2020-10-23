using UnityEngine;

public class PlatformController : MonoBehaviour
{
    private PlatformEffector2D effector;
    private float waitTime;
    // Start is called before the first frame update
    void Start()
    {
        effector = GetComponent<PlatformEffector2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Press S to go down
        
        if(Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.K))
        {
            effector.rotationalOffset = 180f;
           
        }
        else if(effector.rotationalOffset != 0)
        {
            effector.rotationalOffset = 0;
        }
    }
}
