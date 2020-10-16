using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Mission_Manager : MonoBehaviour
{

    public Image checkDoubleJump;
    public Image checkEnemy;
    public Image checkCoins;
    GameObject player;
    [SerializeField] private Text enemyText;
    [SerializeField] private Text coinsText;

    private int enemyCount;
    private int collectedCoins;

    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        checkDoubleJump.enabled = false;
        checkEnemy.enabled = false;
        checkCoins.enabled = false;

        enemyCount = 0;
        collectedCoins = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (checkEnemy.enabled == true && checkCoins.enabled == true && checkDoubleJump.enabled == true)
        {
            SceneManager.LoadScene(4);
        }
    }

    public void UpdateEnemies()
    {
        if (enemyCount < 8)
        {
            enemyCount++;
            enemyText.text = enemyCount.ToString();
        }
        if(enemyCount >= 8)
        {
            checkEnemy.enabled = true;
        }
    }

    public void CoinObtained()
    {
        if (collectedCoins < 45)
        {
            collectedCoins++;
            coinsText.text = collectedCoins.ToString();
        }
        if(collectedCoins >= 45)
        {
            checkCoins.enabled = true;
        }
    }

    public void DJObtained()
    {
        checkDoubleJump.enabled = true;
    }





}
