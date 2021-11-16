using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
    public int selectedWeapon = 0;

    private void Start()
    {
        SelectWeapon();
    }

    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            // selectedWeapon = (selectedWeapon + 1) % transform.childCount;
            selectedWeapon++;
            if (selectedWeapon >= transform.childCount - 1)
            {
                selectedWeapon = 0;
            }
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            // selectedWeapon = Math.Max((selectedWeapon - 1), 0) % transform.childCount;
            selectedWeapon--;
            if (selectedWeapon < 0)
            {
                selectedWeapon = transform.childCount - 1;
            }
        }
        SelectWeapon();
    }

    private void SelectWeapon()
    {
        int i = 0;
        foreach (Transform weapon in transform)
        {
            
            Debug.Log(i == selectedWeapon ? "Actived weapon: " + selectedWeapon : "Deactivated weapon: " + i);
            weapon.gameObject.SetActive(i == selectedWeapon);
            i++;
        }
    }
}