using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AMController : MonoBehaviour
{
    private GameObject[] _checkpoints;
    private int _currentCheckPointInArray = 0;
    private NavMeshAgent _agent;
    private Animator _animator;
    private GameObject _player;

    private float _radius;

    private static readonly int WALK_FORWARD = Animator.StringToHash("Walk Forward");

    // Start is called before the first frame update
    void Start()
    {
        _checkpoints = GameObject.FindGameObjectsWithTag("AMCheckpoints");
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _player = GameObject.FindWithTag("Player");
        _radius = 3f;
        InvokeRepeating(nameof(GoToNextCheckPoint), 0, 10);
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, _checkpoints[_currentCheckPointInArray].transform.position) <= _radius)
        {
            _animator.SetBool(WALK_FORWARD, false);
            transform.LookAt(_player.transform);
            _currentCheckPointInArray = _currentCheckPointInArray < _checkpoints.Length - 1?_currentCheckPointInArray + 1:0;
            _agent.velocity = Vector3.zero;
            _agent.isStopped = true;
        }
    }

    void GoToNextCheckPoint()
    {
        _animator.SetBool(WALK_FORWARD, true);
        _agent.destination = _checkpoints[_currentCheckPointInArray].transform.position;
        _agent.isStopped = false;
    }
    

}
