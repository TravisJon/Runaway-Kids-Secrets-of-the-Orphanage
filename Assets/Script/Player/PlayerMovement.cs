using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator anim;

    private float dirX = 0f;
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float runSpeed = 8f;
    [SerializeField] private float jumpForce = 8f;
    [SerializeField] private int totalJump;
    private int airCount;
    public bool isGrounded;
    public bool isCrouching;
    public bool isRunning;
    private bool canRun = true;
    private bool isPlayerRunning = false;

    private Vector2 normalCollSize;
    private Vector2 normalCollOffs;

    [SerializeField]
    private Image bar;

    [SerializeField] private float maxRunTime = 5f; 
    private float currentRunTime = 0f; 
    private bool isFull = false;
    private bool isDecreasingRunTime = false;

    //[SerializeField] private AudioSource walkSoundEffect;

    //private enum MovementState { idle, walk, jumping, falling }

    // Start is called before the first frame update
    private void Start()
    {
        if (InGameDatabase.instance != null)
        {
            if (SceneManager.GetActiveScene().name == "Scene 2")
            {
                if (InGameDatabase.instance.playerPosition.x != 0f && InGameDatabase.instance.playerPosition.y != 0f)
                {
                    transform.position = InGameDatabase.instance.playerPosition;
                }
            }
            
        }

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
        rb.velocity = new Vector2(dirX * (isRunning ? runSpeed : moveSpeed), rb.velocity.y);
        //AudioPlayer.instance.PlaySFX(0);

        if (dirX < 0)
        {
            sprite.flipX = true;
        }

        if (dirX > 0)
        {
            sprite.flipX = false;
        }

        if (dirX == 0 && isGrounded && !isCrouching && !isRunning)
        {
            anim.Play("Player_Idle");
        }

        if (dirX != 0 && isGrounded && !isCrouching && !isRunning)
        {
            AudioPlayer.instance.PlaySFX(0);
            anim.Play("Player_Walk");
        }
    }
    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && airCount < totalJump && !isCrouching)
        {
            AudioPlayer.instance.PlaySFX(1);
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            airCount++;
        }

        //UpdateAnimationState();
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
            anim.Play("Player_Jump");
        }
    }

    private void Run()
    {
        if (canRun && Input.GetKey(KeyCode.LeftShift) && dirX != 0 && !isCrouching)
        {
            if (currentRunTime < maxRunTime) // Check if the bar is not full
            {
                isRunning = true;
                moveSpeed = 8f;

                if (!isPlayerRunning)
                {
                    isPlayerRunning = true;
                    StartCoroutine(UpdateRunningTime());
                }

                float fillAmount = Mathf.Clamp01(currentRunTime / maxRunTime);
                bar.fillAmount = fillAmount;

                if (currentRunTime >= maxRunTime)
                {
                    canRun = false;
                    BarFull();
                }

                if (isDecreasingRunTime)
                {
                    StopCoroutine(DecreaseRunningTime());
                    isDecreasingRunTime = false;
                }
            }
            else
            {
                if (!canRun)
                {
                    isRunning = false;
                    moveSpeed = 3f;

                    if (isPlayerRunning)
                    {
                        isPlayerRunning = false;

                        if (!isDecreasingRunTime)
                        {
                            isDecreasingRunTime = true;
                            StartCoroutine(DecreaseRunningTime());
                        }
                    }
                }
            }
        }
        else
        {
            isRunning = false;
            moveSpeed = 3f;
            if (!canRun && currentRunTime <= 0f)
            {
                canRun = true;
            }
            if (isPlayerRunning)
            {
                isPlayerRunning = false;

                if (!isDecreasingRunTime)
                {
                    isDecreasingRunTime = true;
                    StartCoroutine(DecreaseRunningTime());
                }
            }
        }

        if (isRunning && dirX != 0 && isGrounded && !isCrouching)
        {
            AudioPlayer.instance.PlaySFX(0);
            anim.Play("Player_Run");
        }
        else if (!isRunning && dirX != 0 && isGrounded && !isCrouching)
        {
            anim.Play("Player_Walk");
        }
    }


    private IEnumerator DecreaseRunningTime()
    {
        while (isDecreasingRunTime && currentRunTime > 0f)
        {
            yield return new WaitForSeconds(Time.deltaTime);
            currentRunTime -= Time.deltaTime;
            float fillAmount = Mathf.Clamp01(currentRunTime / maxRunTime);
            bar.fillAmount = fillAmount;

            if (currentRunTime <= 0f)
            {
                canRun = true;
            }
        }
    }
    private IEnumerator UpdateRunningTime()
    {
        while (isPlayerRunning && currentRunTime < maxRunTime)
        {
            yield return new WaitForSeconds(Time.deltaTime);
            // Increase the currentRunTime more quickly
            currentRunTime += Time.deltaTime * 2f; // You can adjust the multiplier to control the speed
        }

        if (currentRunTime >= maxRunTime)
        {
            BarFull();
        }
    }


    private void BarFull()
    {
        Debug.Log("Game Over");
        isFull = true;
        canRun = false;
    }


    private void Crouch()
    {
        if (Input.GetAxisRaw("Vertical") < 0 && isGrounded)
        {
            isCrouching = true;
            GetComponent<BoxCollider2D>().size = new Vector2(0.7992616f, 1.52f);
            GetComponent<BoxCollider2D>().offset = new Vector2(0.007851839f, -0.8347909f);
            AudioPlayer.instance.PlaySFX(1);
            anim.Play("Player_Crouch");
            /*if (Input.GetButtonDown("Jump"))
            {
                rb.velocity = new Vector2(rb.velocity.x, 0f);
            }*/
        }
        else
        {
            isCrouching = false;
            GetComponent<BoxCollider2D>().size = normalCollSize;
            GetComponent<BoxCollider2D>().offset = normalCollOffs;
        }
    }

    /*private void UpdateAnimationState()
    {
        MovementState state;

        if (dirX > 0f)
        {
            state = MovementState.walk;
            sprite.flipX = false;
        }
        else if (dirX < 0f)
        {
            state = MovementState.walk;
            sprite.flipX = true;
        }
        else
        {
            state = MovementState.idle;
        }

        if (rb.velocity.y > .1f)
        {
            state = MovementState.jumping;
        }
        else if (rb.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }

        anim.SetInteger("state", (int)state);
    }*/
}
