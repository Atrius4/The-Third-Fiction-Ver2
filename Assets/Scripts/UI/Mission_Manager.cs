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
    GameObject enemy;
    GameObject player;
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
        if (player.GetComponent<Player_Controller>().enemyCount <= 8)
        {
            enemyText.text = player.GetComponent<Player_Controller>().enemyCount.ToString();
        }

        if (player.GetComponent<Player_Controller>().enemyCount == 8)
        {
            checkEnemy.enabled = true;
        }
        if (player.GetComponent<CollectablesManager>().collectedCoins == 45)
        {
            checkCoins.enabled = true;
        }
        if (player.GetComponent<Player_Controller>().hasDoubleJump == true)
        {
            checkDoubleJump.enabled = true;
        }
        if (checkEnemy.enabled == true && checkCoins.enabled == true && checkDoubleJump.enabled == true)
        {
            SceneManager.LoadScene(4);
        }
    }



}
