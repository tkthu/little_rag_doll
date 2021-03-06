﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Animations;

public class PlayerMovement : MonoBehaviour
{
    public float runSpeed = 35f;
    public float jumpTime = 0.15f;
    public Animator animator;
    public Animator headAnimator;

    public AudioClip playerAttack;
    public AudioClip playerJump;

    private CharacterController2D controller2D;
    private GameObject attack;
    private GameObject grabShoot;    

    private float hormove = 0f;
    private float vermove = 0f;
    private bool isRunning = false;
    private bool isDucking = false;
    private bool isJumping = false;
    private bool isClinging = false;
    private bool isClimbing = false;
    private bool isSliding = false;
    private bool isAttacking = false;
    private bool isWallJumping = false;

    private bool highJump = false;
    private float jumpCounter = 0;

    private float attackTimeRate = 0.3f;
    private float attackTimeLimit = 0;

    private void Start()
    {
        controller2D = GetComponent<CharacterController2D>();
        attack = transform.Find("Attack").gameObject;
        grabShoot = transform.Find("GrabShoot").gameObject;
    }

    public void resetAction()
    {
            hormove = 0f;
            vermove = 0f;
            isRunning = false;
            isDucking = false;
            isJumping = false;
            isClinging = false;
            isClimbing = false;
            isSliding = false;
            isAttacking = false;
            isWallJumping = false;

            highJump = false;
            jumpCounter = 0;

    }

    // Update is called once per frame
    void Update()
    {
        #region RunHandler
        hormove = 0;
        if (Input.GetKey(GameManager.GM.left))
            hormove = -1 * runSpeed;
        else if (Input.GetKey(GameManager.GM.right))
            hormove = 1 * runSpeed;
        vermove = 0;
        if (Input.GetKey(GameManager.GM.down))
            vermove = -1 * runSpeed;
        else if (Input.GetKey(GameManager.GM.up))
            vermove = 1 * runSpeed;

        //hormove = Input.GetAxisRaw("Horizontal") * runSpeed;
        //vermove = Input.GetAxisRaw("Vertical") * runSpeed;

        isRunning = false;
        if (hormove != 0 && controller2D.m_Grounded)
            isRunning = true;
        #endregion

        #region DuckHandler
        isDucking = false;
        if (Input.GetKey(GameManager.GM.down) && controller2D.m_Grounded)
            isDucking = true;
        #endregion

        #region JumpHandler
        
        else if (!Input.GetKey(GameManager.GM.down) &&  Input.GetKeyDown(GameManager.GM.jump) && controller2D.m_Grounded)
        {
            AudioManager.instance.PlaySound(playerJump, transform.position);
            isJumping = true;
            highJump = true;
            jumpCounter = jumpTime;
        }
        else if (Input.GetKeyDown(GameManager.GM.jump) && isClinging && !isWallJumping)
        {
            AudioManager.instance.PlaySound(playerJump, transform.position);
            isWallJumping = true;
            highJump = true;
            jumpCounter = jumpTime;
        }


        if (Input.GetKeyUp(GameManager.GM.jump))
        {
            highJump = false;
        }
        if (Input.GetKey(GameManager.GM.jump) && highJump)
        {            
            if (jumpCounter > 0)
            {
                jumpCounter -= Time.deltaTime;
            }
            else
            {
                highJump = false;
            }
        }

        
        

        if (isWallJumping)
        {
            hormove = 0;
        }
        #endregion

        #region AttackHandler

        if (Input.GetKeyDown(GameManager.GM.attack) && !attack.activeSelf && !grabShoot.activeSelf)
        {
            AudioManager.instance.PlaySound(playerAttack, transform.position);
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
        #endregion

        #region GrabHandler
        if (Input.GetKeyDown(GameManager.GM.eatShoot) && !attack.activeSelf)
        {
            PlayerGrabShoot pGrab = grabShoot.GetComponent<PlayerGrabShoot>();

            Vector2 shootDir = transform.localScale;
            Vector2 grabDir = Vector2.one;
            Vector2 offset = Vector2.zero;
            if (isDucking)
                offset = new Vector2(0, -0.4f);
            else if (isClinging)
            {
                shootDir = new Vector2(-1 * transform.localScale.x, 1);
                grabDir = new Vector2(-1, 1);
                offset = Vector2.zero;
            }

            pGrab.grabOrShoot(offset, grabDir, shootDir);
        }
        if (grabShoot.activeSelf && isClinging)
            vermove = 0;
        #endregion

        #region ClingHandler

        isClinging = false;
        isClimbing = false;
        isSliding = false;
        if (controller2D.m_Walled && !controller2D.m_Grounded && !controller2D.m_Themed)
        {
            isClinging = true;
            hormove = 0;
            if (vermove > 0)
                isClimbing = true;
            else if(vermove < 0)
                isSliding = true;
        }
        #endregion

        #region UpdateAnimtion
        setAnimParameter("isRunning", isRunning);
        setAnimParameter("isDucking", isDucking);
        setAnimParameter("isJumping", isJumping);
        setAnimParameter("isGrounded", controller2D.m_Grounded);
        setAnimParameter("isClinging", isClinging);
        setAnimParameter("isClimbing", isClimbing);
        setAnimParameter("isSliding", isSliding); 
        setAnimParameter("isAttacking", isAttacking);
        #endregion
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
    public void OnClinging()
    {
        isWallJumping = false;
        setAnimParameter("isJumping", isJumping);
    }

    void FixedUpdate()
    {
        controller2D.Move(hormove * Time.fixedDeltaTime, vermove * Time.fixedDeltaTime, isDucking, isJumping, highJump , isClinging, isClimbing, isSliding, isWallJumping);
    }
}
