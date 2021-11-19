using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float damage;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * speed);
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnCollisionEnter(Collision other)
    {
         Destroy(gameObject);
    }
}
