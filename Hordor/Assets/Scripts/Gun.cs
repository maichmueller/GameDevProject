using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Gun : MonoBehaviour
{
    /// <summary>
    /// The camera attached to the player object
    /// </summary>
    public Camera mainCamera;

    /// <summary>
    /// Number in seconds which controls how often the player can fire
    /// </summary>
    public float fireRate;

    /// <summary>
    /// Distance in Unity units over which the player can fire
    /// </summary>
    public float weaponRange;

    /// <summary>
    /// The optional particle system to launch on fire as muzzle flash
    /// </summary>
    public ParticleSystem muzzleFlash = null;

    /// <summary>
    /// Float to store the time the player will be allowed to fire again, after firing
    /// </summary>
    private float nextFire;


    protected virtual void Update()
    {
        if (CanFire())
        {
            Fire();
            nextFire = Time.time + fireRate;
            if (muzzleFlash)
            {
                muzzleFlash.Play();
            }

            Ray centerRay = CenterRay();
            RaycastHit hit;
            if (Physics.Raycast(centerRay, out hit, weaponRange))
            {
                HandleHit(hit);
            }
        }
    }

    /// <summary>
    /// The function to execute on firing the gun
    /// </summary>
    protected abstract void Fire();

    /// <summary>
    /// The function to check whether a weapon can be fired
    /// </summary>
    protected virtual bool CanFire()
    {
        return Input.GetButtonDown("Fire1") && Time.time > nextFire;
    }

    /// <returns>Ray from center of the main camera's viewport forward</returns>
    private Ray CenterRay()
    {
        return mainCamera.ViewportPointToRay(Vector3.one * 0.5f);
    }

    protected abstract void HandleHit(RaycastHit ray);
}