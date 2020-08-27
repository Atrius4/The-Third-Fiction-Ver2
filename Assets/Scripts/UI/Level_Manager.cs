using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level_Manager : MonoBehaviour
{
    public Text text;

    public void SetLevel(int newLevel) // se llama en el player.
    {
        text.text = newLevel.ToString();
    }

}
