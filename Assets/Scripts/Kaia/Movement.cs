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


    public float jumpTimeCounter;
    public float jumpTime;
    public bool isJumping;

    private int extraJump;
    public int extraJumpsValue;
    public float secondJumpForce;

    public bool hasDoubleJump;

    // ---- Animator -----

    // ----- Audio -------
    Kaia_AudioController Kaia_Audio;


    // ---- DASH Variables ----

    [SerializeField] float dashForce;
    private bool canDash, startCoolDown;

    public GameObject dashParticles;

    void Awake()
    {
        //Jump

        rb = GetComponent<Rigidbody2D>();
        extraJump = extraJumpsValue;
        hasDoubleJump = false;

        //Dash


        canDash = true;
        startCoolDown = false;


        //animator = GetComponent<Animator>();
        Kaia_Audio = GetComponent<Kaia_AudioController>();
    }

    // Update is called once per frame

    public void Jump(bool Grounded)
    {
        jumpTimeCounter -= Time.deltaTime;

        Debug.Log(Grounded);
        if (Grounded == true)
        {

            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.velocity = Vector2.up * jumpForce;
            Kaia_Audio.PlayJumpSound();

        }
        else if (hasDoubleJump == true && extraJump>0) // Aqui se hace el doble salto.
        {
            rb.velocity = Vector2.up * secondJumpForce;
            extraJump--;
            Kaia_Audio.PlayJumpSound();

        }
        if (jumpTimeCounter > 0)
        {
            rb.velocity = Vector2.up * jumpForce;

        }


    }

    public void AlreadyJumped()
    {
        jumpTimeCounter = -1;
        extraJump = extraJumpsValue;
    }

    public void Move(float input)
     {
        
        movement = new Vector3(input, 0f, 0f);
        transform.position += movement * Time.deltaTime * speed;
   

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
        if (!faceingRight && canDash)
        {
            rb.velocity = Vector2.right * dashForce;
            Instantiate(dashParticles, this.transform.position, dashParticles.transform.rotation);
            canDash = false;
            
        }

        if (faceingRight && canDash)
        {
            rb.velocity = Vector2.left * dashForce;
            Instantiate(dashParticles, this.transform.position, dashParticles.transform.rotation);
            canDash = false;
        }
    }

    

    public void flip()
    {
        faceingRight = !faceingRight;

        transform.Rotate(0f, 180f, 0f);
    }

    public void Resetcooldown()
    {
        canDash = true;
    }

}
