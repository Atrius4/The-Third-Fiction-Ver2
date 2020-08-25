using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart_lvl : MonoBehaviour
{
    public AudioSource select;
    // Start is called before the first frame update
    void Start()
    {
        select = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            select.Play();
            SceneManager.LoadScene(1);
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            select.Play();
            SceneManager.LoadScene(0);
        }
    }
}
