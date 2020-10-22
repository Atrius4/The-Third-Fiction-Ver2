using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Memory : MonoBehaviour
{
    Memento[] mementos = new Memento[1];
    int index = 0;
    DataPersistance dataPersistance;

    static Memory instance = null;
    static public Memory Instance
    {
        get
        {
            if (instance == null) instance = FindObjectOfType<Memory>();
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            dataPersistance = new DataPersistance();
        }
    }

    private void Start()
    {
        string data = dataPersistance.ReadLine();

        if (data != null)
        {
            string[] tmp = data.Split(';');
            Vector2 position = DataExtras.StringToVector2(tmp[0]);
            int health = int.Parse(tmp[1]);
            int lifes = int.Parse(tmp[2]);
            int coins = int.Parse(tmp[3]);
            bool doubleJump = bool.Parse(tmp[4]);
            int xp = int.Parse(tmp[5]);
            int lvl = int.Parse(tmp[6]);

            mementos[0] = new Memento(position, health, lifes, coins, doubleJump, xp, lvl);
            Load(0);
        }
    }

    public void Save()
    {
        int index = this.index % mementos.Length;
        mementos[index] = new Memento(Player_Controller.Instance.transform.position, 
                                      GetComponent<Player_Controller>().currentHealth, 
                                      GetComponent<Player_Controller>().lifes,
                                      GetComponent<CollectablesManager>().coins,
                                      GetComponent<Player_Controller>().hasDoubleJump,
                                      GetComponent<Level_Manager>().xp,
                                      GetComponent<Level_Manager>().lvl);

        dataPersistance.WriteLine(mementos[index].ToString());
        this.index++;
    }

    public void Load(int index)
    {
        index = Mathf.Max(index, 0) % mementos.Length;
        if (mementos[index] == null)
            return;

        Player_Controller.Instance.transform.position = mementos[index].Position;
        GetComponent<Player_Controller>().currentHealth = mementos[index].Health;

        GetComponent<Player_Controller>().lifes = mementos[index].Lifes;
        if (GetComponent<Player_Controller>().lifes == 0)
        {
            GetComponent<Player_Controller>().lifeSprite.enabled = false;
        }

        GetComponent<CollectablesManager>().coins = mementos[index].Coins;
        GetComponent<CollectablesManager>().coinsText.text = mementos[index].Coins.ToString();

        GetComponent<Player_Controller>().hasDoubleJump = mementos[index].DoubleJump;
        GetComponent<Level_Manager>().GainXP(mementos[index].Xp);

        GetComponent<Level_Manager>().SetLevel(mementos[index].Lvl);
    }
}
