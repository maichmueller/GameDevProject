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
        var hitObject = hit.collider.gameObject;
        var rb = hitObject.GetComponent<Rigidbody>();
        if (rb && hitObject.gameObject.CompareTag("Enemy"))
        {
            hitObject.GetComponent<SimpleEnemy>().EnablePhysics();
            rb.AddForce(mainCamera.transform.forward * pushPower, ForceMode.Impulse);
        }
    }
}