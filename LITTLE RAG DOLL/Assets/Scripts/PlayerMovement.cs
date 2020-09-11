using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller2D;
    public Animator animator;
    public float runSpeed = 40f;
    public Animator headAnimator;

    private float hormove = 0f;
    private bool isJumping = false;
    private bool isDucking = false;    

    // Update is called once per frame
    void Update()
    {
        hormove = Input.GetAxisRaw("Horizontal") * runSpeed;
        animator.SetFloat("Speed", Mathf.Abs(hormove));
        if (Input.GetButtonDown("Attack"))
        {
            headAnimator.SetTrigger("strgAttack");
        }

        if (Input.GetButtonDown("Duck"))
        {
            isDucking = true;
            animator.SetBool("isDucking",true);
        }
        if (Input.GetButtonUp("Duck"))
        {
            isDucking = false;
            animator.SetBool("isDucking",false);
        }

        if (Input.GetButtonDown("Jump"))
        {
            isJumping = true;
            animator.SetBool("isJumping", true);            
        }

    }
    public void OnLanding()
    {
        isJumping = false;
        animator.SetBool("isJumping", false);
    }
    void FixedUpdate()
    {
        controller2D.Move(hormove * Time.fixedDeltaTime, isDucking, isJumping);
    }
}
