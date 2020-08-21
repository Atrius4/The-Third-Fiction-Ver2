using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveIA : MonoBehaviour
{
    //Player Variables
    public Vector3 PlayerTarget;
    public Transform DefaultPos;

    //Enemie Variables
    public float Speed;

    //States Machine
    public int State;

    // Update is called once per frame
    void Update()
    {
        //Estado de Ataque >> Perseguir al Jugador
        if(State == 1)
        {
            transform.GetChild(0).position = Vector2.MoveTowards
                (transform.GetChild(0).position, PlayerTarget, Time.deltaTime * Speed);
            RotateAnim(PlayerTarget);
        }
        //Estado de Reposo >> Ir a la Posicion Inicial
        else if (State == 0)
        {
            transform.GetChild(0).position = Vector2.MoveTowards
                (transform.GetChild(0).position, DefaultPos.position, Time.deltaTime * Speed);
            RotateAnim(DefaultPos.position);
        }
    }

    //Girar Sprite para coincidir con la direccion
    public void RotateAnim(Vector3 Pos)
    {
        PlayerTarget = Pos;
        float x = (transform.GetChild(0).position - Pos).x;

        if (x >= 0)
        {
            transform.GetChild(0).localScale = new Vector3(1, 1, 1);
            transform.GetChild(0).GetChild(0).localScale = new Vector3(0.005f, 0.005f, 1);
        }
        else
        {
            transform.GetChild(0).localScale = new Vector3(-1, 1, 1);
            transform.GetChild(0).GetChild(0).localScale = new Vector3(-0.005f, 0.005f, 1);
        }
    }

}
