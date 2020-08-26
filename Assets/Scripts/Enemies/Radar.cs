using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Radar : MonoBehaviour
{
    // Dependencia con el Script de Movimiento para Enemigos a Melee
    MoveIA movement;

    void Awake()
    {
        movement = GetComponent<MoveIA>();
    }

    //Deteccion del jugador
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //Activa la maquina de estados (State 1)
            movement.State = 1;
            movement.PlayerTarget = collision.transform.position;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //Activa la maquina de estados (State 0)
            movement.State = 0;
        }
    }
}
