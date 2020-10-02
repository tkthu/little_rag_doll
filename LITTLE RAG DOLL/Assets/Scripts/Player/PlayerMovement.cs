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
    private bool isJumping = false;
    private bool isClinging = false;
    private bool isDucking = false;

    private float attackTimeRate = 0.5f;
    private float attackTimeLimit = 0; 

    // Update is called once per frame
    void Update()
    {
        hormove = Input.GetAxisRaw("Horizontal") * runSpeed;
        vermove = Input.GetAxisRaw("Vertical") * runSpeed;
            
        animator.SetFloat("speed", Mathf.Abs(hormove));
        headAnimator.SetFloat("speed", Mathf.Abs(hormove));

        animator.SetFloat("speedY", Mathf.Abs(vermove));
        headAnimator.SetFloat("speedY", Mathf.Abs(vermove));

        if (Input.GetButtonDown("Attack") && !attack.activeSelf)
        {
            animator.SetTrigger("trgAttack");
            headAnimator.SetTrigger("trgAttack");
            attack.SetActive(true);
            attackTimeLimit = Time.time + attackTimeRate;
        }
        if (Time.time >= attackTimeLimit)        
            attack.SetActive(false);

        if (Input.GetButtonDown("Duck"))
            isDucking = true;
        if (Input.GetButtonUp("Duck"))
            isDucking = false;

        if (Input.GetButtonDown("Jump"))
            isJumping = true;

        if (!controller2D.m_Walled)
            isClinging = false;

        animator.SetBool("isDucking", isDucking);
        headAnimator.SetBool("isDucking", isDucking);
        animator.SetBool("isJumping", isJumping);
        headAnimator.SetBool("isJumping", isJumping);
        animator.SetBool("isGrounded", controller2D.m_Grounded);
        headAnimator.SetBool("isGrounded", controller2D.m_Grounded);
        animator.SetBool("isClinging", isClinging);
        headAnimator.SetBool("isClinging", isClinging);       

    }
    public void OnLanding()
    {
        isJumping = false;
        animator.SetBool("isJumping", isJumping);
        headAnimator.SetBool("isJumping", isJumping);

    }
    public void OnClinging()
    {
        isClinging = true;
        animator.SetBool("isClinging", isClinging);
        headAnimator.SetBool("isClinging", isClinging);

    }
    void FixedUpdate()
    {
        controller2D.Move(hormove * Time.fixedDeltaTime, vermove * Time.fixedDeltaTime, isDucking, isJumping, isClinging);
    }
}
