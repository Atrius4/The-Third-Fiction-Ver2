using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller2 : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] Animator animator;
    private int state; //Enables Move, Jump, etc. Depending on the situation
    



    // ---- MOVEMENT Variables ----

    private float moveInput;
    Vector3 movement;
    [SerializeField] float speed;
    private bool faceingright;


    // ---- JUMP Variables ----

    [SerializeField] float jumpForce;
    private bool isGrounded;
    public Transform feetPos;
    public float checkRaious;
    public LayerMask whatGround;

    public float jumpTimeCounter;
    public float jumpTime;
    public bool isJumping;

    private int extraJump;
    public int extraJumpsValue;
    public float secondJumpForce;

    // ---- DASH Variables ----

    [SerializeField] float dashForce;
    private bool canDash;

    // ---- SHOOT Variables ---

    public float shootTime;
    public float ShootTimeCounter;

    // ---- UI Variables ----

    //public Health_Bar healthBar;
    [SerializeField] int maxHealth;
    public int currentHealth;

    // ---- Check Point Variables ----
    public Vector3 respawnPoint;


    // ----------------------------------------------------------- CODE ---------------------------------------------------------------------

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        extraJump = extraJumpsValue;
        canDash = true;
        ShootTimeCounter = shootTime;
        currentHealth = maxHealth;
        //healthBar.SetMaxHealth(maxHealth);
        state = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // ---- JUMP Controller ----

        switch (state)
        {
            case 0: //Idle, Moving
                Jump();
                Movement();
                Dash();
                Attack();
                break;
            case 1: //Attacking
                Attack();
                ShootTimeCounter -= Time.deltaTime;
                break;
            case 2:
                ShootTimeCounter -= Time.deltaTime;
                Attack();
                if (animator.GetBool("jumping"))
                {
                    transform.position += new Vector3(0, -12 * Time.deltaTime, 0);
                }
                if (ShootTimeCounter <= 0) { state = 0; animator.SetBool("runShooting", false); }        
                break;
        }

        // ---- End JUMP Controller ----

        // ---- DASH Controller----





        // ---- End DASH Controller ----

        // ---- ANIMATION Controller ----  *P.S -> No todas las animaciones estan áca, algunas estan con sus respectivos controladores.

        // -> Shooting

        

        // ---- End Animation Controller ----

        // ---- HEALTH Controller ----

        if (Input.GetKeyDown(KeyCode.U))
        {
            TakeDamage(20);
        }

        // ---- End HEALTH Controller ----
    }

    void FixedUpdate()
    {
        // -------------------------- Movement by Physics--------------------
        //moveInput = Input.GetAxis("Horizontal");
        //rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        //animator.SetFloat("speed", Math.Abs(moveInput));

        //if (facingRight == false && moveInput > 0)
        //{
        //    flipSprite();
        //}
        //else if (facingRight == true && moveInput < 0)
        //{
        //    flipSprite();
        //}

        // -------------------------- Movement by Transform
       


    }

    //void flipSprite() // Alternative function for Flip
    //{
    //    facingRight = !facingRight;

    //    Vector3 scaler = transform.localScale;
    //    scaler.x *= -1;
    //    transform.localScale = scaler;
    //}

    public void Movement() //Movement controller by transform
    {
        moveInput = Input.GetAxis("Horizontal");
        movement = new Vector3(moveInput, 0f, 0f);
        transform.position += movement * Time.deltaTime * speed;
        animator.SetFloat("speed", Mathf.Abs(moveInput));

        if (movement.x < 0 && !faceingright)
        {
            flip();

        }
        else if (movement.x > 0 && faceingright)
        {
            flip();
        }
    }
    
    public void Jump() //Jump Controller (Deactivated when attacking)
   
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRaious, whatGround);

        if (isGrounded == true && Input.GetButtonDown("Jump") && extraJump > 0)
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.velocity = Vector2.up * jumpForce;

        }
        else if (Input.GetButtonDown("Jump") && extraJump > 0) // Aqui se hace el doble salto.
        {
            rb.velocity = Vector2.up * secondJumpForce;
            animator.SetBool("jumping", true);
            extraJump--;
        }


        if (Input.GetButton("Jump") && isJumping == true)
        {

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

        if (Input.GetButtonUp("Jump"))
        {
            isJumping = false;
        }

        if (isGrounded == true)
        {
            animator.SetBool("jumping", false);
            extraJump = extraJumpsValue;
        }
        else
        {
            animator.SetBool("jumping", true);
        }
    }

    public void Dash() //Dash Controller (Deactivated when attacking)
    {
        if (!faceingright && Input.GetKeyDown(KeyCode.X))
        {

            if (canDash)
            {
                rb.velocity = Vector2.right * dashForce;
                canDash = false;
                Invoke("cooldownDash", 2);
            }

        }

        if (faceingright && Input.GetKeyDown(KeyCode.X))
        {

            if (canDash)
            {
                rb.velocity = Vector2.left * dashForce;
                canDash = false;
                Invoke("cooldownDash", 2);
            }
        }
    }

    void flip()
    {
        faceingright = !faceingright;

        transform.Rotate(0f, 180f, 0f);

        //if (movement.x < 0)
        //{
        //    transform.localScale = new Vector2(-1, 1);
        //}
        //if (movement.x > 0)
        //{
        //    transform.localScale = new Vector2(1, 1);
        //}

    }

    void cooldownDash()
    {
        canDash = true;
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;

        //healthBar.SetHealth(currentHealth);
    }

    public void Attack()  //Attacking and chain attacks
    {
        if (state == 0) {
            if (Input.GetKeyDown(KeyCode.Z) && moveInput == 0)
            {
                state = 1;
                animator.SetBool("shooting", true);      
            }
            else if (Input.GetKeyDown(KeyCode.Z) && moveInput != 0)
            {
                // ShootTimeCounter = shootTime;
                state = 2;
                animator.SetBool("runShooting", true);
            }
        }
        if(state == 1)
        {
           
            if (Input.GetKeyDown(KeyCode.Z))
            {
                ShootTimeCounter = shootTime;
            }
           
        }

        if (ShootTimeCounter <= 0)
        {
            animator.SetBool("runShooting", false);
            animator.SetBool("shooting", false);
            state = 0;

        }

        //if (Input.GetKey(KeyCode.Z) && movement.x != 0)
        //{
        //    animator.SetBool("shooting", false);
        //}



        //if (Input.GetKeyDown(KeyCode.Z) && animator.GetBool("shooting"))
        //{
        //    ShootTimeCounter = shootTime;
        //    animator.SetBool("shootingLoop", true);
        //    animator.SetBool("shooting",false);
        //}
        //if (Input.GetKeyDown(KeyCode.Z) && animator.GetBool("shootingLoop"))
        //{
        //    ShootTimeCounter = shootTime;
        //    animator.SetBool("shooting", true);
        //    animator.SetBool("shootingLoop", false);

        //}

        //if (Input.GetKeyUp(KeyCode.Z))
        //{
        //    animator.SetBool("shooting", false);
        //}

        //if (Mathf.Abs(moveInput) <= 0.01)
        //{
        //    ShootTimeCounter = 0;
        //}

        //if (ShootTimeCounter <= 0)
        //{
        //    animator.SetBool("runShooting", false);
        //}
        //else
        //{
        //    ShootTimeCounter -= Time.deltaTime;
        //}
    }


    public void AttackFinish() //End Attacks and Disable Hit Box
    {
        if (animator.GetBool("runShooting"))
        {
            animator.SetBool("runShooting", false);
            state = 0;

        }

        if (ShootTimeCounter<= 0)
        {
            animator.SetBool("shooting", false);
            

            ShootTimeCounter = shootTime;
            state = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Checkpoint")
        {
            respawnPoint = other.transform.position;
        }
        if (other.tag == "FallHazard")
        {
            transform.position = respawnPoint;
            TakeDamage(30);
        }
    }




}
