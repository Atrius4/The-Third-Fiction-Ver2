using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectablesManager : MonoBehaviour
{
    [SerializeField] public int coins, collectedCoins;
    [SerializeField] private Text coinsText, collectedCoinsText;

   

    public void BuyItemCost(int itemCost)
    {
        coins -= itemCost;
        coinsText.text = coins.ToString();
    }

    public void ObtainedCoins()
    {
        coins++;
        coinsText.text = coins.ToString();
    }
}
