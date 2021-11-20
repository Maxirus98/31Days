using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    private Transform _playerTransform;

    private float SPEED = 250f;

    private Vector3 offset = new Vector3(0, 3, 7);
    // Start is called before the first frame update
    void Start()
    {
        _playerTransform = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Input.GetMouseButton(0) && Input.GetMouseButton(1))
        {
            RotateCameraWithMouse();
        }
        else
        {
            StartCoroutine(nameof(LookAtPlayer));
        }
        
        FollowPlayer();
    }

    IEnumerator LookAtPlayer()
    {
        yield return new WaitForFixedUpdate();
        transform.LookAt(_playerTransform);
    }

    void RotateCameraWithMouse()
    {
        var mouseX = Input.GetAxis("Mouse X") * SPEED * Time.deltaTime;
        var mouseY=  Input.GetAxis("Mouse Y") * SPEED * Time.deltaTime;
        
        
        transform.Rotate(mouseX * Vector3.down);
        transform.Rotate( mouseY * Vector3.right);
    }

    void FollowPlayer()
    {
        transform.position = (_playerTransform.position + offset);
    }
}
