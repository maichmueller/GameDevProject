using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class blink : MonoBehaviour
{
    private float timer = 0;

    public float offset = 0;
    void Update()
    {
        timer = timer + Time.deltaTime;
        if (timer >= 1 + offset)
        {
            GetComponent<TextMeshProUGUI>().enabled = true;
        }

        if (timer >= 2 + offset)
        {
            GetComponent<TextMeshProUGUI>().enabled = false;
            timer = offset;
        }
    }
}