using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Xp_Bar : MonoBehaviour
{
    public Slider slider;
    public int xp;

    private void Update()
    {
        if (xp > 0)
        {
            slider.value++;
            xp--;
        }
    }




}
