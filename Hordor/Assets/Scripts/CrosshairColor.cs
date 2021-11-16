using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrosshairColor : MonoBehaviour
{
    private Color defaultColour;
    private float maxGrabDistance = 500;
    public Camera mainCamera;
    private Image imgComponent;

    private void Awake()
    {
        imgComponent = this.GetComponent<Image>();
        defaultColour = imgComponent.color;
    }

    void Update()
    {
        Ray ray = CenterRay();
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, maxGrabDistance))
        {
            if (hit.collider.gameObject.CompareTag("Enemy"))
            {
                ChangeColour(Color.red);
            }
            else
            {
                ChangeColour(defaultColour);
            }
        }
    }

    public void ChangeColour(Color colour)
    {
        imgComponent.color = colour;
    }
    
    /// <returns>Ray from center of the main camera's viewport forward</returns>
    private Ray CenterRay()
    {
        return mainCamera.ViewportPointToRay(Vector3.one * 0.5f);
    }
}