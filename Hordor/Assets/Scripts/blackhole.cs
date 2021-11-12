using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blackhole : MonoBehaviour
{
    // Start is called before the first frame update
    public float pullRadius = 1;
    public float pullForce = 1;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Collider collider in Physics.OverlapSphere(transform.position, pullRadius))
        {
            if (collider.GetComponent<Rigidbody>() != null)
            {
                Vector3 forceDirection = transform.position - collider.transform.position;
                collider.GetComponent<Rigidbody>().AddForce(forceDirection.normalized * (pullForce * Time.fixedDeltaTime));   
            }
        }
    }
}
