using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* "Gravity Gun" script I quickly threw together to help another user out on Reddit.
 * When clicking the mouse button, you will grab a rigidbody object in front of the
 * main camera's view. 
 * Some initial information is recorded about where you grabbed the object, and
 * the difference between it's rotation and yours.
 * 
 * The object will be moved around according to the offset point you initially
 * picked up.
 * Moving around, the object will rotate with the player so that the player will
 * always be viewing the object at the same angle. 
 * 
 * 
 * Feel free to use or modify this script however you see fit.
 * I hope you guys can learn something from this script. Enjoy :)
 * 
 * Original author: Jake Perry, reddit.com/user/nandos13
 */
public class GravityGun : Gun
{
    // Simple Enemy
    private EnemyStateMachine _enemy;

    /// <summary>The rigidbody we are currently holding</summary>
    public Rigidbody _rigidbody;

    #region Held Object Info

    /// <summary>The offset vector from the object's position to hit point, in local space</summary>
    private Vector3 hitOffsetLocal;

    /// <summary>The distance we are holding the object at</summary>
    private float currentGrabDistance;

    /// <summary>The interpolation state when first grabbed</summary>
    private RigidbodyInterpolation initialInterpolationSetting;

    /// <summary>The difference between player & object rotation, updated when picked up or when rotated by the player</summary>
    private Vector3 rotationDifferenceEuler;

    #endregion

    /// <summary>Tracks player input to rotate current object. Used and reset every fixedupdate call</summary>
    private Vector2 rotationInput;

    public bool gravGunBeingUsed;


    new void Update()
    {
        if (!Input.GetMouseButton(0))
        {
            // We are not holding the mouse button. Release the object and return before checking for a new one

            gravGunBeingUsed = false;
            if (muzzleFlash)
            {
                muzzleFlash.Stop();
            }
            if (_rigidbody != null)
            {
                // Reset the rigidbody to how it was before we grabbed it
                _rigidbody.interpolation = initialInterpolationSetting;

                _rigidbody = null;
            }

            return;
        }
        else
        {
            gravGunBeingUsed = true;
            if (muzzleFlash)
            {
                muzzleFlash.Play();
            }
        }

        if (_rigidbody == null)
        {
            // We are not holding an object, look for one to pick up

            Ray ray = CenterRay();
            RaycastHit hit;

            Debug.DrawRay(ray.origin, ray.direction * weaponRange, Color.blue, 1f);

            if (Physics.Raycast(ray, out hit, weaponRange))
            {
                // Don't pick up kinematic rigidbodies (they can't move)
                if (hit.rigidbody != null && hit.transform.CompareTag("Enemy"))
                {
                    _enemy = hit.transform.GetComponent<EnemyStateMachine>();
                    _enemy.EnablePhysics();
                    // Track rigidbody's initial information
                    _rigidbody = hit.rigidbody;
                    initialInterpolationSetting = _rigidbody.interpolation;
                    rotationDifferenceEuler = hit.transform.rotation.eulerAngles - transform.rotation.eulerAngles;

                    hitOffsetLocal = hit.transform.InverseTransformVector(hit.point - hit.transform.position);

                    currentGrabDistance = Vector3.Distance(ray.origin, hit.point);

                    // Set rigidbody's interpolation for proper collision detection when being moved by the player
                    _rigidbody.interpolation = RigidbodyInterpolation.Interpolate;
                }
            }
        }
        else
        {
            // We are already holding an object, listen for rotation input

            if (Input.GetKey(KeyCode.R))
            {
                rotationInput += new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
            }
        }
    }

    private void FixedUpdate()
    {
        if (_rigidbody)
        {
            // We are holding an object, time to rotate & move it
            LineRenderer lineRenderer = GetComponent<LineRenderer>();
            lineRenderer.enabled = true;

            lineRenderer.SetPosition(0, transform.GetChild(0).position);
            lineRenderer.SetPosition(1, _rigidbody.transform.position);

            Ray ray = CenterRay();

            // Rotate the object to remain consistent with any changes in player's rotation
            _rigidbody.MoveRotation(Quaternion.Euler(rotationDifferenceEuler + transform.rotation.eulerAngles));

            // Get the destination point for the point on the object we grabbed
            Vector3 holdPoint = ray.GetPoint(currentGrabDistance);
            Debug.DrawLine(ray.origin, holdPoint, Color.blue, Time.fixedDeltaTime);

            // Apply any intentional rotation input made by the player & clear tracked input
            Vector3 currentEuler = _rigidbody.rotation.eulerAngles;
            _rigidbody.transform.RotateAround(holdPoint, transform.right, rotationInput.y);
            _rigidbody.transform.RotateAround(holdPoint, transform.up, -rotationInput.x);

            // Remove all torque, reset rotation input & store the rotation difference for next FixedUpdate call
            _rigidbody.angularVelocity = Vector3.zero;
            rotationInput = Vector2.zero;
            rotationDifferenceEuler = _rigidbody.transform.rotation.eulerAngles - transform.rotation.eulerAngles;

            // Calculate object's center position based on the offset we stored
            // NOTE: We need to convert the local-space point back to world coordinates
            Vector3 centerDestination = holdPoint - _rigidbody.transform.TransformVector(hitOffsetLocal);

            // Find vector from current position to destination
            Vector3 toDestination = centerDestination - _rigidbody.transform.position;

            // Calculate force
            Vector3 force = toDestination / Time.fixedDeltaTime;

            // Remove any existing velocity and add force to move to final position
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.AddForce(force, ForceMode.VelocityChange);
        }
        else
        {
            LineRenderer lineRenderer = GetComponent<LineRenderer>();
            lineRenderer.enabled = false;
        }
    }

    protected override HashSet<GameObject> CheckForHits()
    {
        return new HashSet<GameObject>();
    }

    protected override void HandleHit(HashSet<GameObject> hitObjects)
    {
    }

    protected override void Fire()
    {
    }
}