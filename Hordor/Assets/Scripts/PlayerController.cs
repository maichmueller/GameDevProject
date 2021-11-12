using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float jumpHeight;
    public float boost;
    public float radius;
    public LayerMask mask;
    public GameObject groundChecker;
    public GameObject cam;
    public List<Weapon> weaponList;

    private bool _isGrounded;
    private Vector3 _jumpVector;
    private const float Gravity = 9.81f;

    private bool _canBoost;

    // Components Access
    private Transform tf;
    private Rigidbody rb;


    // Start is called before the first frame update
    void Start()
    {
        _jumpVector = Vector3.zero;
        //Assigns components to vars
        tf = transform;
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _jumpVector = Vector3.zero;
        _isGrounded = Physics.CheckSphere(groundChecker.transform.position, radius, mask);

        if (_isGrounded && !_canBoost) rb.velocity = Vector3.zero;
        if (_isGrounded) _canBoost = true;

        // Gets axis inputs and applies them to the rigidbodies movement
        var v = Input.GetAxis("Vertical");
        var h = Input.GetAxis("Horizontal");
        var movement = tf.forward * v + tf.right * h;
        //Jumping
        if (Input.GetButton("Jump") && _isGrounded)
        {
            rb.AddForce((transform.up * jumpHeight));
            //_jumpVector.y = Mathf.Sqrt(jumpHeight);
            //_jumpVector.y += Gravity * Time.deltaTime;
        }

        //rb.MovePosition((tf.position + _jumpVector + ( movement * (moveSpeed * Time.deltaTime))));
        rb.MovePosition((tf.position + (movement * (moveSpeed * Time.deltaTime))));

        if (Input.GetKey(KeyCode.LeftShift) && !_isGrounded && _canBoost)
        {
            Vector3 dir;
            if (Input.GetKey(KeyCode.A))
            {
                dir = -transform.right;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                dir = transform.right;
            }
            else
            {
                dir = cam.transform.forward;
            }
            rb.AddForce(dir * boost);
            _canBoost = false;
        }
    }
}