using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    private void Update()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward) * 10;
        Debug.DrawRay(transform.position, forward, Color.green);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position + Vector3.up, Vector3.forward, out hit))

            if (hit.transform.CompareTag("Player"))
            {
                var jointMotor = gameObject.GetComponent<HingeJoint>();
                jointMotor.useMotor = true;
            }
    }
}
