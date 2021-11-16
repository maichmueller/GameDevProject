using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;
using EZCameraShake;
using Unity.Mathematics;
using UnityEngine.ProBuilder;

public class GravityPushGun : Gun
{
    public float pushPower = 10;

    public Transform gunEnd;
    public ParticleSystem cartridgeEjection;

    private Animator anim;
    private ConeViewTrigger _coneView;
    private Vector3 _coneNormal;

    void Start()
    {
        anim = transform.GetChild(0).GetComponent<Animator>();
        // build the gun view mesh
        var cone = UnityEngine.ProBuilder.ShapeGenerator.GenerateCone(PivotLocation.Center, 5f, weaponRange, 10);
        cone.gameObject.name = "ConeView";
        var collider = cone.gameObject.AddComponent<MeshCollider>();
        _coneView = cone.gameObject.AddComponent<ConeViewTrigger>();
        collider.convex = true;
        collider.isTrigger = true;
        cone.gameObject.GetComponent<MeshRenderer>().enabled = false;
        var coneTransform = cone.transform;
        
        coneTransform.SetParent(mainCamera.transform);
        coneTransform.localScale = Vector3.one;
        coneTransform.localRotation = Quaternion.Euler(0, -90, 90);
        coneTransform.localPosition = new Vector3(0, 0, weaponRange / 2);
        
        _coneNormal = mainCamera.transform.forward.normalized * weaponRange;
    }
    

    protected override void Fire()
    {
        CameraShaker.Instance.ShakeOnce(0.15f, 4f, .1f, .1f);
        anim.SetTrigger("Fire");
        if (cartridgeEjection)
        {
            cartridgeEjection.Play();
        }
    }
    
    protected override HashSet<GameObject> CheckForHits()
    {
        return _coneView.inConeObjects;
    }

    protected override void HandleHit(HashSet<GameObject> hits)
    {
        foreach (GameObject gobject in hits)
        {
            var rb = gobject.GetComponent<Rigidbody>();
            if (rb)
            {
                gobject.GetComponent<SimpleEnemy>().EnablePhysics();
                Vector3 heading = gobject.transform.position - mainCamera.transform.position;
                Vector3 force = Vector3.Project(heading, mainCamera.transform.forward) * pushPower;

                // GetComponent<Rigidbody>().AddForce(force);
                Debug.Log("applying force " + (_coneNormal - force) + " to object (magnitute: " + (-(_coneNormal - force)).magnitude + ") gobject.name");
                rb.AddForce(-(_coneNormal - force), ForceMode.Impulse);
            }
        }
    }
}