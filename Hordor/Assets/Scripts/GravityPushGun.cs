using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

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
        if (rb)
        {
            rb.AddForce(mainCamera.transform.forward * pushPower, ForceMode.Impulse);
        }
    }
}