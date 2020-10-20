using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            Vector3 position = DataExtras.StringToVector2(tmp[0]);
            int health = int.Parse(tmp[1]);
            mementos[0] = new Memento(position, health);
            Load(0);
        }
    }

    public void Save()
    {
        int index = this.index % mementos.Length;
        mementos[index] = new Memento(GetComponent<Player_Controller>().transform.position, GetComponent<Player_Controller>().currentHealth);
        dataPersistance.WriteLine(mementos[index].ToString());
        this.index++;
    }

    public void Load(int index)
    {
        index = Mathf.Max(index, 0) % mementos.Length;
        if (mementos[index] == null)
            return;

        GetComponent<Player_Controller>().transform.position = mementos[index].Position;
        GetComponent<Player_Controller>().currentHealth = mementos[index].Health;
    }
}
