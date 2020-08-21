using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : MonoBehaviour
{
   
    void Start()
    {
        int index = GameManager.id;
        transform.GetChild(index).gameObject.SetActive(true);
    }

    
}
