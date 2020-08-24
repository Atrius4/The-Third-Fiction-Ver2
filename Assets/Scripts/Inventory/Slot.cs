using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    private Inventory inventory;
    private GameObject item;
    [SerializeField] private int i;

    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }

    private void Update()
    {
        if (transform.childCount <= 0)
        {
            inventory.isFull[i] = false;
        }

        /*if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            UseItem(0);
        }*/

    }

    /*public void UseItem(int slot)
    {
        foreach (Transform child in transform)
        {
            if (child.CompareTag("HealthPotion"))
            {
                Debug.Log("Curar");
            }
        }
    }

    public void DestroyItem()
    {
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }*/



}
