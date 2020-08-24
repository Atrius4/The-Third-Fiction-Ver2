using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageIndicator : MonoBehaviour
{
    public Color[] DamageColor;
    Renderer GetRen;

    public float MaxTimer;
    float Timer;
    bool DamagedInd;

    void Start()
    {
        GetRen = GetComponent<Renderer>();
    }

    void Update()
    {
        if (DamagedInd)
        {
            GetRen.material.color = DamageColor[1];
            Timer += Time.deltaTime;
            if (Timer >= MaxTimer)
            {
                Timer = 0;
                DamagedInd = false;
                GetRen.material.color = DamageColor[0];
            }
        }
    }

    public void Damaged()
    {
        DamagedInd = true;
    }

}
