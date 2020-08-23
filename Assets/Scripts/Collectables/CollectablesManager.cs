using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectablesManager : MonoBehaviour
{
    [SerializeField] public int coins = 0;
    [SerializeField] private Text coinsText;


   private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coin"))
        {
            coins += 1;
            coinsText.text = coins.ToString();
        }
    }
}
