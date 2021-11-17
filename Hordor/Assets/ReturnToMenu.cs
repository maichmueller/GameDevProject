using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToMenu : MonoBehaviour
{
   
    public GameObject VideoGameObject;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            this.gameObject.SetActive(false);
            VideoGameObject.SetActive(false);
            transform.parent.GetChild(1).transform.gameObject.SetActive(true);
        }
    }
}
