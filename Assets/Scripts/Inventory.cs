using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    private List<Item> itemList;

    public Inventory()
    {
        itemList = new List<Item>();

        AddItem(new Item { itemType = Item.EItemType.HealthPotion, amount = 1 });
        AddItem(new Item { itemType = Item.EItemType.ManaPotion, amount = 1 });
        Debug.Log(itemList.Count);
    }

    public void AddItem(Item item)
    {
        itemList.Add(item);
    }

    //Metodo para obtener los elementos de la lista de items.
    public List<Item> GetItemList()
    {
        return itemList;
    }
}
