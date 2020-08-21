using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint_Controller : MonoBehaviour
{
    public Sprite lampOff;
    public Sprite lampOn;
    private SpriteRenderer checkPointRenderer;
    public bool checkPointreached;
    GameObject []lamps;

    // Start is called before the first frame update
    void Start()
    {
        checkPointRenderer = GetComponent<SpriteRenderer>();
        lamps = GameObject.FindGameObjectsWithTag("Checkpoint");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            UpdateCheckPoint();
        }

    }

    void UpdateCheckPoint()
    {
        for (int i = 0; i < lamps.Length; i++)
        {
            lamps[i].GetComponent<SpriteRenderer>().sprite = lampOff;

        }
        checkPointRenderer.sprite = lampOn;
        checkPointreached = true;
    }
}
