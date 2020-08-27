using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy_HPManager : MonoBehaviour
{
    // Variables del Enemigo
    public Animator anim;
    public Enemy_UIManager Enemy_UI;
    public int Health;
    public int MaxHealth;
    public GameObject Object;
    Player_Controller player;
    private Renderer render;
    [SerializeField]private Collider2D dmgCollider,bodyCollider;
    private float destructionCounter = 1.8f;
    private Rigidbody2D rb;


    void Awake()
    {
        anim = GetComponent<Animator>();
        Enemy_UI = GetComponent<Enemy_UIManager>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Controller>();
        render = GetComponent<Renderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    //Activar la animacion cuando la Vida llega a 0
    //La animacion de "Explosion" tiene un evento que destruye el objeto.
    void Update()
    {
        if(Health <= 0)
        {
            anim.SetBool("IsDeath", true);
            dmgCollider.enabled = false;
            bodyCollider.enabled = false;
            rb.isKinematic = true;
        }
        if (anim.GetBool("IsDeath"))
        {
            destructionCounter -= Time.deltaTime;
            if(destructionCounter <= 0) { Destroy(Object); } // el Object destruye el prefab Padrino *italian noises*
        }
    }

    //La animacion activa esta funcion en un evento.
    public void DestroyOnTime() // evento de animación
    {
        player.gainXp(10);
        player.AdEnemy();
        render.enabled = false;
        Enemy_UI.DisableHP();
    }

    public void TakeDamage(int Damage)
    {
        Health -= Damage;
        Enemy_UI.HP_Update();
    }


}
