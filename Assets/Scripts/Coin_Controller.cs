using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin_Controller : MonoBehaviour
{
    public GameObject coinParticles;
    public GameObject coinSound;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Instantiate(coinParticles, this.transform.position, coinParticles.transform.rotation);
            Instantiate(coinSound, this.transform.position, coinParticles.transform.rotation);

            Destroy(gameObject);

        }
    }


}
