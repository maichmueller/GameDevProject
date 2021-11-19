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
    public int maxNrJumps = 2;
    public LayerMask mask;
    public GameObject groundChecker;
    public GameObject cam;
    public List<Weapon> weaponList;

    private bool _isGrounded;
    private bool _wasGrounded;
    private const float Gravity = 9.81f;

    private bool _canBoost;
    private float _boostCooldown = 2f;

    private int _jumpsRemaining;
    private bool _readyToJump = true;
    private float _jumpCooldown = 0.25f;

    // Components Access
    private Transform tf;
    private Rigidbody rb;


    // Start is called before the first frame update
    void Start()
    {
        //Assigns components to vars
        _wasGrounded = CheckIsGrounded();
        tf = transform;
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckIsGrounded();

        // if (_isGrounded)
        // {
        //     _canBoost = true;
        //     _jumpsRemaining = maxNrJumps;
        //     if (!_canBoost)
        //     {
        //         rb.velocity = Vector3.zero;
        //     }
        // }

        if (_isGrounded && !_wasGrounded)
        {
            // from air to ground now
            _canBoost = true;
            _jumpsRemaining = maxNrJumps;
            rb.velocity = Vector3.zero;
        }

        Move();
        if (Input.GetButton("Jump") && _readyToJump && _jumpsRemaining >= 1)
        {
            Jump();
            _jumpsRemaining--;
        }

        if (Input.GetKey(KeyCode.LeftShift) && _canBoost)
        {
            Dash();
        }

        Invoke(nameof(ResetJump), _jumpCooldown);
    }

    private void ResetJump()
    {
        _readyToJump = true;
    }

    private void ResetBoost()
    {
        if (_isGrounded)
            _canBoost = true;
    }

    private bool CheckIsGrounded()
    {
        bool grounded = Physics.CheckSphere(groundChecker.transform.position, radius, mask);
        if (grounded != _isGrounded)
        {
            _wasGrounded = _isGrounded;
            _isGrounded = grounded;
        }

        return _isGrounded;
    }

    private void Move()
    {
        // Gets axis inputs and applies them to the rigidbodies movement
        var v = Input.GetAxis("Vertical");
        var h = Input.GetAxis("Horizontal");
        var movement = tf.forward * v + tf.right * h;
        rb.MovePosition((tf.position + (movement * (moveSpeed * Time.deltaTime))));
    }

    private void Jump()
    {
        rb.AddForce((transform.up * jumpHeight));

        Invoke(nameof(ResetJump), _jumpCooldown);
    }
    // private void Jump()
    // {
    //     
    //     readyToJump = false;
    //     if (_isGrounded)
    //     {
    //
    //         //Add jump forces
    //         rb.AddForce(Vector2.up * jumpHeight * 1.5f);
    //         rb.AddForce(normalVector * jumpHeight * 0.5f);
    //
    //         //If jumping while falling, reset y velocity.
    //         Vector3 vel = rb.velocity;
    //         if (rb.velocity.y < 0.5f)
    //             rb.velocity = new Vector3(vel.x, 0, vel.z);
    //         else if (rb.velocity.y > 0)
    //             rb.velocity = new Vector3(vel.x, vel.y / 2, vel.z);
    //
    //         Invoke(nameof(ResetJump), jumpCooldown);
    //     }
    //     else 
    //     {
    //         //Add jump forces
    //         rb.AddForce(orientation.forward * jumpForce * 1f);
    //         rb.AddForce(Vector2.up * jumpForce * 1.5f);
    //         rb.AddForce(normalVector * jumpForce * 0.5f);
    //
    //         //Reset Velocity
    //         rb.velocity = Vector3.zero;
    //
    //         //Disable dashForceCounter if doublejumping while dashing
    //         allowDashForceCounter = false;
    //
    //         Invoke(nameof(ResetJump), jumpCooldown);
    //     }
    //
    //     //Walljump
    //     if (isWallRunning)
    //     {
    //         //normal jump
    //         if (isWallLeft && !Input.GetKey(KeyCode.D) || isWallRight && !Input.GetKey(KeyCode.A))
    //         {
    //             rb.AddForce(Vector2.up * jumpForce * 1.5f);
    //             rb.AddForce(normalVector * jumpForce * 0.5f);
    //         }
    //
    //         //sidwards wallhop
    //         if (isWallRight || isWallLeft && Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
    //             rb.AddForce(-orientation.up * jumpForce * 1f);
    //         if (isWallRight && Input.GetKey(KeyCode.A)) rb.AddForce(-orientation.right * jumpForce * 3.2f);
    //         if (isWallLeft && Input.GetKey(KeyCode.D)) rb.AddForce(orientation.right * jumpForce * 3.2f);
    //
    //         //Always add forward force
    //         rb.AddForce(orientation.forward * jumpForce * 1f);
    //
    //         //Disable dashForceCounter if doublejumping while dashing
    //         allowDashForceCounter = false;
    //
    //         Invoke(nameof(ResetJump), jumpCooldown);
    //     }
    // }

    private void Dash()
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

        rb.AddForce(dir * boost, ForceMode.Force);
        _canBoost = false;
        if (_isGrounded)
        {
            Invoke(nameof(ResetBoost), _boostCooldown);
        }
    }
}