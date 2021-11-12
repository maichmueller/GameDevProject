using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float sensitivity;
    public Transform player;
    private float _xRotation;
    void Start()
    {
        //Locks the mouse to the game window
        Cursor.lockState = CursorLockMode.Locked;
        _xRotation = 0f;
    }
    
    void Update()
    {
        //Gets the raw input of the mouse coordinates and multiplies them by the sensitivity
        float x = Input.GetAxisRaw("Mouse X") * sensitivity * Time.deltaTime;
        float y = Input.GetAxisRaw("Mouse Y") * sensitivity * Time.deltaTime;
        _xRotation -= y;
        //Locks the x rotation to a 180 degree angle to make sure the player can't rotate above their head
        _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);
        //rotates the x axis by the clamped value
        transform.localRotation = Quaternion.Euler(_xRotation, 0, 0);
        //y axis can rotate full 360 degrees
        player.Rotate(Vector3.up * x);
    }
}
