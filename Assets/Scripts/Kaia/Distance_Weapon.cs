using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Distance_Weapon : MonoBehaviour
{
    public Transform fireOrigin;
    public GameObject kaiaBulletPrefab;
    private Kaia_AudioController weaponAudio;

    void Awake()
    {
        weaponAudio = GetComponent<Kaia_AudioController>();
    }

    public void Shoot()
    {            
        weaponAudio.PlayShootSound();
        Instantiate(kaiaBulletPrefab, fireOrigin.position, fireOrigin.rotation);

    }
}
