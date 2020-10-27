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
}
