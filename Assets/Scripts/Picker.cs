using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Picker : MonoBehaviour
{
    void OnMouseDown()
    {
        Debug.Log("Seleccionado : " + name);
        GameManager.id = int.Parse(name);
        SceneManager.LoadScene(0);
    }
      
    
}
