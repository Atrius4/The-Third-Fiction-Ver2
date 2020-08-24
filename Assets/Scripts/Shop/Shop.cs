using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    private Inventory inventory;
    private CollectablesManager collectablesManager;

    int itemCost = 0;

    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        collectablesManager = GameObject.FindGameObjectWithTag("Player").GetComponent<CollectablesManager>();
    }
    
    public void SetCost(int iCost)
    {
        itemCost = iCost;
    }

    public void BuyItem(GameObject item)
    {
        for (int i = 0; i < inventory.slots.Length; i++)
        {
            if (inventory.isFull[i] == false)
            {
                if (itemCost <= collectablesManager.coins)
                {
                    collectablesManager.BuyItemCost(itemCost);
                    inventory.isFull[i] = true;
                    Instantiate(item, inventory.slots[i].transform);
                    break;
                }
            }
        }
    }

}
