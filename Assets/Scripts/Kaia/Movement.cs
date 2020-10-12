using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    // ---- MOVEMENT Variables ----
    private float moveInput; // obtiene valores del Input.GetAxis("Horizontal")
    Vector3 movement;
    [SerializeField] float speed;
    private bool faceingRight; // metodos: Movement, Flip
    Rigidbody2D rb;


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

    // ---- Animator -----
     Animator animator;

    // ----- Audio -------
    Kaia_AudioController Kaia_Audio;


    // ---- DASH Variables ----

    [SerializeField] float dashForce, dashCd, dashCount;
    private bool canDash, startCoolDown;
    public Text dashCdText;
    public GameObject dashParticles;

    void Awake()
    {
        //Jump

        rb = GetComponent<Rigidbody2D>();
        extraJump = extraJumpsValue;
        hasDoubleJump = false;

        //Dash

        dashCount = dashCd;
        canDash = true;
        startCoolDown = false;
        dashCdText.text = dashCount.ToString();

        animator = GetComponent<Animator>();
        Kaia_Audio = GetComponent<Kaia_AudioController>();
    }

    // Update is called once per frame

    public void Jump()
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRaious, whatGround); // Booleano para verificar si el jugador toca el suelo.

        if (isGrounded == true && Input.GetButtonDown("Jump") && extraJump > 0)
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.velocity = Vector2.up * jumpForce;
            Kaia_Audio.PlayJumpSound();

        }
        else if (Input.GetButtonDown("Jump") && extraJump > 0 && hasDoubleJump == true) // Aqui se hace el doble salto.
        {
            rb.velocity = Vector2.up * secondJumpForce;
            animator.SetBool("jumping", true);
            extraJump--;
            Kaia_Audio.PlayJumpSound();

        }


        if (Input.GetButton("Jump") && isJumping == true) // Minetras se deja la barra espaciadora slata más alto
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

    public void Move()
     {
        moveInput = Input.GetAxis("Horizontal");
        movement = new Vector3(moveInput, 0f, 0f);
        transform.position += movement * Time.deltaTime * speed;
        animator.SetFloat("speed", Mathf.Abs(moveInput));

        if (movement.x < 0 && !faceingRight)
        {
            flip();

        }
        else if (movement.x > 0 && faceingRight)
        {
            flip();
        }
     }

    public  void Dash()
    {
        if (!faceingRight && Input.GetKeyDown(KeyCode.X) && canDash)
        {
            rb.velocity = Vector2.right * dashForce;
            Instantiate(dashParticles, this.transform.position, dashParticles.transform.rotation);
            startCoolDown = true;
            canDash = false;
            Invoke("cooldownDash", dashCd); // resetea variables (Mirar método cooldownDash)
        }

        if (faceingRight && Input.GetKeyDown(KeyCode.X) && canDash)
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
    }

    

    public void flip()
    {
        faceingRight = !faceingRight;

        transform.Rotate(0f, 180f, 0f);
    }

    public void cooldownDash()
    {
        canDash = true;
        dashCount = dashCd;
        dashCdText.text = dashCount.ToString();
        startCoolDown = false;
    }

}
