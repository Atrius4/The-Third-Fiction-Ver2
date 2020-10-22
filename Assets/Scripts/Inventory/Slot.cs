using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    private Inventory inventory;
    private Player_Controller player;
    private GameObject item;
    private AudioSource drinkSound;
    [SerializeField] private int i;

    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Controller>();
        drinkSound = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (transform.childCount <= 0)
        {
            inventory.isFull[i] = false;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            UseItem1();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            UseItem2();
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            UseItem3();
        }

    }

    public void UseItem1()
    {
        foreach (Transform child in transform)
        {
            if (child.gameObject.CompareTag("HealthPotion") && i == 0)
            {
                player.RegainLife();
                GameObject.Destroy(child.gameObject);
                drinkSound.Play();
            }
        }
    }

    public void UseItem2()
    {
        foreach (Transform child in transform)
        {
            if (child.gameObject.CompareTag("HealthPotion") && i == 1)
            {
                player.RegainLife();
                GameObject.Destroy(child.gameObject);
                drinkSound.Play();
            }
        }
    }

    public void UseItem3()
    {
        foreach (Transform child in transform)
        {
            if (child.gameObject.CompareTag("HealthPotion") && i == 2)
            {
                player.RegainLife();
                GameObject.Destroy(child.gameObject);
                drinkSound.Play();
            }
        }
    }

}
