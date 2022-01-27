using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private readonly float SPEED = 6f;
    private readonly float INIT_YAW_SPEED = 100f;
    public float YawSpeed { get; set; }

    private void Awake()
    {
        YawSpeed = INIT_YAW_SPEED;
    }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

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
        transform.Rotate(Vector3.down * (YawSpeed * Time.deltaTime));
    }

    void TurnRight()
    {
        transform.Rotate(Vector3.up * (YawSpeed * Time.deltaTime));
    }

    public void ResetYawSpeed()
    {
        YawSpeed = INIT_YAW_SPEED;
    }
}
