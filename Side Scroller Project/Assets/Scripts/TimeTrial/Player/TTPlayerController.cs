using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TTPlayerController : MonoBehaviour
{
    [Header("Player Refrences")]

    //Components
    [SerializeField] Rigidbody2D rigidBody;
    [SerializeField] Animator anim;


    [Header("Player Movement Settings")]

    //Serialize and Public Variables
    [SerializeField] float moveSpeed;

    //Private Variables
    float moveInput;
    bool isFacingRight = true;

    [Header("Player Jump Settings")]

    //Serialize and Public Variables
    [SerializeField] Transform feetpos;
    [SerializeField] float checkRadius;
    [SerializeField] LayerMask whatIsGround;
    [SerializeField] float jumpForce;
    [SerializeField] float jumpTime;

    //Private Variables
    bool isGrounded;
    bool isJumping;
    [HideInInspector] public bool canMove;
    float jumpTimeCounter;

    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(feetpos.position, checkRadius, whatIsGround);

        if(canMove)
        {
            MoveInput();

            JumpMechanics();

            AnimControl();
        }
    }

    private void FixedUpdate()
    {
        if(canMove)
        {
            MoveMechanism();
        }
    }

    void MoveInput()
    {
        moveInput = Input.GetAxis("Horizontal");
    }

    void MoveMechanism()
    {
        rigidBody.velocity = new Vector2(moveInput * moveSpeed, rigidBody.velocity.y);

        if (isFacingRight == false && moveInput > 0)
        {
            FlipCharacter();
        }
        else if (isFacingRight == true && moveInput < 0)
        {
            FlipCharacter();
        }
    }

    void JumpMechanics()
    {
        if (isGrounded == true && Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rigidBody.velocity = Vector2.up * jumpForce;
        }

        if (Input.GetKey(KeyCode.Space) && isJumping)
        {
            if (jumpTimeCounter > 0)
            {
                rigidBody.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
        }
    }

    void FlipCharacter()
    {
        isFacingRight = !isFacingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    void AnimControl()
    {
        if(rigidBody.velocity != Vector2.zero)
        {
            anim.SetBool("isRunning", true);
        }
        else 
        { 
            anim.SetBool("isRunning", false);  
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(feetpos.position, checkRadius);
    }

    public void OnDeath()
    {
        canMove = false;
        rigidBody.velocity = Vector2.zero;
    }
}
