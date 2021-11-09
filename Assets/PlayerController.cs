using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private readonly float SPEED = 6f;
    private readonly float YAW_SPEED = 500f;

    // Update is called once per frame
    void Update()
    {
        MoveCharacter();
        TurnCharacter();
    }

    void MoveCharacter()
    {
        var horizontal = Input.GetAxis("Horizontal") * SPEED * Time.deltaTime;
        var vertical = Input.GetAxis("Vertical") * SPEED * Time.deltaTime;
        transform.Translate(horizontal * Vector3.right);
        transform.Translate(vertical * Vector3.forward);
    }

    void TurnCharacter()
    {
        if (Input.GetKey(KeyCode.Q))
            TurnLeft();
        if (Input.GetKey(KeyCode.E))
            TurnRight();
    }

    void TurnLeft()
    {
        transform.Rotate(Vector3.down * (YAW_SPEED * Time.deltaTime));
    }

    void TurnRight()
    {
        transform.Rotate(Vector3.up * (YAW_SPEED * Time.deltaTime));
    }
}
