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
    //---- Singleton ----


    // Scripts que se comunican con la fachada

    Movement movement;
    Distance_Weapon weapon;
    Kaia_AudioController audios;
    Kaia_Animator animator;
    Level_Manager lvlManager;
    Mission_Manager misiones;
    CollectablesManager collectables;

    // ---- Jump Variables ----
    private bool isGrounded;
    public Transform feetPos;
    public float checkRaious;
    public LayerMask whatGround;

    // ---- Dash Variables ----
    [SerializeField] private float dashCount, dashCD;

    // ---- UI Variables ----

    public Health_Bar healthBar;
    [SerializeField] int maxHealth;
    public int currentHealth;
    public Text dashCdText;

    private int lifes;
    public Image lifeSprite;



    // ---- Check Point Variables ----
    public Vector3 respawnPoint;

    // ---- Shop & Inventory Variables ----
    public Slot slot1;

    // ----------------------------------------------------------- CODE ---------------------------------------------------------------------


    private void Awake()
    {
        movement = GetComponent<Movement>();
        weapon = GetComponent<Distance_Weapon>();
        audios = GetComponent<Kaia_AudioController>();
        animator = GetComponent<Kaia_Animator>();
        lvlManager = GetComponent<Level_Manager>();
        misiones = GameObject.Find("UI_Missions").GetComponent<Mission_Manager>();
        collectables = GetComponent<CollectablesManager>();
    }
    void Start()
    {
        dashCount = dashCD;
        dashCdText.text = dashCount.ToString();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        lifeSprite.enabled = true;
        slot1 = GetComponent<Slot>();
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Horizontal") != 0)
        {
            movement.Move(Input.GetAxis("Horizontal"));
            animator.MovementAnimation(Input.GetAxis("Horizontal"));
            
        }
        else
        {
            animator.MovementAnimation(0);
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            weapon.Shoot();
        }
        animator.ShootAnimation();

        IsGrounded();

        if (Input.GetButton("Jump"))
        {
            movement.Jump(isGrounded);
        }
        if (Input.GetButtonUp("Jump"))
        {
            movement.AlreadyJumped();
        }
        
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (dashCount <= 0) { dashCount = dashCD; }
            movement.Dash();
        }
        if (dashCount >= 0)
        {
            dashCount -= Time.deltaTime;
            dashCdText.text = dashCount.ToString();
        }
        else
        {
            movement.Resetcooldown();
            dashCdText.text = dashCD.ToString();
        }
    }
    void Die()
    {
        if (lifes >= 1)
        {
            lifes--;
            lifeSprite.enabled = false;
            transform.position = respawnPoint;
            currentHealth = maxHealth;
            healthBar.SetHealth(currentHealth);
            return;
        }
        
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        audios.PlayHurtSound();
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void EnemyKilled(int gainedXp) // esta funcion se llama en el enemyHp Manager
    {
        lvlManager.GainXP(10);
        misiones.UpdateEnemies();
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
            movement.hasDoubleJump = true;
            misiones.DJObtained();
            Destroy(other.gameObject);
        }
        if(other.tag == "Coin")
        {
            collectables.ObtainedCoins();
            misiones.CoinObtained();
        }

    }


    public void RegainLife()
    {
        currentHealth += 30;
        healthBar.SetHealth(currentHealth);
    }

    public void IsGrounded()
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRaious, whatGround); // Booleano para verificar si el jugador toca el suelo.
        animator.JumpAnimation(isGrounded);
    }
}
