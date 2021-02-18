using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float movementSpeed = 1;
    [SerializeField] float climbingSpeed = 5;
    [SerializeField] float jumpForce = 10;
    [SerializeField] int numberOfJumps = 2;
    [SerializeField] Vector2 deadKick = new Vector2(0, 10);

    Rigidbody2D myRigidbody2d;
    Animator myAnimator;
    CapsuleCollider2D myBodyCollider2d;
    BoxCollider2D myFeetsCollider2d;

    int availableJumps = 0;
    float gravityScale;
    bool isAlive = true;


    // Start is called before the first frame update
    void Start()
    {
        myRigidbody2d = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider2d = GetComponent<CapsuleCollider2D>();
        myFeetsCollider2d = GetComponent<BoxCollider2D>();
        gravityScale = myRigidbody2d.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive)
        {
            HandleMovement();
            HandleJump();
            HandleClimb();
        }
        HandleDead();
    }

    private void HandleClimb()
    {
        var climbingMaskId = LayerMask.GetMask("Climbing");
        var isClimbing = myFeetsCollider2d.IsTouchingLayers(climbingMaskId);

        if (isClimbing)
        {
            myRigidbody2d.gravityScale = 0;
            myAnimator.SetBool("isRunning", false);
            myAnimator.SetBool("isClimbing", true);
            var verticalAxis = Input.GetAxis("Vertical");

            var verticalMovement = verticalAxis * climbingSpeed;

            myRigidbody2d.velocity = new Vector2(myRigidbody2d.velocity.x, verticalMovement);
        }
        else
        {
            myRigidbody2d.gravityScale = gravityScale;
            myAnimator.SetBool("isClimbing", false);
        }
    }

    private void HandleJump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            var groundMaskId = LayerMask.GetMask("Ground");
            var isGrounded = myFeetsCollider2d.IsTouchingLayers(groundMaskId);

            if (isGrounded)
                availableJumps = numberOfJumps;

            if (availableJumps > 0)
            {
                var jumpVector = new Vector2(0, jumpForce);

                if (availableJumps != numberOfJumps)
                    jumpVector /= 1.5f;

                //myRigidbody2.AddForce(jumpVector, ForceMode2D.Impulse);
                myRigidbody2d.velocity += jumpVector;

                availableJumps--;
            }
        }
    }

    private void HandleMovement()
    {
        var horizontalAxis = Input.GetAxis("Horizontal");
        var isRunning = horizontalAxis != 0;

        myAnimator.SetBool("isRunning", isRunning);

        if (isRunning)
            transform.localScale = new Vector3(Mathf.Sign(horizontalAxis), 1, 1);

        var horizontalMovement = horizontalAxis * movementSpeed;// * Time.deltaTime;
        //transform.Translate(horizontalMovement, 0, 0);

        myRigidbody2d.velocity = new Vector2(horizontalMovement, myRigidbody2d.velocity.y);
    }


    private void HandleDead()
    {
        if (isAlive && myBodyCollider2d.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazards")))
        {
            isAlive = false;
            myRigidbody2d.velocity = deadKick;
            myAnimator.SetTrigger("Dying");
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }
    }
}
