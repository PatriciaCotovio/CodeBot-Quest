using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f;
    public float jumpForce = 300f;
    private Rigidbody2D rb;
    private Animator animator;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Movement();
        HandleAnimations();
        CheckJump();
    }

    void Movement()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        transform.position += new Vector3(moveHorizontal, 0, 0) * speed * Time.deltaTime;

        // Handle flipping character sprite
        if (moveHorizontal != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(moveHorizontal), 1, 1);
        }
    }

    void HandleAnimations()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        bool isRunning = Mathf.Abs(moveHorizontal) > 0.5f;
        bool isWalking = Mathf.Abs(moveHorizontal) > 0 && Mathf.Abs(moveHorizontal) <= 0.5f;

        // Set animation parameters
        animator.SetBool("isRunning", isRunning);
        animator.SetBool("isWalking", isWalking);
        animator.SetBool("isClimbing", Mathf.Abs(Input.GetAxis("Vertical")) > 0);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            animator.SetTrigger("jump");
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            animator.SetTrigger("shoot");
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            animator.SetTrigger("slide");
        }
    }

    void CheckJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(new Vector2(0, jumpForce));
            isGrounded = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}

