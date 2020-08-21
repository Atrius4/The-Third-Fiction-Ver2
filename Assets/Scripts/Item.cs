using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item 
{
    public enum EItemType
    {
        HealthPotion,
        ManaPotion,
        Coin
    }

    public EItemType itemType;
    public int amount;

    public Sprite GetSprite()
    {
        switch (itemType)
        {
            default:
            case EItemType.HealthPotion:    return ItemAssets.Instance.healthPotionSprite;
            case EItemType.ManaPotion:      return ItemAssets.Instance.manaPotionSprite;
            case EItemType.Coin:            return ItemAssets.Instance.coinSprite;
        }
    }

}
