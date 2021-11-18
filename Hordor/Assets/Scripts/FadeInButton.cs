using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInButton : MonoBehaviour
{
    public float fadeRate;
    public GameObject player;

    private Button _button;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        _button = this.gameObject.GetComponent<Button>();
        Color curColor = _button.image.color;
        curColor.a = 0;
        _button.image.color = curColor;
        player.GetComponent<Health>().DeathEvent += OnDeath;
    }

    void OnDeath()
    {
        _button.enabled = true;
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        var targetAlpha = 1.0f;
        Color curColor = _button.image.color;
        while (Mathf.Abs(curColor.a - targetAlpha) > 0.01f)
        {
            curColor.a = Mathf.Lerp(curColor.a, targetAlpha, fadeRate * Time.deltaTime);
            _button.image.color = curColor;
            yield return null;
        }
    }
}
