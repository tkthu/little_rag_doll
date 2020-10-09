using UnityEngine;

public class PlatformController : MonoBehaviour
{
    private PlatformEffector2D effector;
    private float waitTime;
    public float delay = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        effector = GetComponent<PlatformEffector2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Press S to go down
        if(Input.GetKeyUp(KeyCode.S))
        {
            waitTime = delay;
        }
        if(Input.GetKey(KeyCode.S))
        {
            if(waitTime <= 0)
            {
                effector.rotationalOffset = 180f;
                waitTime = delay;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
        else
        {
            effector.rotationalOffset = 0;
        }
    }
}
