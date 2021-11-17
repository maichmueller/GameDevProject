using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInImage : MonoBehaviour
{
    public float fadeRate;
    public GameObject player;
    private Image _blackScreen;
    // Start is called before the first frame update
    void Start()
    {
        _blackScreen = this.gameObject.GetComponent<Image>();
        Color curColor = _blackScreen.color;
        curColor.a = 0;
        _blackScreen.color = curColor;
        player.GetComponent<Health>().DeathEvent += OnDeath;
    }

    // Update is called once per frame
    void OnDeath()
    {
        _blackScreen.enabled = true;
        StartCoroutine(FadeIn());
    }
    
    IEnumerator FadeIn() {
        var targetAlpha = 1.0f;
        Color curColor = _blackScreen.color;
        while(Mathf.Abs(curColor.a - targetAlpha) > 0.0001f) {
            // Debug.Log(_blackScreen.material.color.a);
            curColor.a = Mathf.Lerp(curColor.a, targetAlpha, fadeRate * Time.deltaTime);
            _blackScreen.color = curColor;
            yield return null;
        }
    }
}
