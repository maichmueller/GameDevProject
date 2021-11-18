using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject playerObject;
    private Image _healthBarImage;
    private Text _healthBarValueText;
    [SerializeField] private float updateSpeedSeconds = 0.5f;

    private Coroutine _activeCoroutine;

    private void Start()
    {
        mainCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        playerObject = GameObject.FindWithTag("Player");
        playerObject.GetComponent<Health>().HealthChangeEvent += OnHealthChange;
        _healthBarImage = this.transform.GetChild(1).GetComponent<Image>();
        _healthBarValueText = this.transform.GetChild(2).GetComponent<Text>();
    }


    private void OnHealthChange(float max, float absolute)
    {
        Health health = playerObject.GetComponent<Health>();
        float preChangeFill = health.currentHealth / health.MAXHealth;
        float elapsedTimeChange = 0f;
        float pct = preChangeFill + absolute / max;
        _healthBarImage.fillAmount = preChangeFill;
        _healthBarValueText.text = ((int) (_healthBarImage.fillAmount * max)).ToString() + " / " + ((int) max).ToString();
        // if (_activeCoroutine != null)
        // {
        //     StopCoroutine(_activeCoroutine);
        // }
        // _activeCoroutine = StartCoroutine(ChangeToPct(max, absolute));
    }

    // private IEnumerator ChangeToPct(float max, float absolute)
    // {
    //     Health health = playerObject.GetComponent<Health>();
    //     float preChangeFill = (health.currentHealth + absolute) / health.MAXHealth;
    //     float elapsedTimeChange = 0f;
    //     float pct = preChangeFill + absolute / max;
    //     Debug.Log("From: " + preChangeFill + " to " + pct);
    //
    //     while (elapsedTimeChange < updateSpeedSeconds)
    //     {
    //         elapsedTimeChange += Time.deltaTime;
    //         _healthBarImage.fillAmount = Mathf.Lerp(preChangeFill, pct, elapsedTimeChange / updateSpeedSeconds);
    //         _healthBarValueText.text =
    //             ((int) (_healthBarImage.fillAmount * max)).ToString() + " / " + ((int) max).ToString();
    //         yield return null;
    //     }
    //
    //     _healthBarImage.fillAmount = pct;
    //     
    // }
    
}