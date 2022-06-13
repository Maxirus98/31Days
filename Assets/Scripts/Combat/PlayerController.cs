using System;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    public readonly float INIT_SPEED = 6f;
    public readonly float INIT_YAW_SPEED = 100f;
    
    public float YawSpeed { get; set; }
    public float Speed { get; set; }

    private void Awake()
    {
        YawSpeed = INIT_YAW_SPEED;
        Speed = INIT_SPEED;
    }

    void Update()
    {
        MoveCharacter();
        TurnCharacter();
    }

    void MoveCharacter()
    {
        var horizontal = Input.GetAxis("Horizontal") * Speed * Time.deltaTime;
        var vertical = Input.GetAxis("Vertical") * Speed * Time.deltaTime;
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
        transform.Rotate(Vector3.down * (YawSpeed * Time.deltaTime));
    }

    void TurnRight()
    {
        transform.Rotate(Vector3.up * (YawSpeed * Time.deltaTime));
    }
}
