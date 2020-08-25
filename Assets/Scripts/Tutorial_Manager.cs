using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Tutorial_Manager : MonoBehaviour
{
    private Text textComp;
    private string text;
    public int state = 0;
    private float nextTextCounter;
    private bool cumplido = false;
    [SerializeField]private GameObject spawningEnemy;
    private GameObject killable;
    [SerializeField] private GameObject positionSapwn;
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
                textComp.text = AttackCheck();
                break;
            case 3:
                textComp.text = DashCheck();
                break;
            case 4:
                killable = Instantiate(spawningEnemy, positionSapwn.transform.position, positionSapwn.transform.rotation);

                state = 5;
                break;
            case 5:
                textComp.text = KillingCheck(killable);
                break;
            case 6:
                if(nextTextCounter < nextTextEvery)
                {
                    SceneManager.LoadScene(2);
                }
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
            text = "Te puedes mover con '<-' , '->'";
            cumplido = false;
        }
        else if (nextTextCounter < nextTextEvery && cumplido == false)
        {
            text = "Intenta Moverte";
            nextTextCounter = nextTextEvery - 0.1f;
            if(Input.GetAxis("Horizontal") != 0)
            {
                cumplido = true;
            }
        }
        else if(nextTextCounter< nextTextEvery && cumplido == true)
        {
            text = "Muy bien";
        }
        if (nextTextCounter < 0)
        {
            state = 2;
            PassText();
        }

        return text;

    }

    public string AttackCheck()
    {
        if (nextTextCounter < 2 * nextTextEvery && nextTextCounter >= nextTextEvery)
        {
            text = "Ahora el ataque, Puedes atacar con 'Z', tambien puedes hacer un ataque mientras corres";
            cumplido = false;
        }
        else if (nextTextCounter < nextTextEvery && cumplido == false)
        {
            text = "Intenta Atacar";
            nextTextCounter = nextTextEvery - 0.1f;
            if (Input.GetKeyDown(KeyCode.Z))
            {
                cumplido = true;
            }
        }
        else if (nextTextCounter < nextTextEvery && cumplido == true)
        {
            text = "Muy bien";
        }
        if (nextTextCounter < 0)
        {
            state = 3;
            PassText();
        }

        return text;
    }

    public string DashCheck()
    {
        if (nextTextCounter < 2 * nextTextEvery && nextTextCounter >= nextTextEvery)
        {
            text = "Todos los personajes tienen un Dash, Puedes usarlo con la tecla 'X', Ten cuidado pues tiene CD";
            cumplido = false;
        }
        else if (nextTextCounter < nextTextEvery && cumplido == false)
        {
            text = "Intenta hacer un Dash";
            nextTextCounter = nextTextEvery - 0.1f;
            if (Input.GetKeyDown(KeyCode.X))
            {
                cumplido = true;
            }
        }
        else if (nextTextCounter < nextTextEvery && cumplido == true)
        {
            text = "Muy bien";
        }
        if (nextTextCounter < 0)
        {
            state = 4;
            PassText();
        }
        return text;
    }

    public string KillingCheck(GameObject tutorialEnemy)
    {
        if(tutorialEnemy != null)
        {
            text = "Ahora mata al cangrejo";
        }
        else
        {
            text = "Buen trabajo!";
            PassText();
            state = 6;
        }
        return text;
    }

    public void PassText()
    {
        nextTextCounter = nextTextEvery * 2;
    }
}
