using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Cursor = UnityEngine.Cursor;

public class RestartScene : MonoBehaviour
{
    private bool _menuOpen = false;
    private Button _button;
    void Start()
    {
        var player = GameObject.FindWithTag("Player");
        _button = this.gameObject.GetComponent<Button>();
        player.GetComponent<Health>().DeathEvent += () => { _button.gameObject.SetActive(true); };
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!_menuOpen)
            {
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
                _button.enabled = true;
                float alpha = 1;
                var color = _button.image.color;
                color.a = alpha;
                _button.image.color = color;
                Text text = _button.transform.GetChild(0).GetComponent<Text>();
                if (text)
                {
                    color = text.color;
                    color.a = alpha;
                    text.color = color;
                }

                _menuOpen = true;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = false;
                _button.enabled = false;
                float alpha = 0;
                var color = _button.image.color;
                color.a = alpha;
                _button.image.color = color;
                Text text = _button.transform.GetChild(0).GetComponent<Text>();
                if (text)
                {
                    color = text.color;
                    color.a = alpha;
                    text.color = color;
                }
                
                _menuOpen = false;
            }
        }
    }

    public void OnClick()
    {
        Debug.Log("BACK TO MENU BUTTON CLICKED");
        SceneManager.LoadScene("Scenes/Menu");
    }
}