using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInText : MonoBehaviour
{
    public float fadeRate;
    public GameObject player;

    private Text _text;

    void Start()
    {
        _text = this.gameObject.GetComponent<Text>();
        Color curColor = _text.color;
        curColor.a = 0;
        _text.color = curColor;
        player.GetComponent<Health>().DeathEvent += OnDeath;
    }

    void OnDeath()
    {
        _text.enabled = true;
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        var targetAlpha = 1.0f;
        Color curColor = _text.color;
        while (Mathf.Abs(curColor.a - targetAlpha) > 0.01f)
        {
            curColor.a = Mathf.Lerp(curColor.a, targetAlpha, fadeRate * Time.deltaTime);
            _text.color = curColor;
            yield return null;
        }
    }
}