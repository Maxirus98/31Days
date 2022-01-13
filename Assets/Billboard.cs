using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    public Transform cam;

    void LateUpdate()
    {
        // Take current position and go 1 unity in the direction the camera is currently facing
        // Standard billboard script
        transform.LookAt(transform.position + cam.forward);
    }
}
