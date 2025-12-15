using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets
{
    public class PlayerCharacter : MonoBehaviour
    {
        public Rigidbody2D body;
        public Animator animator;

        [Header("Attack Details")]
        [SerializeField] private float attackRadius;
        [SerializeField] private Transform attackPoint;
        [SerializeField] private LayerMask whatIsEnemy;

        [Header("Orientation Settings")]
        [SerializeField] private CharacterDirection currentDirection = CharacterDirection.Right;
        private bool rotatedLeft = false;

        [Header("Movement Settings")]
        [SerializeField] private bool instantAcceleration = true;
        [SerializeField] private float moveSpeed = 3.5f;
        [SerializeField] private bool introJump = false;
        private float xInput;
        private bool canmove = true, canjump = true;
        public float jumpForce = 12;
        public bool CanMove { get { return canmove; } }
        public bool CanJump { get { return canjump; } }
    

        [Header("Collision Settings")]
        [SerializeField] private float groundCheckDistance;
        [SerializeField] private LayerMask whatIsGround;
        private bool isGrounded;

        [Header("Action Settings")]
        [SerializeField]
        private bool canAttack = true;
        
        private PlayerActionLoop customActionLoop;

        private void Awake()
        {
            if (customActionLoop == null)
                customActionLoop = new PlayerActionLoop(this);

            if (body == null)
                body = GetComponent<Rigidbody2D>();
        
            if (animator == null)
                animator = GetComponentInChildren<Animator>();

            //Ensure player is facing the correct direction on start
            HandleOrientation();

            if (introJump)
                customActionLoop.AppendAction(new IntroJumpAction());
        }

        private void Update()
        {
            HandleCollision();
            HandleMovement();
            HandleAnimation();
            HandleOrientation();
            customActionLoop.ProcessActions();
        }

        public void DamageEnimies()
        {
            var enemyColliders = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, whatIsEnemy);
            Debug.Log($"Damaging {enemyColliders.Length} enemies");

            foreach (var enemyCollider in enemyColliders)
            { 
                enemyCollider.GetComponent<Enemy>()?.TakeDamage();
            }
        }

        public void SetMovementAndJump(bool state)
        {
            canjump = state;
            canmove = state;
        }

        public void SetCanAttack(bool state)
        { 
            canAttack = state;
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
                TryJump(llinearVelocityYOverride: 1);
                jumpForce = ogJumpForce;
                introJump = false;
            }
        }

        private void HandleMovement()
        {
            Move();

            if (JumpInputDetected())
                TryJump();

            if(AttackInputDetected())
                TryAttack();
        }


        private void Move()
        {

            if (!canmove)
                return;

            //Course directs to use GetAxisRaw for instant input response and GetAxis for smoothed input so we allow both
            xInput = (instantAcceleration ? Input.GetAxisRaw(Constants.HORIZONTAL_INPUT) : Input.GetAxis(Constants.HORIZONTAL_INPUT));

            if (xInput != 0)
                currentDirection = xInput < 0 ? CharacterDirection.Left : CharacterDirection.Right;

            //Course directs to create a new Vector2 each frame, but this is more efficient
            body.linearVelocityX = xInput * moveSpeed;
        }

        private bool JumpInputDetected()
        {
            return Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(1);
        }

        public bool AttackInputDetected()
        {
            return Input.GetKeyDown(KeyCode.Mouse0);
        }


        public void TryJump(float llinearVelocityYOverride = 0)
        {
            if(isGrounded && canjump)
                body.linearVelocityY = (llinearVelocityYOverride != 0 ? llinearVelocityYOverride : body.linearVelocityY) + jumpForce;
        }

        private bool IsinMotion()
        {
            return body.linearVelocityX + body.linearVelocityY != 0;
        }

        private void TryAttack()
        {
            if (isGrounded && canAttack)
            {
                animator.SetTrigger("attack");
                body.linearVelocityX = 0;
            }
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
            Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
        }
    }
}