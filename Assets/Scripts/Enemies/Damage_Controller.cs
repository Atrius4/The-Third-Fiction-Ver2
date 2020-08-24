using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage_Controller : MonoBehaviour
{
    public int AttackDamage;
    public float PunchForce;
    public GameObject EnemyObject;

    Rigidbody2D EnemyRB, PlayerRB;
    Vector2 VecDif;

    // Start is called before the first frame update
    void Awake()
    {
        EnemyRB = EnemyObject.GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D ColliderPlayer)
    {
        if (ColliderPlayer.CompareTag("Player"))
        {
            PlayerRB = ColliderPlayer.GetComponent<Rigidbody2D>();
            ColliderPlayer.GetComponent<Player_Controller>().TakeDamage(AttackDamage);

            VecDif = EnemyObject.transform.position - ColliderPlayer.transform.position;
            PlayerRB.AddForce(VecDif.normalized * PunchForce * -1);
        }
    }
}
