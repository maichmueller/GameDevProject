using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    
    // Components Access
    private Transform tf;
    private Rigidbody rb;
    
    // Start is called before the first frame update
    void Start()
    {
        tf = transform;
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var v = Input.GetAxis("Vertical");
        var h = Input.GetAxis("Horizontal");
        var movement = tf.forward * v + tf.right * h;
        Debug.Log(movement);
        rb.MovePosition((tf.position + ( movement * (moveSpeed * Time.deltaTime))));
    }
}
