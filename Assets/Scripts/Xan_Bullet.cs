using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Xan_Bullet : MonoBehaviour
{
    public float bulletSpeed;
    public Rigidbody2D bulletRb;
    public GameObject  explosionParticles;


    public int Damage;

    void Start()
    {
        bulletRb.velocity = transform.right * bulletSpeed;
    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D hit)
    {
        if (hit.gameObject.CompareTag("Ground") || hit.gameObject.CompareTag("Wall"))
        {
            Instantiate(explosionParticles, this.transform.position, explosionParticles.transform.rotation);
            Destroy(gameObject);
        }
        else if (hit.gameObject.CompareTag("Enemie"))
        {
            Instantiate(explosionParticles, this.transform.position, explosionParticles.transform.rotation);
            hit.GetComponent<Enemy_HPManager>().TakeDamage(Damage);
            Destroy(gameObject);
        }
    }
}
