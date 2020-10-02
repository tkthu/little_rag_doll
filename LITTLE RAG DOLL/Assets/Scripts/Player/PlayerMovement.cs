using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller2D;
    public GameObject attack;
    public Animator animator;
    public float runSpeed = 40f;
    public Animator headAnimator;

    private float hormove = 0f;
    private float vermove = 0f;
    private bool isRunning = false;
    private bool isDucking = false;
    private bool isJumping = false;
    private bool isClinging = false;
    private bool isClimbing = false;
    private bool isSliding = false;
    private bool isAttacking = false;


    private float attackTimeRate = 0.3f;
    private float attackTimeLimit = 0; 

    // Update is called once per frame
    void Update()
    {
        hormove = Input.GetAxisRaw("Horizontal") * runSpeed;
        vermove = Input.GetAxisRaw("Vertical") * runSpeed;

        isRunning = false;
        if (hormove != 0 && controller2D.m_Grounded)
            isRunning = true;

        isDucking = false;
        if (Input.GetButton("Duck") && controller2D.m_Grounded)
            isDucking = true;            

        if (Input.GetButtonDown("Attack") && !attack.activeSelf)
        {
            isAttacking = true;
            setAnimParameter("trgAttack");
            attack.SetActive(true);
            attackTimeLimit = Time.time + attackTimeRate;
            attack.transform.localPosition = new Vector2(0.5f, 0);
            attack.transform.localScale = new Vector2(1, 1);
            if (isDucking)
                attack.transform.localPosition = new Vector2(0.5f, -0.4f);
            else if (isClinging)
            {
                attack.transform.localScale = new Vector2(-1, 1);
                attack.transform.localPosition = new Vector2(-0.5f, 0);
            }
                
        }
        if (Time.time >= attackTimeLimit)
        {
            isAttacking = false;
            attack.SetActive(false);
        }

        if (isAttacking && isClinging)
            vermove = 0;

        
        if (Input.GetButtonDown("Jump"))
            isJumping = true;

        isClinging = false;
        isClimbing = false;
        isSliding = false;
        if (controller2D.m_Walled && ((controller2D.m_FacingRight && hormove > 0) || (!controller2D.m_FacingRight && hormove < 0)))
        {
            isClinging = true;            
            if (vermove > 0)
                isClimbing = true;
            else if(vermove < 0)
                isSliding = true;
        }

        setAnimParameter("isRunning", isRunning);
        setAnimParameter("isDucking", isDucking);
        setAnimParameter("isJumping", isJumping);
        setAnimParameter("isGrounded", controller2D.m_Grounded);
        setAnimParameter("isClinging", isClinging);
        setAnimParameter("isClimbing", isClimbing);
        setAnimParameter("isSliding", isSliding); 
        setAnimParameter("isAttacking", isAttacking); 
    }

    private void setAnimParameter(string name,bool value)
    {
        animator.SetBool(name, value);
        headAnimator.SetBool(name, value);
    }
    private void setAnimParameter(string name)
    {
        animator.SetTrigger(name);
        headAnimator.SetTrigger(name);
    }
    public void OnLanding()
    {
        isJumping = false;
        setAnimParameter("isJumping", isJumping);

    }

    void FixedUpdate()
    {
        controller2D.Move(hormove * Time.fixedDeltaTime, vermove * Time.fixedDeltaTime, isDucking, isJumping, isClinging, isClimbing, isSliding);
    }
}
