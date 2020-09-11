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
    private bool isJumping = false;
    private bool isDucking = false;

    private float attackTimeRate = 0.5f; // 5 secs
    private float attackTimeLimit = 0; // 1 secs
    // Update is called once per frame
    void Update()
    {
        
        hormove = Input.GetAxisRaw("Horizontal") * runSpeed;        
        animator.SetFloat("speed", Mathf.Abs(hormove));
        headAnimator.SetFloat("speed", Mathf.Abs(hormove));

        if (Input.GetButtonDown("Attack"))
        {
            animator.SetTrigger("trgAttack");
            headAnimator.SetTrigger("trgAttack");
            attack.SetActive(true);
            attackTimeLimit = Time.time + attackTimeRate;
        }
        if (Time.time >= attackTimeLimit)
        {            
            attack.SetActive(false);
        }            

        if (Input.GetButtonDown("Duck"))
        {
            isDucking = true;
            animator.SetBool("isDucking",true);
            headAnimator.SetBool("isDucking",true);
        }
        if (Input.GetButtonUp("Duck"))
        {
            isDucking = false;
            animator.SetBool("isDucking",false);
            headAnimator.SetBool("isDucking",false);
        }

        if (Input.GetButtonDown("Jump"))
        {
            isJumping = true;
            animator.SetBool("isJumping", true);
            headAnimator.SetBool("isJumping", true);
        }

    }
    public void OnLanding()
    {
        isJumping = false;
        animator.SetBool("isJumping", false);
        headAnimator.SetBool("isJumping", false);

    }
    void FixedUpdate()
    {
        controller2D.Move(hormove * Time.fixedDeltaTime, isDucking, isJumping);
    }
}
