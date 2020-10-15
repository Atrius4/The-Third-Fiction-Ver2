using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kaia_Animator : MonoBehaviour
{
    Animator animator;
    float moveInput;

    // ---- SHOOT Variables ---

    public float shootTime;
    private float ShootTimeCounter;
    void Awake()
    {
        animator = GetComponent<Animator>();
        ShootTimeCounter = shootTime;
    }

    // Update is called once per frame
    void Update()
    {
        moveInput = Input.GetAxis("Horizontal");
    }

    public void Animation()
    {
        // ---- Shooting Animation ----

        if (Input.GetKeyDown(KeyCode.Z) && moveInput == 0)
        {
            ShootTimeCounter = shootTime;
            animator.SetBool("shooting", true);
        }
        else if (Input.GetKeyDown(KeyCode.Z) && moveInput != 0)
        {
            ShootTimeCounter = shootTime;
            animator.SetBool("runShooting", true);
        }

        if (Input.GetKey(KeyCode.Z) && moveInput != 0) // blinjae para que si se deja undido y disparar y te mueves pasa a la animación de correr.
        {
            animator.SetBool("shooting", false);
        }


        if(moveInput == 0)
        {
            animator.SetBool("runShooting", false);
        }
        if (ShootTimeCounter <= 0)
        {
            animator.SetBool("runShooting", false);
            animator.SetBool("shooting", false);
        }
        else
        {
            ShootTimeCounter -= Time.deltaTime;
        }
    }

    public void MovementAnimation(float input)
    {
        animator.SetFloat("speed", Mathf.Abs(input));
    }

    public void JumpAnimation(bool isGrounded)
    {
        if (!isGrounded)
        {
            animator.SetBool("jumping", true);
        }
        else
        {
            animator.SetBool("jumping", false);
        }
    }
}
