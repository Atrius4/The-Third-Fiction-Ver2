using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController_Crab : MonoBehaviour
{
    [SerializeField]private float soundTmerValue = 3f;   //Defines a "cooldown" for each sound
    private float soundTimer;
    
    AudioSource audio;
    [SerializeField] AudioClip[] clips = new AudioClip[2];
    void Awake()
    {
        audio = GetComponent<AudioSource>();
        soundTimer = soundTmerValue;
    }

    // Update is called once per frame
    void Update()
    {
        soundTimer -= Time.deltaTime;
        if(soundTimer <= (0 - Random.Range(1.5f, -0.5f)))
        {
            audio.clip = clips[0];
            audio.pitch += Random.Range(-0.15f, 0.15f);
            if (!audio.isPlaying)
            {
                audio.Play();
            }
            soundTimer = soundTmerValue;
        }
    }

    public void PlayDeathSound()
    {
        audio.Stop();
        soundTimer = 100;
        audio.PlayOneShot(clips[1]);
    }
}
