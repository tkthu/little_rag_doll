using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
	[SerializeField] private float m_JumpForce = 400f;							// Amount of force added when the player jumps.
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;	// How much to smooth out the movement
	[SerializeField] private LayerMask m_WhatIsGround;							// A mask determining what is ground to the character
	[SerializeField] private LayerMask m_WhatIsThem;                          // A mask determining what is ground to the character
	[SerializeField] private Transform m_GroundCheck;							// A position marking where to check if the player is grounded.
	[SerializeField] private Transform m_CeilingCheck;                          // A position marking where to check for ceilings
	[SerializeField] private Transform Face;


	const float k_ColliderRadius = .02f; // Radius of the overlap circle to determine if grounded
	[HideInInspector] public bool m_Grounded;            // Whether or not the player is grounded.
	[HideInInspector] public bool m_Themed;            // Whether or not the player is grounded.
	[HideInInspector] public bool m_Walled;            // Whether or not the player is grounded.
	[HideInInspector] public bool m_FacingRight = true;  // For determining which way the player is currently facing.
	private Rigidbody2D m_Rigidbody2D;	
	private Vector2 m_Velocity = Vector2.zero;

	//private float wallJumpTime = 0.04f;

	private float wallJumpTime = 0.02f;

	private bool wallJumping = false;

	[Header("Events")]
	[Space]

	public UnityEvent OnLandEvent;
	public UnityEvent OnClingEvent;

	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }

	private void Awake()
	{
		
		m_Rigidbody2D = GetComponent<Rigidbody2D>();

		if (OnLandEvent == null)
			OnLandEvent = new UnityEvent();
		if (OnClingEvent == null)
			OnClingEvent = new UnityEvent();
	}

	private void FixedUpdate()
	{
		bool wasGrounded = m_Grounded;
		m_Grounded = false;
		m_Walled = false;
		m_Themed = false;

		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_ColliderRadius, m_WhatIsGround);
		if(colliders.Length > 0)
        {
			m_Grounded = true;
			if (!wasGrounded)
				OnLandEvent.Invoke();
		}

		// check if cling to wall
		Collider2D[] wallColliders = Physics2D.OverlapCircleAll(Face.position, k_ColliderRadius, m_WhatIsGround);
		if (wallColliders.Length > 0)
        {
			m_Walled = true;
		}

		Collider2D[] themColliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_ColliderRadius, m_WhatIsThem);
		if (themColliders.Length > 0)
		{
			m_Themed = true;			
			m_Grounded = true;			
			if (!wasGrounded)
				OnLandEvent.Invoke();
		}

	}


	public void Move(float moveX, float moveY, bool crouch, bool jump, bool highJump, bool cling, bool climb, bool slide, bool wallJump)
	{
		Vector2 targetVelocity;
		targetVelocity = new Vector2(moveX * 10f, m_Rigidbody2D.velocity.y);
		m_Rigidbody2D.velocity = Vector2.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
		m_Rigidbody2D.gravityScale = 3;
		

		if (highJump && !wallJumping)
		{
			m_Rigidbody2D.velocity = new Vector2(0f, m_JumpForce);
		}
		if (m_Grounded)
		{			
			if (jump)
				m_Rigidbody2D.velocity = new Vector2(0f, m_JumpForce);
			else if (crouch)
				m_Rigidbody2D.velocity = Vector2.zero;
		}else if (m_Walled)
        {
			if (cling && !wallJumping)
            {
				m_Rigidbody2D.velocity = Vector2.zero;
				m_Rigidbody2D.gravityScale = 0;
				if (climb)
				{
					targetVelocity = new Vector2(m_Rigidbody2D.velocity.x, moveY * 10f);
					m_Rigidbody2D.velocity = Vector2.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);					
				}
				else if (slide)
				{
					targetVelocity = new Vector2(m_Rigidbody2D.velocity.x, moveY * 15f);
					m_Rigidbody2D.velocity = Vector2.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
				}				
			}
		}

		if (wallJump && !wallJumping)
		{
			wallJumping = true;
			Invoke("setWallJumpingToFalse", wallJumpTime);
			int dir = m_FacingRight ? 1 : -1;
			m_Rigidbody2D.velocity = new Vector2(-dir * 10f, 7f);
			Debug.Log("first"+m_Rigidbody2D.velocity);
		}

		if (moveX > 0 && !m_FacingRight)
			Flip();
		else if (moveX < 0 && m_FacingRight)
			Flip();

	}
    private void setWallJumpingToFalse()
    {
		wallJumping = false;
		OnClingEvent.Invoke();
	}

	/*
	IEnumerator wallJump(Vector2 targetVelocity)
	{
		while (Vector2.Distance(m_Rigidbody2D.position, targetVelocity) > 1f)
        {
			Debug.Log(m_Rigidbody2D.position + " " + targetVelocity);
			m_Rigidbody2D.position = Vector2.SmoothDamp(m_Rigidbody2D.position, targetVelocity, ref m_Velocity, m_MovementSmoothing);
			yield return null;
		}
		yield return null;
	}

	*/
	private void Flip()
	{
		m_FacingRight = !m_FacingRight;
		Vector2 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
