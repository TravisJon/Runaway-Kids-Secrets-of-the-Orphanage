using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator anim;

    private float dirX = 0f;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 14f;
    [SerializeField]
    private int totalJump;
    private int airCount;
    public bool isGrounded;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        Run();
        Move();
        Jump();
    }
    private void Move()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);
    }
    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && airCount < totalJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            airCount++;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ground")
        {
            airCount = 0;
            isGrounded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Ground")
        {
            isGrounded = false;
        }
    }

    private void Run()
    {
        if (Input.GetKey(KeyCode.LeftShift)) 
        {
            moveSpeed = 14f;
        }
        else 
        { 
            moveSpeed = 5f; 
        }

        UpdateAnimationState();
    }

    private void UpdateAnimationState()
    {
        if (dirX > 0f)
        {
            anim.SetBool("walk", true);
            sprite.flipX = false;
        }
        else if (dirX < 0f)
        {
            anim.SetBool("walk", true);
            sprite.flipX = true;
        }
        else
        {
            anim.SetBool("walk", false);
        }
    }
}
