using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float bulletSpeed;
    public Rigidbody2D bulletRb;
    public int AttackDamage;

    void Awake()
    {
        bulletRb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        bulletRb.velocity = transform.right * bulletSpeed;
    }

    private void OnTriggerEnter2D(Collider2D hit)
    {
        if (hit.gameObject.CompareTag("Ground") || hit.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
        else if (hit.gameObject.CompareTag("Player"))
        {
            hit.GetComponent<Player_Controller>().TakeDamage(AttackDamage);
            hit.GetComponent<DamageIndicator>().Damaged();
            Destroy(gameObject);
        }
    }
}
