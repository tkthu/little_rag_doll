using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller2D;
    public GameObject attack;
    public GameObject grab;
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
    [HideInInspector] public bool isRetracting = false;
    private bool isWallJumping = false;

    private bool highJump = false;
    private float jumpCounter = 0;
    public float jumpTime = 0.35f;
    public float grabLength = 2f;
    public float retractSpeed = 5f;



    private float attackTimeRate = 0.3f;
    private float attackTimeLimit = 0; 

    // Update is called once per frame
    void Update()
    {
        #region RunHandler
        hormove = Input.GetAxisRaw("Horizontal") * runSpeed;
        vermove = Input.GetAxisRaw("Vertical") * runSpeed;

        isRunning = false;
        if (hormove != 0 && controller2D.m_Grounded)
            isRunning = true;
        #endregion

        #region DuckHandler
        isDucking = false;
        if (Input.GetButton("Duck") && controller2D.m_Grounded)
            isDucking = true;
        #endregion

        #region JumpHandler
        
        else if (!Input.GetButton("Duck") &&  Input.GetButtonDown("Jump") && controller2D.m_Grounded)
        {
            isJumping = true;
            highJump = true;
            jumpCounter = jumpTime;
        }
        else if (Input.GetButtonDown("Jump") && isClinging && !isWallJumping)
        {
            isWallJumping = true;
            highJump = true;
            jumpCounter = jumpTime;
        }


        if (Input.GetButtonUp("Jump"))
        {
            highJump = false;
        }
        if (Input.GetButton("Jump") && highJump)
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
        #endregion    
        
        #region GrabHandler

        if (Input.GetButtonDown("Grab/Shoot") && !grab.activeSelf)
        {
            grab.SetActive(true);
        }
        if(grab.activeSelf)
        {
            

            if (!isRetracting && Vector2.Distance(Vector2.zero, grab.transform.localPosition) < grabLength )
            {
                grab.transform.localPosition = Vector2.MoveTowards(grab.transform.localPosition, new Vector2(grabLength,0), retractSpeed * Time.deltaTime);
            }
            else
            {
                isRetracting = true;
                grab.transform.localPosition = Vector2.MoveTowards(grab.transform.localPosition, Vector2.zero, retractSpeed * Time.deltaTime);
                if (grab.transform.localPosition.magnitude <= 0)
                {
                    isRetracting = false;
                    foreach (Transform child in grab.transform)
                    {
                        PoolingItem pi = child.GetComponent<PoolingItem>();
                        if(pi != null) pi.resetState();
                    }
                    grab.SetActive(false);
                }
            }
            
        }
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
