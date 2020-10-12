using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kaia_AudioController : MonoBehaviour
{
    [SerializeField] AudioSource voz;
    [SerializeField] AudioSource arma;
    [SerializeField] AudioClip[] clipsVoces = new AudioClip[3];


    // Update is called once per frame

    public void PlayJumpSound()
    {
        voz.clip = clipsVoces[0];
        voz.pitch = Random.Range(0.9f, 1.1f);
        if (!voz.isPlaying)
        {
            voz.Play();
        }
    }

    public void PlayHurtSound()
    {
        voz.pitch = 1;
        voz.clip = clipsVoces[1];
        voz.Play();
    }

    public void PlayShootSound()
    {
        arma.pitch = UnityEngine.Random.Range(0.8f, 1.2f);
        arma.Play();
    }

    public void PlaylvlUpSound()
    {
        voz.clip = clipsVoces[2];
        voz.Play();
    }
}
