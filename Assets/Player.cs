using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D body;
    public Animator animator;


    [Header("Orientation Settings")]
    [SerializeField] private CharacterDirection currentDirection = CharacterDirection.Right;
    private bool rotatedLeft = false;

    [Header("Movement Settings")]
    [SerializeField] private bool instantAcceleration = true;
    [SerializeField] private float moveSpeed = 3.5f;
    [SerializeField] private float jumpForce = 12;
    private float xInput;
    [SerializeField] private bool introJump = true;

    [Header("Collision Settings")]
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask whatIsGround;
    private bool isGrounded;

    private void Awake()
    {
        if (body == null)
            body = GetComponent<Rigidbody2D>();
        
        if (animator == null)
            animator = GetComponentInChildren<Animator>();

        //Ensure player is facing the correct direction on start
        HandleOrientation();
    }

    private void Update()
    {
        HandleCollision();
        HandleMovement();
        HandleAnimation();
        HandleOrientation();
    }

    [ContextMenu("Flip Player")]
    private void HandleOrientation()
    {
        if ((currentDirection == CharacterDirection.Left && !rotatedLeft) || (currentDirection != CharacterDirection.Left && rotatedLeft))
        {
            transform.Rotate(0, 180, 0);
            rotatedLeft = !rotatedLeft;
        }
    }

    private void HandleAnimation()
    { 
        //Set the paramater values for the animator component
        animator.SetBool("isGrounded", isGrounded);
        animator.SetFloat("yVelocity", body.linearVelocityY);
        animator.SetFloat("xVelocity", body.linearVelocityX);

        if (introJump)
        {
            float ogJumpForce = jumpForce;
            jumpForce = 18;
            Jump(llinearVelocityYOverride: 1);
            jumpForce = ogJumpForce;
            introJump = false;
        }
    }

    private void HandleMovement()
    {
        Move();

        if (JumpInputDetected())
            Jump();
    }

    private void Move()
    {
        //Course directs to use GetAxisRaw for instant input response and GetAxis for smoothed input so we allow both
        xInput = (instantAcceleration ? Input.GetAxisRaw(Constants.HORIZONTAL_INPUT) : Input.GetAxis(Constants.HORIZONTAL_INPUT));
        
        if(xInput != 0)
            currentDirection = xInput < 0 ? CharacterDirection.Left : CharacterDirection.Right;

        //Course directs to create a new Vector2 each frame, but this is more efficient
        body.linearVelocityX = xInput * moveSpeed;
    }
    private static bool JumpInputDetected()
    {
        return Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(1);
    }


    private void Jump(float llinearVelocityYOverride = 0)
    {
        if(isGrounded)
            body.linearVelocityY = (llinearVelocityYOverride != 0 ? llinearVelocityYOverride : body.linearVelocityY) + jumpForce;
    }

    private void HandleCollision()
    {
        //Cast a ray to check for collision with any object in the layer assinged to whatIsGround
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatIsGround);
    }

    private void OnDrawGizmos()
    {
        //Draw a design-time only line to visualize the ground detection ray
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(0, -groundCheckDistance));
    }
}