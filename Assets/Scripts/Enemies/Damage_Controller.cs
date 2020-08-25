using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage_Controller : MonoBehaviour
{
    public int AttackDamage;
    public float PunchForce;
    public float punchCD;
    public GameObject EnemyObject;

    Rigidbody2D EnemyRB, PlayerRB;
    Vector2 VecDif;

    // Start is called before the first frame update
    void Awake()
    {
        EnemyRB = EnemyObject.GetComponent<Rigidbody2D>();
        
    }

    private void Update()
    {
        if (punchCD > 0)
        {
            punchCD -= Time.deltaTime;
        }
    }

    void OnTriggerStay2D(Collider2D ColliderPlayer)
    {
        if (ColliderPlayer.CompareTag("Player") && punchCD <= 0)
        {
            PlayerRB = ColliderPlayer.GetComponent<Rigidbody2D>();
            ColliderPlayer.GetComponent<Player_Controller>().TakeDamage(AttackDamage);

            VecDif = EnemyObject.transform.position - ColliderPlayer.transform.position;
            PlayerRB.AddForce(VecDif.normalized * PunchForce * -1);

            ColliderPlayer.GetComponent<DamageIndicator>().Damaged();
            punchCD = 1;
        }
    }
}
