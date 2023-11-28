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
    public bool isCrouching;

    private Vector2 normalCollSize;
    private Vector2 normalCollOffs;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        normalCollSize = GetComponent<BoxCollider2D>().size;
        normalCollOffs = GetComponent<BoxCollider2D>().offset;
    }

    // Update is called once per frame
    private void Update()
    {
        Run();
        Move();
        Jump();
        Crouch();
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

    private void Crouch()
    {
        if (Input.GetAxisRaw("Vertical") < 0 && isGrounded)
        {
            isCrouching = true;
            GetComponent<BoxCollider2D>().size = new Vector2(0.7992616f, 1.52f);
            GetComponent<BoxCollider2D>().offset = new Vector2(0.007851839f, -0.8347909f);
            if (Input.GetButtonDown("Jump"))
            {
                rb.velocity = new Vector2(rb.velocity.x, 0f);
            }
        }
        else
        {
            isCrouching = false;
            GetComponent<BoxCollider2D>().size = normalCollSize;
            GetComponent<BoxCollider2D>().offset = normalCollOffs;
        }
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
