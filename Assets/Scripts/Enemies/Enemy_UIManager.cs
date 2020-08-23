using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy_UIManager : MonoBehaviour
{
    Enemy_HPManager HPManager;
    Health_Bar HealthUI;

    public Slider HP_Slider;
    public float TimeActiveUI, MaxActiveUI;
    private bool ActiveUI;

    // Start is called before the first frame update
    void Awake()
    {
        ActiveUI = false;
        TimeActiveUI = 0f;

        HPManager = GetComponent<Enemy_HPManager>();
        HealthUI = HP_Slider.GetComponent<Health_Bar>();

        HealthUI.SetMaxHealth(HPManager.MaxHealth);
        HP_Slider.gameObject.SetActive(false);
    }

    void Update()
    {
        if (ActiveUI)
        {
            HP_Slider.gameObject.SetActive(true);
            TimeActiveUI += Time.deltaTime;
            if(TimeActiveUI >= MaxActiveUI)
            {
                ActiveUI = false;
                TimeActiveUI = 0f;
                HP_Slider.gameObject.SetActive(false);
            }
        }
        else
        {
            HP_Slider.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    public void HP_Update()
    {
        HealthUI.SetHealth(HPManager.Health);
        ActiveUI = true;
        TimeActiveUI = 0f;
    }

    public void DisableHP()
    {
        ActiveUI = false;
        Debug.Log(ActiveUI);
    }
}
