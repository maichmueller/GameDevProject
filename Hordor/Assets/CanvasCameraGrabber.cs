using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasCameraGrabber : MonoBehaviour
{
    private Canvas _canvas;
    private Camera _camera;
    // Start is called before the first frame update
    void Start()
    {
        _canvas = gameObject.GetComponent<Canvas>();
        _camera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        _canvas.worldCamera = _camera;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
