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


    // Scripts que se comunican con la fachada

    Movement movement;
    Distance_Weapon weapon;
    Kaia_AudioController audios;
    Kaia_Animator animator;

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


    private void Awake()
    {
        movement = GetComponent<Movement>();
        weapon = GetComponent<Distance_Weapon>();
        audios = GetComponent<Kaia_AudioController>();
        animator = GetComponent<Kaia_Animator>();
    }
    void Start()
    {

        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        xpBar.slider.maxValue = xpToNextLevel;
        xpBar.slider.value = 0;
        lvlManager.SetLevel(level);
        lifeSprite.enabled = true;
        slot1 = GetComponent<Slot>();
    }


    // Update is called once per frame
    void Update()
    {
        movement.Move();

        if (Input.GetKeyDown(KeyCode.Z))
        {
            weapon.Shoot();
        }

        movement.Jump();

        animator.Animation();

        movement.Dash();

        HealthController();


        // ---- XP and Lvl Controller ----
        XpLevel();
    }

    

    

    

    

    

    void HealthController()
    {
        if (Input.GetKeyDown(KeyCode.U)) // para testeo
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
    }

    void XpLevel()
    {
        if (Input.GetKeyDown(KeyCode.I)) // ----> testeo del sistema de xp y nivel.
        {
            gainXp(10);
        }

        if (xpBar.slider.value >= xpBar.slider.maxValue)
        {
            xpBar.slider.value = 0;
            audios.PlaylvlUpSound();
            level++;
            lvlManager.SetLevel(level);
        }

    }




    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        audios.PlayHurtSound();
    }

    public void gainXp(int gainedXp) // esta funcion se llama en el enemyHp Manager
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
            movement.hasDoubleJump = true;
            Destroy(other.gameObject);
        }

    }

    public void AdEnemy() // esta funcion se llama en el enemyHp Manager y se usa para la misión de enemigos.
    {
        enemyCount++;
    }

    public void RegainLife()
    {
        currentHealth += 30;
        healthBar.SetHealth(currentHealth);
    }

}
