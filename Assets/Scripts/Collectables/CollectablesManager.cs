using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectablesManager : MonoBehaviour
{
    [SerializeField] public int coins, collectedCoins;
    [SerializeField] private Text coinsText, collectedCoinsText;

    


   private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coin"))
        {
            coins ++;
            collectedCoins++;
            collectedCoinsText.text = collectedCoins.ToString();
            coinsText.text = coins.ToString();
        }
    }

    public void BuyItemCost(int itemCost)
    {
        coins -= itemCost;
        coinsText.text = coins.ToString();
    }
}
