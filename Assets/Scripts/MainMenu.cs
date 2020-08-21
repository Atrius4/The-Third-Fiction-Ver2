using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void PlayGame ()
    {
        //Verifica cual es la escena activa y carga la escena siguiente segun el orden de escenas.
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); 
    }

}
