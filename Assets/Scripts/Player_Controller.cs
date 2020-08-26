using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player_Controller : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] Animator animator;
    public AudioSource shootSound;
    public AudioSource jumpSound;
    public AudioSource hurtSound;



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

    public bool hasDoubleJump;

    // ---- DASH Variables ----

    [SerializeField] float dashForce, dashCd, dashCount;
    private bool canDash, startCoolDown;
    public Text dashCdText;
    public GameObject dashParticles;

    // ---- SHOOT Variables ---

    public float shootTime;
    private float ShootTimeCounter;

    // ---- UI Variables ----

    public Health_Bar healthBar;
    [SerializeField] int maxHealth;
    public int currentHealth;

    public Xp_Bar xpBar;
    public int xpToNextLevel;

    public Level_Manager lvlManager;
    public int level;
    public int enemyCount;
    public int lifes;
    public Image lifeSprite;



    // ---- Check Point Variables ----
    public Vector3 respawnPoint;

    // ---- Shop & Inventory Variables ----
    public Slot slot1;

    // ----------------------------------------------------------- CODE ---------------------------------------------------------------------

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        extraJump = extraJumpsValue;

        dashCount = dashCd;
        canDash = true;
        startCoolDown = false;
        dashCdText.text = dashCount.ToString();

        ShootTimeCounter = shootTime;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        xpBar.slider.maxValue = xpToNextLevel;
        xpBar.slider.value = 0;
        lvlManager.SetLevel(level);
        hasDoubleJump = false;
        shootSound = GetComponent<AudioSource>();
        lifeSprite.enabled = true;
        slot1 = GetComponent<Slot>();
    }


    // Update is called once per frame
    void Update()
    {
        // ---- JUMP Controller ----

        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRaious, whatGround);

        if (isGrounded == true && Input.GetButtonDown("Jump") && extraJump > 0)
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.velocity = Vector2.up * jumpForce;
            jumpSound.Play();

        }
        else if (Input.GetButtonDown("Jump") && extraJump > 0 && hasDoubleJump == true) // Aqui se hace el doble salto.
        {
            rb.velocity = Vector2.up * secondJumpForce;
            animator.SetBool("jumping", true);
            extraJump--;
            jumpSound.Play();

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

        if (Input.GetKeyDown(KeyCode.O)) // ----> testeo de power up de doble salto.
        {
            hasDoubleJump = true;
        }

        // ---- End JUMP Controller ----

        // ---- DASH Controller----


        if (!faceingright && Input.GetKeyDown(KeyCode.X) && canDash)
        {
            rb.velocity = Vector2.right * dashForce;
            Instantiate(dashParticles, this.transform.position, dashParticles.transform.rotation);
            startCoolDown = true;
            canDash = false;
            Invoke("cooldownDash", dashCd);
        }

        if (faceingright && Input.GetKeyDown(KeyCode.X) && canDash)
        {
            rb.velocity = Vector2.left * dashForce;
            Instantiate(dashParticles, this.transform.position, dashParticles.transform.rotation);
            startCoolDown = true;
            canDash = false;
            Invoke("cooldownDash", dashCd);
        }

        if (startCoolDown == true && dashCount >= 0)
        {
            dashCount -= 1 * Time.deltaTime;
            dashCdText.text = dashCount.ToString();
        }

        // ---- ANIMATION Controller ----  *P.S -> No todas las animaciones estan áca, algunas estan con sus respectivos controladores.

        // -> Shooting

        if (Input.GetKeyDown(KeyCode.Z) && moveInput == 0)
        {
            animator.SetBool("shooting", true);
            shootSound.pitch = UnityEngine.Random.Range(0.8f, 1.2f);
            shootSound.Play();
        }
        else if (Input.GetKeyDown(KeyCode.Z) && moveInput != 0)
        {
            ShootTimeCounter = shootTime;
            animator.SetBool("runShooting", true);
            shootSound.pitch = UnityEngine.Random.Range(1.2f, 1.35f);
            shootSound.Play();
        }

        if (Input.GetKey(KeyCode.Z) && movement.x != 0)
        {
            animator.SetBool("shooting", false);
        }

        if (Input.GetKeyUp(KeyCode.Z))
        {
            animator.SetBool("shooting", false);
        }

        if (Math.Abs(moveInput) <= 0.01)
        {
            ShootTimeCounter = 0;
        }

        if (ShootTimeCounter <= 0)
        {
            animator.SetBool("runShooting", false);
        }
        else
        {
            ShootTimeCounter -= Time.deltaTime;
        }

        // ---- End Animation Controller ----

        // ---- HEALTH Controller ----

        if (Input.GetKeyDown(KeyCode.U))
        {
            TakeDamage(20);
        }

        if (currentHealth <= 0 && lifes >= 1)
        {
            lifes--;
            lifeSprite.enabled = false;
            transform.position = respawnPoint;
            currentHealth = maxHealth;
            healthBar.SetHealth(currentHealth);
        }

        if (currentHealth <= 0 && lifes < 1)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        // ---- End HEALTH Controller ----

        // ---- XP and Lvl Controller ----
        if (Input.GetKeyDown(KeyCode.I)) // ----> testeo del sistema de xp y nivel.
        {
            gainXp(10);
        }

        if (xpBar.slider.value >= xpBar.slider.maxValue)
        {
            xpBar.slider.value = 0;
            level++;
            lvlManager.SetLevel(level);
        }


        // ---- End XP and Lvl Controller ----

        // -> Inventory Use
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
        moveInput = Input.GetAxis("Horizontal");
        movement = new Vector3(moveInput, 0f, 0f);
        transform.position += movement * Time.deltaTime * speed;
        animator.SetFloat("speed", Math.Abs(moveInput));

        if (movement.x < 0 && !faceingright )
        {
            flip();

        }
        else if (movement.x > 0 && faceingright)
        {
            flip();
        }




        // ---- End DASH Controller ----

    }

    //void flipSprite() // Alternative function for Flip
    //{
    //    facingRight = !facingRight;

    //    Vector3 scaler = transform.localScale;
    //    scaler.x *= -1;
    //    transform.localScale = scaler;
    //}

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
        dashCount = dashCd;
        dashCdText.text = dashCount.ToString();
        startCoolDown = false;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        hurtSound.Play();
    }

    public void gainXp(int gainedXp)
    {
        xpBar.xp += gainedXp;
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
        if (other.tag == "PowerUpJump" )
        {
            hasDoubleJump = true;
            Destroy(other.gameObject);
        }

    }


    public void AdEnemy()
    {
        enemyCount++;
    }

    public void RegainLife()
    {
        currentHealth += 30;
        healthBar.SetHealth(currentHealth);
    }

}
