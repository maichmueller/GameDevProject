using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject playerObject;
    private Image _healthBarImage;
    private Text _healthBarValueText;
    [SerializeField] private float updateSpeedSeconds = 0.5f;

    private void Start()
    {
        playerObject.GetComponent<Health>().HealthChangeEvent += OnHealthChange;
        _healthBarImage = this.transform.GetChild(1).GetComponent<Image>();
        _healthBarValueText = this.transform.GetChild(2).GetComponent<Text>();
    }


    private void OnHealthChange(float max, float absolute)
    {
        StartCoroutine(ChangeToPct(max, absolute));
    }

    private IEnumerator ChangeToPct(float max, float absolute)
    {
        float preChangeFill = _healthBarImage.fillAmount;
        float elapsedTimeChange = 0f;
        float pct = preChangeFill + absolute / max;

        while (elapsedTimeChange < updateSpeedSeconds)
        {
            elapsedTimeChange += Time.deltaTime;
            _healthBarImage.fillAmount = Mathf.Lerp(preChangeFill, pct, elapsedTimeChange / updateSpeedSeconds);
            _healthBarValueText.text =
                ((int) (_healthBarImage.fillAmount * max)).ToString() + " / " + ((int) max).ToString();
            yield return null;
        }

        _healthBarImage.fillAmount = pct;
    }
    
}