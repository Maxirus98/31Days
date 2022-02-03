﻿using System;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Camera _camera;
    private Canvas _canvas;

    private void Start()
    {
        _camera = GameObject.Find("PlayerCamera").GetComponent<Camera>();
    }

    void LateUpdate()
    {
        // Take current position and go 1 unity in the direction the camera is currently facing
        // Standard billboard script
        transform.LookAt(transform.position + _camera.transform.forward);
    }
}
