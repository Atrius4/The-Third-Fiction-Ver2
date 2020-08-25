using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial_Manager : MonoBehaviour
{
    private Text textComp;
    private string text;
    private int state = 0;
    private float nextTextCounter;
    [SerializeField] private float nextTextEvery;

    public void Awake()
    {
        textComp = GetComponent<Text>();
        nextTextCounter = 2 * nextTextEvery;
    }

    public void Update()
    {
        nextTextCounter -= Time.deltaTime;
        switch (state)
        {
            case 0:
                textComp.text = StartingTutorial();
                break;
            case 1:
                textComp.text = MoveCheck();
                break;
            case 2:
                //textComp.text = AttackCheck();
                break;
        }

    }

    public string StartingTutorial()
    {

        if(nextTextCounter < 2*nextTextEvery && nextTextCounter>= nextTextEvery)
        {
           text  = "Welcome To The third fiction";
        }
        else if (nextTextCounter < nextTextEvery)
        {
            text = "Here you will learn the basic controls";
        }
        if (nextTextCounter < 0)
        {
            state = 1;
            PassText();
        }
        return text;
    }

    public string MoveCheck()
    {
        if (nextTextCounter < 2 * nextTextEvery && nextTextCounter >= nextTextEvery)
        {
            text = "Te puedes mover con <-,->";
        }
        else if (nextTextCounter < nextTextEvery)
        {
            text = "Intentalo Ahora";
            Debug.Log(Input.GetAxis("Horizontal"));
            nextTextCounter = nextTextEvery - 0.1f;
            if(Input.GetAxis("Horizontal") != 0)
            {
                text = "Muy bien";          
            }
        }
        if (nextTextCounter < 0)
        {
            state = 2;
            PassText();
        }
        return text;

    }

    //public string AttackCheck()
    //{

    //}
    
    public void PassText()
    {
        nextTextCounter = nextTextEvery * 2;
    }
}
