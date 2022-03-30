using System;
using UnityEditor.Timeline;
using UnityEngine;
using UnityEngine.Serialization;

// TODO: Make parent class Buff
// On GameObject HammerOfJustice
public class HammerOfJusticeScript : MonoBehaviour
{
    [SerializeField] private float orbitalSpeed = 5f;
    [SerializeField] private float yawSpeed = 1000f;
    [SerializeField] private float damage = 100f;
    [SerializeField] private float stunPercentage = 0.1f;

    private Transform _player;
    private float _timer;

    private void Start()
    {
        _player = transform.parent;
    }

    private void Update()
    {
        OrbitAround();
        Swivel();
    }

    private void OrbitAround()
    {
        _timer += Time.deltaTime * orbitalSpeed;
        float x = Mathf.Cos(_timer);
        float z = Mathf.Sin(_timer);
        var pos = new Vector3(x, transform.localPosition.y, z);
        transform.position = pos + _player.position;
    }

    private void Swivel()
    {
        transform.Rotate(Vector3.up *  yawSpeed * Time.deltaTime, Space.World);
    }
}
