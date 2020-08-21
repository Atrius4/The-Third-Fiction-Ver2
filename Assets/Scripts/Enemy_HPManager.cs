using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy_HPManager : MonoBehaviour
{
    // Variables del Enemigo
    public Animator anim;
    public Enemy_UIManager Enemy_UI;
    public GameObject Object;
    Player_Controller player;
    private Renderer render;
    private Collider2D collider;
    private float destructionCounter = 1.8f;
    public int Health;
    public int MaxHealth;


    void Awake()
    {
        anim = GetComponent<Animator>();
        Enemy_UI = GetComponent<Enemy_UIManager>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Controller>();
        render = GetComponent<Renderer>();
        collider = GetComponent<Collider2D>();      
    }

    //Activar la animacion cuando la Vida llega a 0
    //La animacion de "Explosion" tiene un evento que destruye el objeto.
    void Update()
    {
        if(Health <= 0)
        {
            anim.SetBool("IsDeath", true);
        }
        if (anim.GetBool("IsDeath"))
        {
            destructionCounter -= Time.deltaTime;
            if(destructionCounter <= 0) { Destroy(Object); }
        }
    }

    //La animacion activa esta funcion en un evento.
    public void DestroyOnTime()
    {
        player.gainXp(10);
        render.enabled = false;
        collider.enabled = false;
        Enemy_UI.DisableHP();
    }

    public void TakeDamage(int Damage)
    {
        Health -= Damage;
        Enemy_UI.HP_Update();
    }


}
