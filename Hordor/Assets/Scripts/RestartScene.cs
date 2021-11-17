using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using Cursor = UnityEngine.Cursor;

public class RestartScene : MonoBehaviour
{
    void Start()
    {
        var player = GameObject.FindWithTag("Player");
        var button = this.gameObject.GetComponent<Button>();
        button.enabled = false;
        player.GetComponent<Health>().DeathEvent += () => { button.enabled = true; };
    }

    public void OnClick()
    {
        Debug.Log("RESTART BUTTON CLICKED");
        SceneManager.LoadScene("Scenes/Menu");
    }
}