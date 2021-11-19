using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class MainMenu : MonoBehaviour
{
    public GameObject bgMusic;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    public GameObject VideoGameObject;
    
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }
    
    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }
    
    public void LoadTutorial()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
    public void OptionsClicked()
    {
        this.gameObject.SetActive(false);
        var video = transform.parent.GetChild(2).gameObject;
        video.gameObject.SetActive(true);
        VideoGameObject.SetActive(true);
        bgMusic.GetComponent<AudioSource>().Stop();
    }
    

}
