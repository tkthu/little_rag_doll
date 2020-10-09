using UnityEngine;

public class FairyMovement : MonoBehaviour
{
    public float speed = 0.65f;
    private Rigidbody2D fairyBody;
    private Transform target;
    private GameObject player;

    private Vector2 steering;
    private float desiredSpeed;
    private Vector2 currentVelocity;
    private Vector2 desiredVelocity;

    private bool isSprinting = false;

    void Start()
    {
        if (GameManager.GM != null)
            player = GameManager.GM.player;
        else
            player = GameObject.FindGameObjectWithTag("Player");    

        fairyBody = GetComponent<Rigidbody2D>();
        target = player.transform;
    }

    void FixedUpdate()
    {       
        bool wasSprinting = isSprinting;

        if(Vector3.Distance(fairyBody.position, target.position) < 2)
            isSprinting = true;

        if (isSprinting && !wasSprinting )
        {
            seek();
            currentVelocity = currentVelocity + steering / fairyBody.mass;
            currentVelocity.Normalize();
            currentVelocity *= desiredSpeed;
            fairyBody.AddForce(currentVelocity ,ForceMode2D.Impulse);
        }
        if (!isSprinting)
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.fixedDeltaTime);

        if (fairyBody.velocity.magnitude < 1)
            isSprinting = false;       
        
    }

    
    void seek()
    {
        desiredVelocity = (target.position - transform.position);
        desiredVelocity.Normalize();
        desiredVelocity *= speed;
        steering = desiredVelocity - currentVelocity;
        desiredSpeed = speed;
    }
}
