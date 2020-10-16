using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level_Manager : MonoBehaviour
{
    [SerializeField]private Text lvlText;
    [SerializeField]private int xpToNextLvl;
    [SerializeField] private Slider xpBar;

    private Kaia_AudioController audios;

    private int xp = 0;
    private int lvl = 0;


    private void Awake()
    {
        audios = GetComponent<Kaia_AudioController>();
        xpBar.maxValue = 50;
    }


    public void SetLevel(int newLevel) // se llama en el player.
    {
        lvlText.text = newLevel.ToString();

    }

    public void GainXP(int xpValue)
    {
        xp += xpValue;
        xpBar.value = xp;
        if(xp>= xpToNextLvl)
        {
            int temp = xp - xpToNextLvl;
            lvl++;
            SetLevel(lvl);
            audios.PlaylvlUpSound();
            xp = temp;
            xpBar.value = xp;
        }
    }
}
