using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TTPlayerController : MonoBehaviour
{
    #region Variables
    private PlayerInputs plyInput;
    private InputAction plyAction;

    [Header("References")]
    public Rigidbody2D rigidbody;

    [Header("Movement Settings")]
    public float moveSpeed;
    float moveX;

    [Header("Jump Settings")]
    public Transform feetPos;
    public float checkRadius;
    public LayerMask ground;
    public float jumpForce;
    public float jumpTime;
    [SerializeField] bool isGrounded;
    [SerializeField] bool isJumping;
    float jumpTimeCount;
    #endregion

    #region Pre-Defined Functions
    private void Awake()
    {
        plyInput = new PlayerInputs(); ;
    }

    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, ground);
    }

    private void FixedUpdate()
    {
        moveX = plyInput.Player.Movement.ReadValue<Vector2>().x;
        rigidbody.velocity = new Vector2(moveX * moveSpeed, rigidbody.velocity.y);
    }

    private void OnEnable()
    {
        plyAction = plyInput.Player.Movement;
        plyAction.Enable();

        plyInput.Player.Jump.started += JumpStart;
        plyInput.Player.Jump.performed += JumpPerform;
        plyInput.Player.Jump.canceled += JumpCancel;
        plyInput.Player.Jump.Enable();
    }

    private void OnDisable()
    {
        plyAction.Disable();
        plyInput.Player.Jump.Disable();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(feetPos.position, checkRadius);
    }
    #endregion

    #region Custom Functions
    void JumpStart(InputAction.CallbackContext obj)
    {
        if (isGrounded == true)
        {
            isJumping = true;
            jumpTimeCount = jumpTime;
            rigidbody.velocity = Vector2.up * jumpForce;
        }
    }

    void JumpPerform(InputAction.CallbackContext obj)
    {
        if (isJumping)
        {
            if (jumpTimeCount > 0)
            {
                rigidbody.velocity = Vector2.up * jumpForce;
                jumpTimeCount -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }
    }

    void JumpCancel(InputAction.CallbackContext obj)
    {
        isJumping = false;
    }
    #endregion
}
