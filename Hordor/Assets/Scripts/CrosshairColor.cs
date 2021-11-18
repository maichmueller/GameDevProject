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
    private Gun activeWeapon;

    private void Start()
    {
        mainCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
    }


    private void Awake()
    {
        imgComponent = this.GetComponent<Image>();
        defaultColour = imgComponent.color;
    }

    void SetActiveWeapon()
    {
        var weaponHolder = mainCamera.transform.GetChild(0);
        activeWeapon = weaponHolder.transform
            .GetChild(weaponHolder.gameObject.GetComponent<WeaponSwitch>().selectedWeapon).gameObject
            .GetComponent<Gun>();
        var x = 4;
    }

    void Update()
    {
        SetActiveWeapon();

        Ray ray = CenterRay();
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, activeWeapon.weaponRange))
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
        else
        {
            ChangeColour(defaultColour);
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