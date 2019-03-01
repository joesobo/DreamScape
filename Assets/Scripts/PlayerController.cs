using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed;
    public float jumpForce;
    private float moveInput;

    private Rigidbody2D rb;

    public bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    public int numJumps = 2;
    private int extraJumps;

    private float jumpTimeCounter;
    private float jumpTimeCounter2;
    public float jumpTime = 0.35f;
    private bool isJumping = false;

    public bool unlocker = false;

    private Animator anim;
    private Transform childTransform;

    private bool jump1 = false;
    private bool jump2 = false;

    public GameObject groundHitParticle;
    public GameObject runGroundParticle;
    private bool groundHitCheck = false;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        childTransform = gameObject.transform.Find("Player Body").GetComponent<Transform>();
        extraJumps = numJumps;
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    private void FixedUpdate()
    {
        //left/right move input
        moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        //running animation
        if(moveInput == 0)
        {
            anim.SetBool("isRunning", false);
        }
        else
        {
            anim.SetBool("isRunning", true);
        }

        //flips character
        if (moveInput > 0)
        {
            //transform.eulerAngles = Vector3.zero;
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (moveInput < 0)
        {
            //transform.eulerAngles = new Vector3(0, 180, 0);
            transform.localScale = new Vector3(-1, 1, 1);
        }

        //unlocks character movement
        if (unlocker)
        {
            unlock();
            unlocker = false;
        }
    }

    private void Update()
    {
        //check if on ground
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        if(groundHitCheck && isGrounded)
        {
            Instantiate(groundHitParticle, new Vector3(transform.position.x, transform.position.y - .5f, transform.position.z), Quaternion.identity);
            groundHitCheck = false;
        }

        if (isGrounded)
        {
            jump1 = false;
            jump2 = false;
            isJumping = false;
            extraJumps = numJumps;
            anim.SetBool("isJumping", false);

            if(Input.GetAxisRaw("Horizontal") != 0)
            {
                Instantiate(runGroundParticle, new Vector3(transform.position.x, transform.position.y - .5f, transform.position.z), Quaternion.identity);
            }
        }
        else
        {
            groundHitCheck = true;
            anim.SetBool("isJumping", true);
        }

        //first jump
        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            //Debug.Log("Jump 1");
            anim.SetTrigger("takeoff");
            jumpTimeCounter = jumpTime;
            isJumping = true;
            rb.velocity = Vector2.up * jumpForce;
            jump1 = true;
        }        

        //extra jumps
        if (Input.GetKeyDown(KeyCode.W) && extraJumps > 0 && !isGrounded)
        {
            //Debug.Log("Jump 2");
            anim.SetTrigger("takeoff");
            jumpTimeCounter2 = jumpTime;
            rb.velocity = Vector2.up * jumpForce;
            extraJumps--;
            jump2 = true;
        }
        

        //adds delay to first jump based on length of press
        if (Input.GetKey(KeyCode.W) && isJumping && jump1)
        {
            //Debug.Log("1");
            if (jumpTimeCounter > 0)
            {
                rb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }

        //adds delay to second jump based on length of press
        if (Input.GetKey(KeyCode.W) && !isGrounded && jump2)
        {
            //Debug.Log("2");
            if (jumpTimeCounter2 > 0)
            {
                rb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter2 -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            isJumping = false;
            jump1 = false;
        }
    }

    public void unlock(){
        rb.constraints = RigidbodyConstraints2D.None;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
}
