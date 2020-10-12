using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgSoundManager : MonoBehaviour
{
    public AudioSource audio1;
    public AudioSource audio2;
    Movement player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>();
        audio2.Play();
        audio1.Play();
    }

    void Update()
    {
       audio2.Pause();
        if (player.hasDoubleJump == true)
        {
            audio1.Pause();
            audio2.Play();

        }
    }
}
  
