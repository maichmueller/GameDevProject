using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;
using EZCameraShake;


public class GravityPushGun : Gun
{
    public float pushPower = 10;

    public Transform gunEnd;
    public ParticleSystem cartridgeEjection;

    private Animator anim;

    void Start()
    {
        anim = transform.GetChild(0).GetComponent<Animator>();
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

    protected override void HandleHit(RaycastHit hit)
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