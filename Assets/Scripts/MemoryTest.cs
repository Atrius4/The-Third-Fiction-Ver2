using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryTest : MonoBehaviour
{
    Memory memory;
    int index = 0;

    private void Awake()
    {
        memory = GetComponent<Memory>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            memory.Load(index);
            index++;
        }
    }

}
