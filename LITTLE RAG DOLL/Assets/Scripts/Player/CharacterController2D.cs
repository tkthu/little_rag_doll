using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
	[SerializeField] private float m_JumpForce = 400f;							// Amount of force added when the player jumps.
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;	// How much to smooth out the movement
	[SerializeField] private LayerMask m_WhatIsGround;							// A mask determining what is ground to the character
	[SerializeField] private Transform m_GroundCheck;							// A position marking where to check if the player is grounded.
	[SerializeField] private Transform m_CeilingCheck;                          // A position marking where to check for ceilings
	[SerializeField] private Transform Face;


	const float k_ColliderRadius = .02f; // Radius of the overlap circle to determine if grounded
	[HideInInspector] public bool m_Grounded;            // Whether or not the player is grounded.
	[HideInInspector] public bool m_Walled;            // Whether or not the player is grounded.
	const float k_CeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
	private Rigidbody2D m_Rigidbody2D;
	[HideInInspector] public bool m_FacingRight = true;  // For determining which way the player is currently facing.
	private Vector2 m_Velocity = Vector2.zero;

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
		bool wasWalled = m_Walled;
		m_Grounded = false;
		m_Walled = false;

		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_ColliderRadius, m_WhatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			m_Grounded = true;
			if (!wasGrounded)
            {
				OnLandEvent.Invoke();
				break;
			}
				
		}

		// check if cling to wall
		Collider2D[] wallColliders = Physics2D.OverlapCircleAll(Face.position, k_ColliderRadius, m_WhatIsGround);
		for (int i = 0; i < wallColliders.Length; i++)
		{
			m_Walled = true;
			if (!wasWalled)
				OnClingEvent.Invoke();
		}		
	}


	public void Move(float moveX, float moveY, bool crouch, bool jump, bool cling)
	{
		Vector2 targetVelocity = new Vector2(moveX * 10f, m_Rigidbody2D.velocity.y);
		m_Rigidbody2D.velocity = Vector2.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
		m_Rigidbody2D.gravityScale = 3;

		if (m_Grounded)
		{
			if (jump)
				m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
			else if (crouch)
				m_Rigidbody2D.velocity = Vector2.zero;
		}
		if (m_Walled)
        {
			if (moveX > 0 && m_FacingRight || moveX < 0 && !m_FacingRight)
			{
				m_Rigidbody2D.velocity = Vector2.zero;
				m_Rigidbody2D.gravityScale = 0;
				targetVelocity = new Vector2(m_Rigidbody2D.velocity.x, moveY * 10f);
				m_Rigidbody2D.velocity = Vector2.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
			}		
					
		}


		if (moveX > 0 && !m_FacingRight)
			Flip();
		else if (moveX < 0 && m_FacingRight)
			Flip();

	}


	private void Flip()
	{
		m_FacingRight = !m_FacingRight;
		Vector2 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
