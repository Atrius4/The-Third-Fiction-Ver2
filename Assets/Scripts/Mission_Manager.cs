using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mission_Manager : MonoBehaviour
{

    public Image checkDoubleJump;
    public Image checkEnemy;
    public Image checkCoins;
    public Text levelComplete;
    GameObject enemy;
    GameObject player;
    private int enemyCount;
    private int mission;
    [SerializeField] private Text enemyText;

    


    // Start is called before the first frame update
    void Start()
    {
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        player = GameObject.FindGameObjectWithTag("Player");
        checkDoubleJump.enabled = false;
        checkEnemy.enabled = false;
        checkCoins.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemy.GetComponent<Enemy_HPManager>().Health <= 0)
        {
            enemyCount++;
            enemyText.text = enemyCount.ToString();
        }

        if (enemyCount == 8)
        {
            checkEnemy.enabled = true;
            mission++;
        }
        if (player.GetComponent<CollectablesManager>().coins == 45)
        {
            checkCoins.enabled = true;
            mission++;
        }
        if (player.GetComponent<Player_Controller>().hasDoubleJump == true)
        {
            checkDoubleJump.enabled = true;
            mission++;
        }


    }

}
