using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuriken_Particles_Manager : MonoBehaviour
{
    public float time;
    void Start()
    {
        
    }

    void Update()
    {
        if (time > 0)
        {
            time -= Time.deltaTime;
        }
        if (time <= 0)
        {
            Destroy(gameObject);
        }
    }
}
