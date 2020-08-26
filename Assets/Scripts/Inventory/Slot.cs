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
            UseItem();
        }

    }

    public void UseItem()
    {
        foreach (Transform child in transform)
        {
            if (child.gameObject.CompareTag("HealthPotion"))
            {
                player.RegainLife();
                GameObject.Destroy(child.gameObject);
                drinkSound.Play();
            }
        }
    }

}
